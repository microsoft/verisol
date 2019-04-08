// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Numerics;
    using System.Text;
    using BoogieAST;
    using SolidityAST;

    public class ProcedureTranslator : BasicASTVisitor
    {
        private TranslatorContext context;

        // used to declare local vars in a Boogie implementation
        private Dictionary<string, List<BoogieVariable>> boogieToLocalVarsMap;

        // current Boogie procedure being translated to
        private string currentBoogieProc = null;

        // update in the visitor for contract definition
        private ContractDefinition currentContract = null;
        // update in the visitor for function definition
        private FunctionDefinition currentFunction = null;
        
        // store the Boogie call for modifier postlude
        private BoogieStmtList currentPostlude = null;

        public ProcedureTranslator(TranslatorContext context)
        {
            this.context = context;
            boogieToLocalVarsMap = new Dictionary<string, List<BoogieVariable>>();
        }

        public override bool Visit(ContractDefinition node)
        {
            currentContract = node;

            // generate default empty constructor if there is no constructor explicitly defined
            if (!context.IsConstructorDefined(node))
            {
                GenerateDefaultConstructor(node);
            }

            foreach (ASTNode child in node.Nodes)
            {
                if (child is VariableDeclaration varDecl)
                {
                    TranslateStateVarDeclaration(varDecl);
                }
                else if (child is StructDefinition structDefn)
                {
                    TranslateStructDefinition(structDefn);
                }
                else
                {
                    child.Accept(this);
                }
            }

            return false;
        }

        private void TranslateStructDefinition(StructDefinition structDefn)
        {
            foreach(var member in structDefn.Members)
            {
                VeriSolAssert(!member.TypeDescriptions.TypeString.StartsWith("struct "),
                    "Do no handle nested structs yet!");
                var type = TransUtils.GetBoogieTypeFromSolidityTypeName(member.TypeName);
                var mapType = new BoogieMapType(BoogieType.Ref, type);
                var mapName = member.Name + "_" + structDefn.CanonicalName;
                context.Program.AddDeclaration(new BoogieGlobalVariable(new BoogieTypedIdent(mapName, mapType)));
            }
        }

        private void TranslateStateVarDeclaration(VariableDeclaration varDecl)
        {
            VeriSolAssert(varDecl.StateVariable, $"{varDecl} is not a state variable");

            string name = TransUtils.GetCanonicalStateVariableName(varDecl, context);
            BoogieType type = TransUtils.GetBoogieTypeFromSolidityTypeName(varDecl.TypeName);
            BoogieMapType mapType = new BoogieMapType(BoogieType.Ref, type);

            if (varDecl.TypeName is Mapping)
            {
                context.Program.AddDeclaration(new BoogieGlobalVariable(new BoogieTypedIdent(name, mapType)));
            }
            else if (varDecl.TypeName is ArrayTypeName)
            {
                //array variables can be assigned
                context.Program.AddDeclaration(new BoogieGlobalVariable(new BoogieTypedIdent(name, mapType)));
            }
            else // other type of state variables
            {
                context.Program.AddDeclaration(new BoogieGlobalVariable(new BoogieTypedIdent(name, mapType)));
            }
        }

        public override bool Visit(EnumDefinition node)
        {
            // do nothing
            return false;
        }

        public override bool Visit(FunctionDefinition node)
        {
            // VeriSolAssert(node.IsConstructor || node.Modifiers.Count <= 1, "Multiple Modifiers are not supported yet");
            VeriSolAssert(currentContract != null);

            currentFunction = node;

            // procedure name
            string procName = node.Name + "_" + currentContract.Name;

            if (node.IsConstructor)
            {
                procName += "_NoBaseCtor";
            }
            currentBoogieProc = procName;

            // input parameters
            List<BoogieVariable> inParams = TransUtils.GetDefaultInParams();
            // get all formal input parameters
            node.Parameters.Accept(this);
            inParams.AddRange(currentParamList);

            // output parameters
            node.ReturnParameters.Accept(this);
            List<BoogieVariable> outParams = currentParamList;

            // attributes
            List<BoogieAttribute> attributes = new List<BoogieAttribute>();
            if ((node.Visibility == EnumVisibility.PUBLIC || node.Visibility == EnumVisibility.EXTERNAL)
                && !node.IsConstructor)
            {
                attributes.Add(new BoogieAttribute("public"));
            }
            attributes.Add(new BoogieAttribute("inline", 10));

            BoogieProcedure procedure = new BoogieProcedure(procName, inParams, outParams, attributes);
            context.Program.AddDeclaration(procedure);

            // could be just a declaration
            if (!node.Implemented)
            {
                return false;
            }

            // skip if it in ignored set
            if (context.IsMethodInIgnoredSet(node, currentContract))
            {
                Console.WriteLine($"Warning!: Ignoring method {node.Name} in contract {currentContract.Name} specified using /ignoreMethod:");
            }
            else
            {
                // local variables and function body
                boogieToLocalVarsMap[currentBoogieProc] = new List<BoogieVariable>();

                // TODO: each local array variable should be distinct and 0 initialized


                BoogieStmtList procBody = new BoogieStmtList();
                currentPostlude = new BoogieStmtList();

                // if (node.Modifiers.Count == 1)
                for (int i = 0; i < node.Modifiers.Count; ++i)
                {
                    // insert call to modifier prelude
                    if (context.ModifierToBoogiePreImpl.ContainsKey(node.Modifiers[i].ModifierName.Name))
                    {
                        List<BoogieExpr> arguments = TransUtils.GetDefaultArguments();
                        if (node.Modifiers[i].Arguments != null)
                            arguments.AddRange(node.Modifiers[i].Arguments.ConvertAll(TranslateExpr));
                        string callee = node.Modifiers[i].ModifierName.ToString() + "_pre";
                        BoogieCallCmd callCmd = new BoogieCallCmd(callee, arguments, null);
                        procBody.AddStatement(callCmd);
                    }

                    // insert call to modifier postlude
                    if (context.ModifierToBoogiePostImpl.ContainsKey(node.Modifiers[i].ModifierName.Name))
                    {
                        List<BoogieExpr> arguments = TransUtils.GetDefaultArguments();
                        if (node.Modifiers[i].Arguments != null)
                            arguments.AddRange(node.Modifiers[i].Arguments.ConvertAll(TranslateExpr));
                        string callee = node.Modifiers[i].ModifierName.ToString() + "_post";
                        BoogieCallCmd callCmd = new BoogieCallCmd(callee, arguments, null);
                        currentPostlude.AddStatement(callCmd);
                    }
                }

                procBody.AppendStmtList(TranslateStatement(node.Body));

                // add modifier postlude call if function body has no return
                if (currentPostlude != null)
                {
                    procBody.AppendStmtList(currentPostlude);
                    currentPostlude = null;
                }

                // initialization statements
                if (node.IsConstructor)
                {
                    BoogieStmtList initStmts = GenerateInitializationStmts(currentContract);
                    initStmts.AppendStmtList(procBody);
                    procBody = initStmts;
                }

                List<BoogieVariable> localVars = boogieToLocalVarsMap[currentBoogieProc];

                BoogieImplementation impelementation = new BoogieImplementation(procName, inParams, outParams, localVars, procBody);
                context.Program.AddDeclaration(impelementation);
            }

            // generate real constructors
            if (node.IsConstructor)
            {
                GenerateConstructorWithBaseCalls(node, inParams);
            }

            return false;
        }

        public override bool Visit(ModifierDefinition node)
        {
            currentBoogieProc = node.Name + "_pre";
            boogieToLocalVarsMap[currentBoogieProc] = new List<BoogieVariable>();

            Block body = node.Body;
            BoogieStmtList prelude = new BoogieStmtList();
            BoogieStmtList postlude = new BoogieStmtList();

            bool translatingPre = true;
            bool hasPre = false;
            bool hasPost = false;
            foreach (Statement statement in body.Statements)
            {
                if (statement is VariableDeclarationStatement)
                {
                    throw new System.Exception("locals within modifiers not supported");
                }
                if (statement is PlaceholderStatement)
                {
                    translatingPre = false;
                    currentBoogieProc = node.Name + "_post";
                    boogieToLocalVarsMap[currentBoogieProc] = new List<BoogieVariable>();
                    continue;
                }
                BoogieStmtList stmtList = TranslateStatement(statement);
                if (translatingPre)
                {
                    prelude.AppendStmtList(stmtList);
                    hasPre = true;
                }
                else
                {
                    postlude.AppendStmtList(stmtList);
                    hasPost = true;
                }
            }
            if (hasPre)
            {
                context.ModifierToBoogiePreImpl[node.Name].LocalVars = boogieToLocalVarsMap[node.Name + "_pre"];
                context.ModifierToBoogiePreImpl[node.Name].StructuredStmts = prelude;
            }
            if (hasPost)
            {
                context.ModifierToBoogiePostImpl[node.Name].LocalVars = boogieToLocalVarsMap[node.Name + "_post"];
                context.ModifierToBoogiePostImpl[node.Name].StructuredStmts = postlude;
            }
            return false;
        }

        // generate the initialization statements for state variables
        // assume message sender is not null
        // assign null to other address variables
        private BoogieStmtList GenerateInitializationStmts(ContractDefinition contract)
        {
            BoogieStmtList stmtList = new BoogieStmtList();
            stmtList.AddStatement(new BoogieCommentCmd("start of initialization"));

            // assume msgsender_MSG != null;
            BoogieExpr assumeLhs = new BoogieIdentifierExpr("msgsender_MSG");
            BoogieExpr assumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, assumeLhs, new BoogieIdentifierExpr("null"));
            BoogieAssumeCmd assumeCmd = new BoogieAssumeCmd(assumeExpr);
            stmtList.AddStatement(assumeCmd);

            // assign null to other address variables
            foreach (VariableDeclaration varDecl in context.GetStateVarsByContract(contract))
            {
                if (varDecl.TypeName is ElementaryTypeName elementaryType)
                {
                    if (elementaryType.TypeDescriptions.TypeString.Equals("address"))
                    {
                        string varName = TransUtils.GetCanonicalStateVariableName(varDecl, context);
                        BoogieExpr lhs = new BoogieMapSelect(new BoogieIdentifierExpr(varName), new BoogieIdentifierExpr("this"));
                        BoogieAssignCmd assignCmd = new BoogieAssignCmd(lhs, new BoogieIdentifierExpr("null"));
                        stmtList.AddStatement(assignCmd);
                    }
                    else if (elementaryType.TypeDescriptions.TypeString.Equals("bool"))
                    {
                        string varName = TransUtils.GetCanonicalStateVariableName(varDecl, context);
                        BoogieExpr lhs = new BoogieMapSelect(new BoogieIdentifierExpr(varName), new BoogieIdentifierExpr("this"));
                        bool value = false;
                        if (varDecl.Value != null && varDecl.Value.ToString() == "true")
                        {
                            value = true;
                        }
                        BoogieAssignCmd assignCmd = new BoogieAssignCmd(lhs, new BoogieLiteralExpr(value));
                        stmtList.AddStatement(assignCmd);
                    }
                    else if (elementaryType.TypeDescriptions.TypeString.Equals("string")) 
                    {
                        string x = "";
                        if (varDecl.Value != null)
                        {
                            x = varDecl.Value.ToString();
                            x=x.Substring(1, x.Length - 2);  //to strip off the single quotations
                        }
                        int hashCode = x.GetHashCode();
                        BigInteger num = new BigInteger(hashCode);
                        string varName = TransUtils.GetCanonicalStateVariableName(varDecl, context);
                        BoogieExpr lhs = new BoogieMapSelect(new BoogieIdentifierExpr(varName), new BoogieIdentifierExpr("this"));
                        BoogieAssignCmd assignCmd = new BoogieAssignCmd(lhs, new BoogieLiteralExpr(num));
                        stmtList.AddStatement(assignCmd);
                    }
                    else //it is integer valued
                    {
                        string varName = TransUtils.GetCanonicalStateVariableName(varDecl, context);
                        BoogieExpr lhs = new BoogieMapSelect(new BoogieIdentifierExpr(varName), new BoogieIdentifierExpr("this"));
                        BigInteger bigInt = BigInteger.Zero;
                        if (varDecl.Value!=null)
                        {
                            string valStr = varDecl.Value.ToString();
                            if (valStr[0] == '\'')
                                valStr = valStr.Substring(1, valStr.Length - 2); //to strip off the single quotations
                            int baseOfValue = (valStr.StartsWith("0x", true, new CultureInfo("en-US"))) ? 16 : 10;
                            decimal value = new decimal(Convert.ToInt32(valStr, baseOfValue));
                            bigInt = new BigInteger(value);
                        }
                        BoogieAssignCmd assignCmd = new BoogieAssignCmd(lhs, new BoogieLiteralExpr(bigInt));
                        stmtList.AddStatement(assignCmd);
                    }
                }
            }

            // false/0 initialize mappings
            foreach (VariableDeclaration varDecl in context.GetStateVarsByContract(contract))
            {
                if (varDecl.TypeName is Mapping mapping)
                {
                    BoogieMapSelect lhsMap = CreateDistinctArrayMappingAddress(stmtList, varDecl);

                    //nested arrays (only 1 level for now)
                    if (mapping.ValueType is ArrayTypeName array)
                    {
                        Console.WriteLine($"Warning: A mapping with nested array {varDecl.Name} of valuetype {mapping.ValueType.ToString()}");
                        stmtList.AddStatement(new BoogieCommentCmd($"Initialize length of 1-level nested array in {varDecl.Name}"));


                        // Issue with inferring Array[] expressions in GetBoogieTypesFromMapping (TODO: use GetBoogieTypesFromMapping after fix)
                        var mapKeyType = MapArrayHelper.InferExprTypeFromTypeString(mapping.KeyType.TypeDescriptions.ToString());
                        string mapName = MapArrayHelper.GetMemoryMapName(mapKeyType, BoogieType.Ref);
                        string varName = TransUtils.GetCanonicalStateVariableName(varDecl, context);
                        var varExpr = new BoogieIdentifierExpr(varName);
                        //lhs is Mem_t_ref[x[this]]
                        var lhs0 = new BoogieMapSelect(new BoogieIdentifierExpr(mapName),
                            new BoogieMapSelect(varExpr, new BoogieIdentifierExpr("this")));
                        var qVar1 = QVarGenerator.NewQVar(0, 0);
                        //lhs is Mem_t_ref[x[this]][i]
                        var lhs1 = new BoogieMapSelect(lhs0, qVar1);
                        //Length[Mem_t_ref[x[this]][i]] == 0
                        var bodyExpr = new BoogieBinaryOperation(
                            BoogieBinaryOperation.Opcode.EQ,
                            new BoogieMapSelect(new BoogieIdentifierExpr("Length"), lhs1),
                            new BoogieLiteralExpr(0));
                        var qExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar1 }, new List<BoogieType>() { mapKeyType}, bodyExpr);
                        stmtList.AddStatement(new BoogieAssumeCmd(qExpr));

                        //Nested arrays are disjoint and disjoint from other addresses
                        BoogieExpr allocExpr = new BoogieMapSelect(new BoogieIdentifierExpr("Alloc"), lhs1);
                        var negAllocExpr = new BoogieUnaryOperation(BoogieUnaryOperation.Opcode.NOT, allocExpr);
                        var negAllocQExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar1 }, new List<BoogieType>() { mapKeyType }, negAllocExpr);
                        //assume forall i !Alloc[M_t_ref[x[this]][i]]
                        stmtList.AddStatement(new BoogieAssumeCmd(negAllocQExpr));
                        //call HavocAllocMany()
                        stmtList.AddStatement(new BoogieCallCmd("HavocAllocMany", new List<BoogieExpr>(), new List<BoogieIdentifierExpr>()));
                        //assume forall i. Alloc[M_t_ref[x[this]][i]]
                        var allocQExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar1 }, new List<BoogieType>() { mapKeyType }, allocExpr);
                        stmtList.AddStatement(new BoogieAssumeCmd(allocQExpr));

                        //Two different keys/indices within the same array are distinct
                        //forall i, j: i != j ==> M_t_ref[x[this]][i] != M_t_ref[x[this]][j]
                        var qVar2 = QVarGenerator.NewQVar(0, 1);
                        var lhs2 = new BoogieMapSelect(lhs0, qVar2);
                        var distinctQVars = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, qVar1, qVar2);
                        var distinctLhs = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, lhs1, lhs2);
                        var neqExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, distinctQVars, distinctLhs);
                        var distinctQExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar1, qVar2 }, new List<BoogieType>() { mapKeyType, mapKeyType }, neqExpr);
                        stmtList.AddStatement(new BoogieAssumeCmd(distinctQExpr));
                    }
                    else if (mapping.ValueType is UserDefinedTypeName userTypeName ||
                        mapping.ValueType.ToString().Equals("address"))
                    {
                        stmtList.AddStatement(new BoogieCommentCmd($"Initialize address/contract mapping {varDecl.Name}"));

                        BoogieType mapKeyType;
                        BoogieMapSelect lhs;
                        GetBoogieTypesFromMapping(varDecl, mapping, out mapKeyType, out lhs);
                        var qVar = QVarGenerator.NewQVar(0, 0);
                        var bodyExpr = new BoogieBinaryOperation(
                            BoogieBinaryOperation.Opcode.EQ,
                            new BoogieMapSelect(lhs, qVar),
                            new BoogieIdentifierExpr("null"));
                        var qExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar }, new List<BoogieType>() { mapKeyType }, bodyExpr);
                        stmtList.AddStatement(new BoogieAssumeCmd(qExpr));
                    }
                    else if (mapping.ValueType.ToString().Equals("bool"))
                    {
                        stmtList.AddStatement(new BoogieCommentCmd($"Initialize Boolean mapping {varDecl.Name}"));

                        BoogieType mapKeyType;
                        BoogieMapSelect lhs;
                        GetBoogieTypesFromMapping(varDecl, mapping, out mapKeyType, out lhs);
                        var qVar = QVarGenerator.NewQVar(0, 0);
                        var bodyExpr = new BoogieUnaryOperation(BoogieUnaryOperation.Opcode.NOT, new BoogieMapSelect(lhs, qVar));
                        var qExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar }, new List<BoogieType>() { mapKeyType }, bodyExpr);
                        stmtList.AddStatement(new BoogieAssumeCmd(qExpr));
                    }
                    // TODO: Cleanup, StartsWith("uint") can include uint[12] as well. 
                    else if (mapping.ValueType.ToString().StartsWith("uint") ||
                        mapping.ValueType.ToString().StartsWith("int"))
                    {
                        stmtList.AddStatement(new BoogieCommentCmd($"Initialize Integer mapping {varDecl.Name}"));

                        BoogieType mapKeyType;
                        BoogieMapSelect lhs;
                        GetBoogieTypesFromMapping(varDecl, mapping, out mapKeyType, out lhs);
                        var qVar = QVarGenerator.NewQVar(0, 0);
                        var bodyExpr = new BoogieBinaryOperation(
                            BoogieBinaryOperation.Opcode.EQ, 
                            new BoogieMapSelect(lhs, qVar),
                            new BoogieLiteralExpr(0));
                        var qExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar }, new List<BoogieType>() { mapKeyType }, bodyExpr);
                        stmtList.AddStatement(new BoogieAssumeCmd(qExpr));
                    }
                    else
                    {
                        Console.WriteLine($"Warning: A mapping with complex value type {varDecl.Name} of valuetype {mapping.ValueType.ToString()}");
                    }

                }
                else if (varDecl.TypeName is ArrayTypeName array)
                {
                    BoogieMapSelect lhsMap = CreateDistinctArrayMappingAddress(stmtList, varDecl);

                    // lets also initialize the array Lengths (only for Arrays declared in this class)
                    var lengthMapSelect = new BoogieMapSelect(new BoogieIdentifierExpr("Length"), lhsMap);
                    var lengthExpr = array.Length == null ? new BoogieLiteralExpr(BigInteger.Zero) : TranslateExpr(array.Length);
                    // var lengthEqualsZero = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, lengthMapSelect, new BoogieLiteralExpr(0));
                    var lengthConstraint = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, lengthMapSelect, lengthExpr);
                    stmtList.AddStatement(new BoogieAssumeCmd(lengthConstraint));
                }
            }


            stmtList.AddStatement(new BoogieCommentCmd("end of initialization"));

            // TODO: add the initializations outside of constructors

            return stmtList;
        }

        private BoogieMapSelect CreateDistinctArrayMappingAddress(BoogieStmtList stmtList, VariableDeclaration varDecl)
        {
            // define a local variable to generate a fresh constant
            BoogieLocalVariable tmpVar = new BoogieLocalVariable(context.MakeFreshTypedIdent(BoogieType.Ref));
            boogieToLocalVarsMap[currentBoogieProc].Add(tmpVar);
            BoogieIdentifierExpr tmpVarIdentExpr = new BoogieIdentifierExpr(tmpVar.Name);

            stmtList.AddStatement(new BoogieCommentCmd($"Make array/mapping vars distinct for {varDecl.Name}"));
            var lhs = new BoogieIdentifierExpr(TransUtils.GetCanonicalStateVariableName(varDecl, context));
            var lhsMap = new BoogieMapSelect(lhs, new BoogieIdentifierExpr("this"));
            stmtList.AddStatement(new BoogieCallCmd(
                "FreshRefGenerator",
                new List<BoogieExpr>(),
                new List<BoogieIdentifierExpr>() {
                            tmpVarIdentExpr
                }
                ));

            stmtList.AddStatement(new BoogieAssignCmd(lhsMap, tmpVarIdentExpr));
            return lhsMap;
        }

        private void GetBoogieTypesFromMapping(VariableDeclaration varDecl, Mapping mapping, out BoogieType mapKeyType, out BoogieMapSelect lhs)
        {
            mapKeyType = MapArrayHelper.InferExprTypeFromTypeString(mapping.KeyType.TypeDescriptions.ToString());
            var mapValueTypeString = mapping.ValueType is UserDefinedTypeName ?
                ((UserDefinedTypeName)mapping.ValueType).TypeDescriptions.ToString() :
                mapping.ValueType.ToString();
            // needed as a mapping(int => contract A) only has "A" as the valueType.ToSTring()
            var mapValueType = MapArrayHelper.InferExprTypeFromTypeString(mapValueTypeString);
            string mapName = MapArrayHelper.GetMemoryMapName(mapKeyType, mapValueType);

            string varName = TransUtils.GetCanonicalStateVariableName(varDecl, context);
            var varExpr = new BoogieIdentifierExpr(varName);
            lhs = new BoogieMapSelect(new BoogieIdentifierExpr(mapName),
                new BoogieMapSelect(varExpr, new BoogieIdentifierExpr("this")));
        }

        // generate the default empty constructors, including an internal one without base ctors, and an actual one with base ctors
        private void GenerateDefaultConstructor(ContractDefinition contract)
        {
            // generate the internal one without base constructors
            string procName = contract.Name + "_" + contract.Name + "_NoBaseCtor";
            currentBoogieProc = procName;
            if (!boogieToLocalVarsMap.ContainsKey(currentBoogieProc))
            {
                boogieToLocalVarsMap[currentBoogieProc] = new List<BoogieVariable>();
            }
            
            List<BoogieVariable> inParams = TransUtils.GetDefaultInParams();
            List<BoogieVariable> outParams = new List<BoogieVariable>();
            List<BoogieAttribute> attributes = new List<BoogieAttribute>()
            {
                new BoogieAttribute("inline", 10),
            };
            BoogieProcedure procedure = new BoogieProcedure(procName, inParams, outParams, attributes);
            context.Program.AddDeclaration(procedure);

            BoogieStmtList procBody = GenerateInitializationStmts(contract);
            List<BoogieVariable> localVars = boogieToLocalVarsMap[currentBoogieProc];
            BoogieImplementation implementation = new BoogieImplementation(procName, inParams, outParams, localVars, procBody);
            context.Program.AddDeclaration(implementation);

            // generate the actual one with base constructors
            string ctorName = contract.Name + "_" + contract.Name;
            BoogieProcedure ctorWithBaseCalls = new BoogieProcedure(ctorName, inParams, outParams, attributes);
            context.Program.AddDeclaration(ctorWithBaseCalls);

            List<BoogieVariable> ctorLocalVars = new List<BoogieVariable>();
            BoogieStmtList ctorBody = new BoogieStmtList();

            List<int> baseContractIds = new List<int>(contract.LinearizedBaseContracts);
            baseContractIds.Reverse();
            foreach (int id in baseContractIds)
            {
                ContractDefinition baseContract = context.GetASTNodeById(id) as ContractDefinition;
                VeriSolAssert(baseContract != null);

                string callee = TransUtils.GetCanonicalConstructorName(baseContract) + "_NoBaseCtor";
                List<BoogieExpr> inputs = new List<BoogieExpr>();
                List<BoogieIdentifierExpr> outputs = new List<BoogieIdentifierExpr>();

                InheritanceSpecifier inheritanceSpecifier = GetInheritanceSpecifierOfBase(contract, baseContract);
                if (inheritanceSpecifier != null)
                {
                    inputs.Add(new BoogieIdentifierExpr("this"));
                    inputs.Add(new BoogieIdentifierExpr("msgsender_MSG"));
                    inputs.Add(new BoogieIdentifierExpr("msgvalue_MSG"));
                    foreach (Expression argument in inheritanceSpecifier.Arguments)
                    {
                        inputs.Add(TranslateExpr(argument));
                    }
                }
                else // no argument for this base constructor
                {
                    foreach (BoogieVariable param in inParams)
                    {
                        inputs.Add(new BoogieIdentifierExpr(param.TypedIdent.Name));
                    }
                }
                BoogieCallCmd callCmd = new BoogieCallCmd(callee, inputs, outputs);
                ctorBody.AddStatement(callCmd);
            }
            BoogieImplementation ctorImpl = new BoogieImplementation(ctorName, inParams, outParams, ctorLocalVars, ctorBody);
            context.Program.AddDeclaration(ctorImpl);
        }

        // generate actual constructor procedures that invoke constructors without base in linearized order
        private void GenerateConstructorWithBaseCalls(FunctionDefinition ctor, List<BoogieVariable> inParams)
        {
            VeriSolAssert(ctor.IsConstructor, $"{ctor.Name} is not a constructor");

            ContractDefinition contract = context.GetContractByFunction(ctor);
            string procName = contract.Name + "_" + contract.Name;

            // no output params for constructor
            List<BoogieVariable> outParams = new List<BoogieVariable>();
            List<BoogieAttribute> attributes = new List<BoogieAttribute>()
            {
                new BoogieAttribute("constructor"),
                new BoogieAttribute("public"),
                new BoogieAttribute("inline", 10),
            };

            BoogieProcedure procedure = new BoogieProcedure(procName, inParams, outParams, attributes);
            context.Program.AddDeclaration(procedure);

            // skip if it in ignored set
            if (context.IsMethodInIgnoredSet(ctor, currentContract))
            {
                Console.WriteLine($"Warning!: Ignoring constructor {ctor.Name} in contract {currentContract.Name} specified using /ignoreMethod:");
            }
            else
            {

                // no local variables for constructor
                List<BoogieVariable> localVars = new List<BoogieVariable>();
                BoogieStmtList ctorBody = new BoogieStmtList();

                List<int> baseContractIds = new List<int>(contract.LinearizedBaseContracts);
                baseContractIds.Reverse();

                //Note that the current derived contract appears as a baseContractId 
                foreach (int id in baseContractIds)
                {
                    ContractDefinition baseContract = context.GetASTNodeById(id) as ContractDefinition;
                    VeriSolAssert(baseContract != null);

                    // since we are not translating any statements, currentStmtList remains null
                    currentStmtList = new BoogieStmtList();

                    string callee = TransUtils.GetCanonicalConstructorName(baseContract) + "_NoBaseCtor" ;
                    
                    List<BoogieExpr> inputs = new List<BoogieExpr>();
                    List<BoogieIdentifierExpr> outputs = new List<BoogieIdentifierExpr>();

                    InheritanceSpecifier inheritanceSpecifier = GetInheritanceSpecifierOfBase(contract, baseContract);
                    ModifierInvocation modifierInvocation = GetModifierInvocationOfBase(ctor, baseContract);
                    if (inheritanceSpecifier != null)
                    {
                        inputs.Add(new BoogieIdentifierExpr("this"));
                        inputs.Add(new BoogieIdentifierExpr("msgsender_MSG"));
                        inputs.Add(new BoogieIdentifierExpr("msgvalue_MSG"));
                        foreach (Expression argument in inheritanceSpecifier.Arguments)
                        {
                            inputs.Add(TranslateExpr(argument));
                        }
                    }
                    else if (modifierInvocation != null)
                    {
                        inputs.Add(new BoogieIdentifierExpr("this"));
                        inputs.Add(new BoogieIdentifierExpr("msgsender_MSG"));
                        inputs.Add(new BoogieIdentifierExpr("msgvalue_MSG"));
                        foreach (Expression argument in modifierInvocation.Arguments)
                        {
                            inputs.Add(TranslateExpr(argument));
                        }
                    }
                    else // no argument for this base constructor
                    {
                        if (baseContract.Name == contract.Name)
                        {
                            // only do this for the derived contract
                            foreach (BoogieVariable param in inParams)
                            {
                                inputs.Add(new BoogieIdentifierExpr(param.TypedIdent.Name));
                            }
                        }
                        else
                        {

                            // Do we call the constructor or assume that it is invoked in teh base contract?
                            /* Assume it is invoked in the constructor for the base contract if the parameter list is non-empty (HACK) */ 
                            var baseCtr = context.IsConstructorDefined(baseContract) ? context.GetConstructorByContract(baseContract) : null;
                            if (baseCtr != null && baseCtr.Parameters.Length() > 0)
                            {
                                Console.WriteLine($"Warning!!: Base constructor { callee} has non-empty parameters but not specified in { ctor.Name}...assuming it is invoked from a base contract");
                                currentStmtList = null;
                                continue;
                            }
                            inputs.Add(new BoogieIdentifierExpr("this"));
                            inputs.Add(new BoogieIdentifierExpr("msgsender_MSG"));
                            inputs.Add(new BoogieIdentifierExpr("msgvalue_MSG"));
                        }
                    }
                    BoogieCallCmd callCmd = new BoogieCallCmd(callee, inputs, outputs);
                    ctorBody.AppendStmtList(currentStmtList);
                    ctorBody.AddStatement(callCmd);
                    currentStmtList = null;
                }

                localVars.AddRange(boogieToLocalVarsMap[currentBoogieProc]);
                BoogieImplementation implementation = new BoogieImplementation(procName, inParams, outParams, localVars, ctorBody);
                context.Program.AddDeclaration(implementation);
            }
        }

        // get the inheritance specifier of `baseContract' in contract definition `contract' if it specifies arguments
        // return null if there is no matching inheritance specifier
        // NOTE: two inheritance specifiers having the same name leads to a compile error
        private InheritanceSpecifier GetInheritanceSpecifierOfBase(ContractDefinition contract, ContractDefinition baseContract)
        {
            foreach (InheritanceSpecifier inheritanceSpecifier in contract.BaseContracts)
            {
                if (inheritanceSpecifier.Arguments != null && inheritanceSpecifier.BaseName.Name.Equals(baseContract.Name))
                {
                    return inheritanceSpecifier;
                }
            }
            return null;
        }

        // get the modifier invocation of `baseContract' in constructor `ctor'
        // return null if there is no matching modifier
        // NOTE: two constructor modifiers having the same name leads to a compile error
        private ModifierInvocation GetModifierInvocationOfBase(FunctionDefinition ctor, ContractDefinition baseContract)
        {
            foreach (ModifierInvocation modifierInvocation in ctor.Modifiers)
            {
                int id = modifierInvocation.ModifierName.ReferencedDeclaration;
                if (context.GetASTNodeById(id) is ContractDefinition contractDef)
                {
                    if (contractDef == baseContract)
                    {
                        return modifierInvocation;
                    }
                }
            }
            return null;
        }

        // updated in the visitor of parameter list
        private List<BoogieVariable> currentParamList;
        
        public override bool Visit(ParameterList node)
        {
            currentParamList = new List<BoogieVariable>();
            var retParamCount = 0;
            foreach (VariableDeclaration parameter in node.Parameters)
            {
                string name = null;
                if (String.IsNullOrEmpty(parameter.Name))
                {
                    //name = "__ret";
                    name = $"__ret_{retParamCount++}_" ;
                }
                else
                {
                    name = TransUtils.GetCanonicalLocalVariableName(parameter);
                }
                BoogieType type = TransUtils.GetBoogieTypeFromSolidityTypeName(parameter.TypeName);
                currentParamList.Add(new BoogieFormalParam(new BoogieTypedIdent(name, type)));
            }
            return false;
        }

        // update in the visitor of different statements
        // this now accumulates all Boogie stmts generated when they are being generated
        private BoogieStmtList currentStmtList = null;

        /// <summary>
        /// This the only method that returns a BoogieStmtList (value of currentStmtList)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public BoogieStmtList TranslateStatement(Statement node)
        {
            //push the current Statement
            var oldCurrentStmtList = currentStmtList; 

            //new scope
            currentStmtList = new BoogieStmtList(); // reset before starting to translate a Statement
            node.Accept(this);
            VeriSolAssert(currentStmtList != null);

            // add source file path and line number
            BoogieAssertCmd annotationCmd = TransUtils.GenerateSourceInfoAnnotation(node, context);
            BoogieStmtList annotatedStmtList = BoogieStmtList.MakeSingletonStmtList(annotationCmd);
            annotatedStmtList.AppendStmtList(currentStmtList);

            currentStmtList = oldCurrentStmtList; // pop the stack of currentStmtList

            return annotatedStmtList;
        }

        public override bool Visit(Block node)
        {
            BoogieStmtList block = new BoogieStmtList();
            foreach (Statement statement in node.Statements)
            {
                BoogieStmtList stmtList = TranslateStatement(statement);
                block.AppendStmtList(stmtList);
            }

            currentStmtList = block;
            return false;
        }

        public override bool Visit(PlaceholderStatement node)
        {
            return false;
        }

        public override bool Visit(VariableDeclarationStatement node)
        {
            foreach (VariableDeclaration varDecl in node.Declarations)
            {
                string name = TransUtils.GetCanonicalLocalVariableName(varDecl);
                BoogieType type = TransUtils.GetBoogieTypeFromSolidityTypeName(varDecl.TypeName);
                boogieToLocalVarsMap[currentBoogieProc].Add(new BoogieLocalVariable(new BoogieTypedIdent(name, type)));
            }

            // handle the initial value of variable declaration
            if (node.InitialValue != null)
            {
                VeriSolAssert(node.Declarations.Count == 1, "Invalid multiple variable declarations");

                // de-sugar to variable declaration and an assignment
                VariableDeclaration varDecl = node.Declarations[0];

                Identifier identifier = new Identifier();
                identifier.Name = varDecl.Name;
                identifier.ReferencedDeclaration = varDecl.Id;

                Assignment assignment = new Assignment();
                assignment.LeftHandSide = identifier;
                assignment.Operator = "=";
                assignment.RightHandSide = node.InitialValue;

                // call the visitor for assignments
                assignment.Accept(this);
            }
            else
            {
                // havoc the declared variables
                List<BoogieIdentifierExpr> varsToHavoc = new List<BoogieIdentifierExpr>();
                foreach (VariableDeclaration varDecl in node.Declarations)
                {
                    string varIdent = TransUtils.GetCanonicalLocalVariableName(varDecl);
                    varsToHavoc.Add(new BoogieIdentifierExpr(varIdent));
                }
                BoogieHavocCmd havocCmd = new BoogieHavocCmd(varsToHavoc);
                currentStmtList.AppendStmtList(BoogieStmtList.MakeSingletonStmtList(havocCmd));
            }
            return false;
        }

        public override bool Visit(Assignment node)
        {
            List<BoogieExpr> lhs = new List<BoogieExpr>();
            List<BoogieType> lhsTypes = new List<BoogieType>(); //stores types in case of tuples

            bool isTupleAssignment = false;

            if (node.LeftHandSide is TupleExpression tuple)
            {
                // we only handle the case (e1, e2, .., _, _)  = funcCall(...)
                lhs.AddRange(tuple.Components.ConvertAll(x => TranslateExpr(x)));
                isTupleAssignment = true;
                lhsTypes.AddRange(tuple.Components.ConvertAll(x => MapArrayHelper.InferExprTypeFromTypeString(x.TypeDescriptions.TypeString)));
            }
            else
            {
                lhs.Add(TranslateExpr(node.LeftHandSide));
            }
         
            if (node.RightHandSide is FunctionCall funcCall)
            {
                // if lhs is not an identifier (e.g. a[i]), then
                // we have to introduce a temporary
                // we do it even when lhs is identifier to keep translation simple
                var tmpVars = new List<BoogieIdentifierExpr>();

                if (!isTupleAssignment) {
                    tmpVars.Add(lhs[0] is BoogieIdentifierExpr ? lhs[0] as BoogieIdentifierExpr : MkNewLocalVariableForFunctionReturn(funcCall));
                } else {
                    // always use temporaries for tuples regardless if lhs[i] is an identifier
                    tmpVars.AddRange(lhsTypes.ConvertAll(x => MkNewLocalVariableWithType(x)));
                }

                // a Boolean to decide is we needed to use tmpVar
                bool usedTmpVar = true;

                if (IsContractConstructor(funcCall))
                {
                    VeriSolAssert(!isTupleAssignment, "Not expecting a tuple for Constructors");
                    TranslateNewStatement(funcCall, tmpVars[0]);
                }
                else if (IsStructConstructor(funcCall))
                {
                    VeriSolAssert(!isTupleAssignment, "Not expecting a tuple for Constructors");
                    TranslateStructConstructor(funcCall, tmpVars[0]);
                }
                else if (IsKeccakFunc(funcCall))
                {
                    VeriSolAssert(!isTupleAssignment, "Not expecting a tuple for Keccak256");
                    TranslateKeccakFuncCall(funcCall, lhs[0]); //this is not a procedure call in Boogie
                    usedTmpVar = false;
                }
                else if (IsAbiEncodePackedFunc(funcCall))
                {
                    TranslateAbiEncodedFuncCall(funcCall, tmpVars[0]); //this is not a procedure call in Boogie
                    VeriSolAssert(!isTupleAssignment, "Not expecting a tuple for abi.encodePacked");
                    usedTmpVar = false;
                }
                else if (IsTypeCast(funcCall))
                {
                    // assume the type cast is used as: obj = C(var);
                    VeriSolAssert(!isTupleAssignment, "Not expecting a tuple for type cast");
                    TranslateTypeCast(funcCall, tmpVars[0]); //this is not a procedure call in Boogie
                    usedTmpVar = false;
                }
                else // normal function calls
                {
                    VeriSolAssert(tmpVars is List<BoogieIdentifierExpr>, $"tmpVar has to be a list of Boogie identifiers: {tmpVars}");
                    TranslateFunctionCalls(funcCall, tmpVars);
                }
                if (!isTupleAssignment)
                {
                    if (usedTmpVar && !(lhs[0] is BoogieIdentifierExpr))
                        currentStmtList.AddStatement(new BoogieAssignCmd(lhs[0], tmpVars[0]));
                } else
                {
                    for (int i = 0; i < lhs.Count; ++i)
                    {
                        currentStmtList.AddStatement(new BoogieAssignCmd(lhs[i], tmpVars[i]));
                    }
                }
            }
            else
            {
                if (isTupleAssignment)
                    throw new NotImplementedException("Currently only support assignment of tuples as returns of a function call");

                BoogieExpr rhs = TranslateExpr(node.RightHandSide);
                BoogieStmtList stmtList = new BoogieStmtList();
                switch (node.Operator)
                {
                    case "=":
                        stmtList.AddStatement(new BoogieAssignCmd(lhs[0], rhs));
                        break;
                    case "+=":
                        stmtList.AddStatement(new BoogieAssignCmd(lhs[0], new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, lhs[0], rhs)));
                        break;
                    case "-=":
                        stmtList.AddStatement(new BoogieAssignCmd(lhs[0], new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, lhs[0], rhs)));
                        break;
                    case "*=":
                        stmtList.AddStatement(new BoogieAssignCmd(lhs[0], new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.MUL, lhs[0], rhs)));
                        break;
                    case "/=":
                        stmtList.AddStatement(new BoogieAssignCmd(lhs[0], new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.DIV, lhs[0], rhs)));
                        break;
                    default:
                        throw new SystemException($"Unknown assignment operator: {node.Operator}");
                }
                currentStmtList.AppendStmtList(stmtList);
            }

            var lhsType = node.LeftHandSide.TypeDescriptions != null ?
                node.LeftHandSide.TypeDescriptions :
                (node.RightHandSide.TypeDescriptions != null ?
                    node.RightHandSide.TypeDescriptions : null);

            if (lhsType != null && !isTupleAssignment)
            {
                //REFACTOR!
                if (lhsType.TypeString.StartsWith("uint") || lhsType.TypeString.StartsWith("int") || lhsType.TypeString.StartsWith("string ") || lhsType.TypeString.StartsWith("bytes"))
                {
                    var callCmd = new BoogieCallCmd("boogie_si_record_sol2Bpl_int", new List<BoogieExpr>() { lhs[0] }, new List<BoogieIdentifierExpr>());
                    callCmd.Attributes = new List<BoogieAttribute>();
                    callCmd.Attributes.Add(new BoogieAttribute("cexpr", $"\"{node.LeftHandSide.ToString()}\""));
                    currentStmtList.AddStatement(callCmd);
                }
                if (lhsType.TypeString.Equals("address"))
                {
                    var callCmd = new BoogieCallCmd("boogie_si_record_sol2Bpl_ref", new List<BoogieExpr>() { lhs[0] }, new List<BoogieIdentifierExpr>());
                    callCmd.Attributes = new List<BoogieAttribute>();
                    callCmd.Attributes.Add(new BoogieAttribute("cexpr", $"\"{node.LeftHandSide.ToString()}\""));
                    currentStmtList.AddStatement(callCmd);
                }
                if (lhsType.TypeString.Equals("bool"))
                {
                    var callCmd = new BoogieCallCmd("boogie_si_record_sol2Bpl_bool", new List<BoogieExpr>() { lhs[0] }, new List<BoogieIdentifierExpr>());
                    callCmd.Attributes = new List<BoogieAttribute>();
                    callCmd.Attributes.Add(new BoogieAttribute("cexpr", $"\"{node.LeftHandSide.ToString()}\""));
                    currentStmtList.AddStatement(callCmd);
                }
            }



            return false;
        }

        private void TranslateInBuiltFunction(FunctionCall funcCall, BoogieExpr lhs)
        {
            throw new NotImplementedException();
        }

        private void TranslateFunctionCalls(FunctionCall funcCall, List<BoogieIdentifierExpr> outParams)
        {
            if (IsExternalFunctionCall(funcCall))
            {
                TranslateExternalFunctionCall(funcCall, outParams);
            }
            else
            {
                TranslateInternalFunctionCall(funcCall, outParams);
            }
        }

        public override bool Visit(Return node)
        {
            if (node.Expression == null)
            {
                //Void
                BoogieReturnCmd returnCmd = new BoogieReturnCmd();
                if (currentPostlude == null)
                {
                    currentStmtList.AppendStmtList(BoogieStmtList.MakeSingletonStmtList(returnCmd));
                }
                else
                {
                    currentStmtList.AppendStmtList(currentPostlude);
                    currentStmtList.AddStatement(returnCmd);
                }
            }
            else
            {
                BoogieExpr retExpr = TranslateExpr(node.Expression); //TODO: handle tuples here?
                var retParamCount = 0;
                if (node.Expression is TupleExpression tuple)
                {
                    //Tuple
                    if (!(retExpr is BoogieTupleExpr))
                    {
                        VeriSolAssert(false, "Expecting a Boogie tuple expression here");
                    }
                    var bTupleExpr = retExpr as BoogieTupleExpr;

                    //turn the tuple assignment into a serial assignment [TODO: understand the evaluation semantics]
                    foreach (var retVarDecl in currentFunction.ReturnParameters.Parameters)
                    {
                        string retVarName = String.IsNullOrEmpty(retVarDecl.Name) ?
                            $"__ret_{retParamCount}_" :
                            TransUtils.GetCanonicalLocalVariableName(retVarDecl);
                        BoogieIdentifierExpr retVar = new BoogieIdentifierExpr(retVarName);
                        BoogieAssignCmd assignCmd = new BoogieAssignCmd(retVar, bTupleExpr.Arguments[retParamCount++]);
                        currentStmtList.AppendStmtList(BoogieStmtList.MakeSingletonStmtList(assignCmd)); //TODO: simultaneous updates
                    }
                }
                else
                {
                    //Singleton 
                    var retVarDecl = currentFunction.ReturnParameters.Parameters[0];
                    string retVarName = String.IsNullOrEmpty(retVarDecl.Name) ?
                        $"__ret_{retParamCount++}_" :
                        TransUtils.GetCanonicalLocalVariableName(retVarDecl);
                    BoogieIdentifierExpr retVar = new BoogieIdentifierExpr(retVarName);
                    BoogieAssignCmd assignCmd = new BoogieAssignCmd(retVar, retExpr);
                    currentStmtList.AppendStmtList(BoogieStmtList.MakeSingletonStmtList(assignCmd)); //TODO: simultaneous updates
                }

                if (currentPostlude != null)
                {
                    currentStmtList.AppendStmtList(currentPostlude);
                }
                // add a return command, in case the original return expr is in the middle of the function body
                currentStmtList.AddStatement(new BoogieReturnCmd());
            }
            return false;
        }

        public override bool Visit(Throw node)
        {
            BoogieAssumeCmd assumeCmd = new BoogieAssumeCmd(new BoogieLiteralExpr(false));
            currentStmtList.AppendStmtList(BoogieStmtList.MakeSingletonStmtList(assumeCmd));
            return false;
        }

        public override bool Visit(IfStatement node)
        {
            BoogieExpr guard = TranslateExpr(node.Condition);
            BoogieStmtList thenBody = TranslateStatement(node.TrueBody);
            BoogieStmtList elseBody = null;
            if (node.FalseBody != null)
            {
                elseBody = TranslateStatement(node.FalseBody);
            }
            BoogieIfCmd ifCmd = new BoogieIfCmd(guard, thenBody, elseBody);

            //currentStmtList = new BoogieStmtList();
            //currentStmtList.AppendStmtList(auxStmtList);
            currentStmtList.AddStatement(ifCmd);
            return false;
        }

        public override bool Visit(WhileStatement node)
        {
            BoogieExpr guard = TranslateExpr(node.Condition);
            BoogieStmtList body = TranslateStatement(node.Body);

            BoogieWhileCmd whileCmd = new BoogieWhileCmd(guard, body);

            currentStmtList.AddStatement(whileCmd);
            return false;
        }

        public override bool Visit(ForStatement node)
        {
            BoogieStmtList initStmt = TranslateStatement(node.InitializationExpression);
            BoogieExpr guard = TranslateExpr(node.Condition);
            BoogieStmtList loopStmt = TranslateStatement(node.LoopExpression);
            BoogieStmtList body = TranslateStatement(node.Body);

            BoogieStmtList stmtList = new BoogieStmtList();
            stmtList.AppendStmtList(initStmt);

            body.AppendStmtList(loopStmt);
            BoogieWhileCmd whileCmd = new BoogieWhileCmd(guard, body);
            stmtList.AddStatement(whileCmd);

            currentStmtList.AppendStmtList(stmtList);
            return false;
        }

        public override bool Visit(DoWhileStatement node)
        {
            BoogieExpr guard = TranslateExpr(node.Condition);
            BoogieStmtList body = TranslateStatement(node.Body);

            BoogieStmtList stmtList = new BoogieStmtList();
            stmtList.AppendStmtList(body);

            BoogieWhileCmd whileCmd = new BoogieWhileCmd(guard, body);
            stmtList.AddStatement(whileCmd);

            currentStmtList.AppendStmtList(stmtList);
            return false;
        }

        public override bool Visit(Break node)
        {
            BoogieBreakCmd breakCmd = new BoogieBreakCmd();
            currentStmtList.AddStatement(breakCmd);
            return false;
        }

        public override bool Visit(Continue node)
        {
            throw new NotImplementedException(node.ToString());
        }

        public override bool Visit(ExpressionStatement node)
        {
            if (node.Expression is UnaryOperation unaryOperation)
            {
                // only handle increment and decrement operators in a separate statement
                VeriSolAssert(!(unaryOperation.SubExpression is UnaryOperation));

                BoogieExpr lhs = TranslateExpr(unaryOperation.SubExpression);
                if (unaryOperation.Operator.Equals("++"))
                {
                    BoogieExpr rhs = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, lhs, new BoogieLiteralExpr(1));
                    BoogieAssignCmd assignCmd = new BoogieAssignCmd(lhs, rhs);
                    currentStmtList = BoogieStmtList.MakeSingletonStmtList(assignCmd);
                    //print the value
                    var callCmd = new BoogieCallCmd("boogie_si_record_sol2Bpl_int", new List<BoogieExpr>() { lhs }, new List<BoogieIdentifierExpr>());
                    callCmd.Attributes = new List<BoogieAttribute>();
                    callCmd.Attributes.Add(new BoogieAttribute("cexpr", $"\"{unaryOperation.SubExpression.ToString()}\""));
                    currentStmtList.AddStatement(callCmd);
                }
                else if (unaryOperation.Operator.Equals("--"))
                {
                    BoogieExpr rhs = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, lhs, new BoogieLiteralExpr(1));
                    BoogieAssignCmd assignCmd = new BoogieAssignCmd(lhs, rhs);
                    currentStmtList = BoogieStmtList.MakeSingletonStmtList(assignCmd);
                    //print the value
                    var callCmd = new BoogieCallCmd("boogie_si_record_sol2Bpl_int", new List<BoogieExpr>() { lhs }, new List<BoogieIdentifierExpr>());
                    callCmd.Attributes = new List<BoogieAttribute>();
                    callCmd.Attributes.Add(new BoogieAttribute("cexpr", $"\"{unaryOperation.SubExpression.ToString()}\""));
                    currentStmtList.AddStatement(callCmd);
                } 
                else if (unaryOperation.Operator.Equals("delete"))
                {
                    var typeStr = unaryOperation.SubExpression.TypeDescriptions.TypeString;
                    if (typeStr.StartsWith("int") || typeStr.StartsWith("uint") || typeStr.Equals("bool") || typeStr.StartsWith("string "))
                    {
                        //REFACTOR to define isInt, isUint, IsByte, IsString etc.
                        BoogieExpr rhs = null;
                        if (typeStr.StartsWith("int") || typeStr.StartsWith("uint"))
                            rhs = new BoogieLiteralExpr(BigInteger.Zero);
                        else if (typeStr.Equals("bool"))
                            rhs = new BoogieLiteralExpr(false);
                        else if (typeStr.StartsWith("string"))
                        {
                            var emptyStr = "";
                            rhs = new BoogieLiteralExpr(new BigInteger(emptyStr.GetHashCode()));
                        }
                        var assignCmd = new BoogieAssignCmd(lhs, rhs);
                        currentStmtList.AddStatement(assignCmd);
                    }
                    else
                    {
                        Console.WriteLine($"Warning!!: Only handle delete for scalars, found {typeStr}");
                    }
                }
                return false;
            }
            else
            {
                // distribute to different visit functions
                return true;
            }
        }

        // updated in visitors of different expressions
        private BoogieExpr currentExpr;

        private BoogieExpr TranslateExpr(Expression expr)
        {
            currentExpr = null;
            if (expr is FunctionCall && IsTypeCast((FunctionCall) expr))
            {
                expr.Accept(this);
                VeriSolAssert(currentExpr != null);
                // TranslateTypeCast()
                //if (((FunctionCall) expr).Expression is ElementaryTypeNameExpression)
                //{
                //    currentExpr = TranslateExpr(((FunctionCall) expr).Arguments[0]);
                //}
                //else
                //{
                //    throw new NotImplementedException("Cannot handle non-elementary type cast");
                //}
            }
            else if(expr is TupleExpression tuple)
            {
                var transArgs = new List<BoogieExpr>();
                foreach(var e in tuple.Components)
                {
                    e.Accept(this);
                    VeriSolAssert(currentExpr != null);
                    transArgs.Add(currentExpr);
                }
                currentExpr = new BoogieTupleExpr(transArgs);
            }
            else
            {
                expr.Accept(this);
                VeriSolAssert(currentExpr != null);
            }
            return currentExpr;
        }

        public override bool Visit(Literal node)
        {
            if (node.Kind.Equals("bool"))
            {
                bool b = Convert.ToBoolean(node.Value);
                currentExpr = new BoogieLiteralExpr(b);
            }
            else if (node.Kind.Equals("number"))
            {
                currentExpr = TranslateNumberToExpr(node);
            }
            else if (node.Kind.Equals("string"))
            {
                int hashCode = node.Value.GetHashCode();
                BigInteger num = new BigInteger(hashCode);
                currentExpr = new BoogieLiteralExpr(num);
            }
            else
            {
                throw new SystemException($"Unknown literal kind: {node.Kind}");
            }
            return false;
        }

        private BoogieExpr TranslateNumberToExpr(Literal node)
        {
            // assumption: numbers statrting in 0x are addresses
            if (node.Value.StartsWith("0x") || node.Value.StartsWith("0X"))
            {
                BigInteger num = BigInteger.Parse(node.Value.Substring(2), NumberStyles.AllowHexSpecifier);
                if (num == BigInteger.Zero)
                {
                    return new BoogieIdentifierExpr("null");
                }
                else
                {
                    return new BoogieFuncCallExpr("ConstantToRef", new List<BoogieExpr>() { new BoogieLiteralExpr(num) });
                }
            }
            else
            {
                BigInteger num = BigInteger.Parse(node.Value);
                return new BoogieLiteralExpr(num);
            }
        }

        public override bool Visit(Identifier node)
        {
            if (node.Name.Equals("this"))
            {
                currentExpr = new BoogieIdentifierExpr("this");
            }
            else // explicitly defined identifiers
            {
                VeriSolAssert(context.HasASTNodeId(node.ReferencedDeclaration), $"Unknown node: {node}");
                VariableDeclaration varDecl = context.GetASTNodeById(node.ReferencedDeclaration) as VariableDeclaration;
                VeriSolAssert(varDecl != null);

                if (varDecl.StateVariable)
                {
                    string name = TransUtils.GetCanonicalStateVariableName(varDecl, context);

                    BoogieIdentifierExpr mapIdentifier = new BoogieIdentifierExpr(name);
                    BoogieMapSelect mapSelect = new BoogieMapSelect(mapIdentifier, new BoogieIdentifierExpr("this"));
                    currentExpr = mapSelect;
                }
                else
                {
                    string name = TransUtils.GetCanonicalLocalVariableName(varDecl);
                    BoogieIdentifierExpr identifier = new BoogieIdentifierExpr(name);
                    currentExpr = identifier;
                }
            }
            return false;
        }

        public override bool Visit(MemberAccess node)
        {
            // length attribute of arrays
            if (node.MemberName.Equals("length"))
            {
                currentExpr = TranslateArrayLength(node);
                return false;
            }

            // only structs will need to use x.f.g notation, since 
            // one can only access functions of nested contracts
            // RESTRICTION: only handle e.f where e is Identifier | IndexExpr | FunctionCall
            VeriSolAssert(node.Expression is Identifier || node.Expression is IndexAccess || node.Expression is FunctionCall,
                $"Only handle non-nested structures, found {node.Expression.ToString()}");
            if (node.Expression.TypeDescriptions.TypeString.StartsWith("struct "))
            {
                var baseExpr = TranslateExpr(node.Expression);
                var memberMap = node.MemberName + "_" + node.Expression.TypeDescriptions.TypeString.Split(" ")[1];
                currentExpr = new BoogieMapSelect(
                    new BoogieIdentifierExpr(memberMap),
                    baseExpr);
                return false;
            }


            if (node.Expression is Identifier)
            {
                Identifier identifier = node.Expression as Identifier;
                if (identifier.Name.Equals("msg"))
                {
                    if (node.MemberName.Equals("sender"))
                    {
                        currentExpr = new BoogieIdentifierExpr("msgsender_MSG");
                    }
                    else if (node.MemberName.Equals("value"))
                    {
                        currentExpr = new BoogieIdentifierExpr("msgvalue_MSG");
                    }
                    else
                    {
                        throw new SystemException($"Unknown member for msg: {node}");
                    }
                    return false;
                }
                else if (identifier.Name.Equals("this"))
                {
                    if (node.MemberName.Equals("balance"))
                    {
                        currentExpr = new BoogieMapSelect(new BoogieIdentifierExpr("balance_ADDR"), new BoogieIdentifierExpr("this"));
                    }
                    return false;
                }

                VeriSolAssert(context.HasASTNodeId(identifier.ReferencedDeclaration), $"Unknown node: {identifier}");
                ASTNode refDecl = context.GetASTNodeById(identifier.ReferencedDeclaration);

                if (refDecl is EnumDefinition)
                {
                    int enumIndex = TransUtils.GetEnumValueIndex((EnumDefinition)refDecl, node.MemberName);
                    currentExpr = new BoogieLiteralExpr(enumIndex);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                throw new SystemException($"Unknown expression type for member access: {node}");
            }
        }

        private BoogieExpr TranslateArrayLength(MemberAccess node)
        {
            VeriSolAssert(node.MemberName.Equals("length"));

            BoogieExpr indexExpr = TranslateExpr(node.Expression);
            var mapSelect = new BoogieMapSelect(new BoogieIdentifierExpr("Length"), indexExpr);
            return mapSelect;
        }

        public override bool Visit(FunctionCall node)
        {
            // VeriSolAssert(!(node.Expression is NewExpression), $"new expressions should be handled in assignment");
            if (node.Expression is NewExpression)
            {
                BoogieIdentifierExpr tmpVarExpr = MkNewLocalVariableForFunctionReturn(node);
                TranslateNewStatement(node, tmpVarExpr);
                currentExpr = tmpVarExpr;
                return false;
            }

            var functionName = TransUtils.GetFuncNameFromFunctionCall(node);

            if (functionName.Equals("assert"))
            {
                VeriSolAssert(node.Arguments.Count == 1);
                BoogieExpr predicate = TranslateExpr(node.Arguments[0]);
                BoogieAssertCmd assertCmd = new BoogieAssertCmd(predicate);
                currentStmtList.AddStatement(assertCmd);
            }
            else if (functionName.Equals("require"))
            {
                VeriSolAssert(node.Arguments.Count == 1 || node.Arguments.Count == 2);
                BoogieExpr predicate = TranslateExpr(node.Arguments[0]);
                BoogieAssumeCmd assumeCmd = new BoogieAssumeCmd(predicate);
                currentStmtList.AddStatement(assumeCmd);
            }
            else if (functionName.Equals("revert"))
            {
                VeriSolAssert(node.Arguments.Count == 0 || node.Arguments.Count == 1);
                BoogieAssumeCmd assumeCmd = new BoogieAssumeCmd(new BoogieLiteralExpr(false));
                currentStmtList.AddStatement(assumeCmd);
            }
            else if (IsImplicitFunc(node))
            {
                BoogieIdentifierExpr tmpVarExpr = MkNewLocalVariableForFunctionReturn(node);

                if (IsContractConstructor(node)) {
                    TranslateNewStatement(node, tmpVarExpr);
                } else if (IsTypeCast(node)) {
                    TranslateTypeCast(node, tmpVarExpr);
                } else if (IsAbiEncodePackedFunc(node)) {
                    TranslateAbiEncodedFuncCall(node, tmpVarExpr);
                } else if (IsKeccakFunc(node)) {
                    TranslateKeccakFuncCall(node, tmpVarExpr);
                } else if (IsStructConstructor(node)) {
                    TranslateStructConstructor(node, tmpVarExpr);
                } else
                {
                    VeriSolAssert(false, $"Unexpected implicit function {node.ToString()}");
                }

                currentExpr = tmpVarExpr;
            }
            else if (context.HasEventNameInContract(currentContract, functionName))
            {
                // generate empty statement list to ignore the event call                
            }
            else if (functionName.Equals("call"))
            {
                TranslateCallStatement(node);
            }
            else if (functionName.Equals("send") || functionName.Equals("delegatecall"))
            {
                throw new NotImplementedException(functionName);
            }
            else if (IsDynamicArrayPush(node))
            {
                TranslateDynamicArrayPush(node);
            }
            else if (IsExternalFunctionCall(node))
            {
                // external function calls

                // HACK: this is the way to identify the return type is void and hence don't need temporary variable
                if (node.TypeDescriptions.TypeString != "tuple()")
                {
                    var tmpVarExpr = MkNewLocalVariableForFunctionReturn(node);
                    var outParams = new List<BoogieIdentifierExpr>() { tmpVarExpr };
                    TranslateExternalFunctionCall(node, outParams);
                    currentExpr = tmpVarExpr;
                }
                else
                {
                    TranslateExternalFunctionCall(node);
                }
            }
            else // internal function calls
            {
                // HACK: this is the way to identify the return type is void and hence don't need temporary variable
                if (node.TypeDescriptions.TypeString != "tuple()")
                {
                    // internal function calls
                    var tmpVarExpr = MkNewLocalVariableForFunctionReturn(node);
                    var outParams = new List<BoogieIdentifierExpr>() { tmpVarExpr };
                    TranslateInternalFunctionCall(node, outParams);
                    currentExpr = tmpVarExpr;
                }
                else
                {
                    TranslateInternalFunctionCall(node);
                }

            }
            return false;
        }

        private BoogieIdentifierExpr MkNewLocalVariableForFunctionReturn(FunctionCall node)
        {
            var boogieTypeCall = MapArrayHelper.InferExprTypeFromTypeString(node.TypeDescriptions.TypeString);
            return MkNewLocalVariableWithType(boogieTypeCall);
        }

        private BoogieIdentifierExpr MkNewLocalVariableWithType(BoogieType boogieTypeCall)
        {
            var tmpVar = new BoogieLocalVariable(context.MakeFreshTypedIdent(boogieTypeCall));
            boogieToLocalVarsMap[currentBoogieProc].Add(tmpVar);

            var tmpVarExpr = new BoogieIdentifierExpr(tmpVar.Name);
            return tmpVarExpr;
        }

        #region implicit functions 
        /// <summary>
        /// Implicit function calls
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsImplicitFunc(FunctionCall node)
        {
            return
                IsKeccakFunc(node) ||
                IsAbiEncodePackedFunc(node) ||
                IsTypeCast(node) ||
                IsStructConstructor(node) ||
                IsContractConstructor(node);
         }

        private bool IsContractConstructor(FunctionCall node)
        {
            return node.Expression is NewExpression;
        }

        private bool IsStructConstructor(FunctionCall node)
        {
            return node.Kind.Equals("structConstructorCall");
        }

        private bool IsKeccakFunc(FunctionCall node)
        {
            if (node.Expression is Identifier ident)
            {
                return ident.Name.Equals("keccak256");
            }
            return false;
        }

        private bool IsAbiEncodePackedFunc(FunctionCall node)
        {
            if (node.Expression is MemberAccess member)
            {
                if (member.Expression is Identifier ident)
                {
                    if (ident.Name.Equals("abi"))
                    {
                        if (member.MemberName.Equals("encodePacked"))
                            return true;
                    }
                }
            }
            return false;
        }

        private void TranslateKeccakFuncCall(FunctionCall funcCall, BoogieExpr lhs)
        {
            var expression = funcCall.Arguments[0];
            var boogieExpr = TranslateExpr(expression);
            var keccakExpr = new BoogieFuncCallExpr("keccak256", new List<BoogieExpr>() { boogieExpr });
            currentStmtList.AddStatement(new BoogieAssignCmd(lhs, keccakExpr));
            return;
        }

        private void TranslateAbiEncodedFuncCall(FunctionCall funcCall, BoogieExpr lhs)
        {
            var arguments = funcCall.Arguments;
            if (arguments.Count > 2)
            {
                throw new NotImplementedException($"Variable argument function abi.encodePacked(...) currently supported only for 1 or 2 arguments, encountered  {arguments.Count} arguments");
            }
            var boogieExprs = arguments.ConvertAll(x => TranslateExpr(x));
            var funcName = $"abiEncodePacked{arguments.Count}";
            var abiEncodeFuncCall = new BoogieFuncCallExpr(funcName, boogieExprs);
            currentStmtList.AddStatement(new BoogieAssignCmd(lhs, abiEncodeFuncCall));
            return;
        }

        private void TranslateCallStatement(FunctionCall node)
        {
            currentStmtList.AddStatement(new BoogieSkipCmd(node.ToString()));
            throw new NotImplementedException();
        }

        private void TranslateNewStatement(FunctionCall node, BoogieExpr lhs)
        {
            VeriSolAssert(node.Expression is NewExpression);
            NewExpression newExpr = node.Expression as NewExpression;
            VeriSolAssert(newExpr.TypeName is UserDefinedTypeName);
            UserDefinedTypeName udt = newExpr.TypeName as UserDefinedTypeName;

            ContractDefinition contract = context.GetASTNodeById(udt.ReferencedDeclaration) as ContractDefinition;
            VeriSolAssert(contract != null);

            // define a local variable to temporarily hold the object
            BoogieTypedIdent freshAllocTmpId = context.MakeFreshTypedIdent(BoogieType.Ref);
            BoogieLocalVariable allocTmpVar = new BoogieLocalVariable(freshAllocTmpId);
            boogieToLocalVarsMap[currentBoogieProc].Add(allocTmpVar);

            // define a local variable to store the new msg.value
            BoogieTypedIdent freshMsgValueId = context.MakeFreshTypedIdent(BoogieType.Int);
            BoogieLocalVariable msgValueVar = new BoogieLocalVariable(freshMsgValueId);
            boogieToLocalVarsMap[currentBoogieProc].Add(msgValueVar);

            BoogieIdentifierExpr tmpVarIdentExpr = new BoogieIdentifierExpr(freshAllocTmpId.Name);
            BoogieIdentifierExpr msgValueIdentExpr = new BoogieIdentifierExpr(freshMsgValueId.Name);
            BoogieIdentifierExpr allocIdentExpr = new BoogieIdentifierExpr("Alloc");

            // suppose the statement is lhs := new A(args);

            // call tmp := FreshRefGenerator();
            currentStmtList.AddStatement(
                new BoogieCallCmd(
                    "FreshRefGenerator",
                    new List<BoogieExpr>(),
                    new List<BoogieIdentifierExpr>() { tmpVarIdentExpr }
                    ));

            // call constructor of A with this = tmp, msg.sender = this, msg.value = tmpMsgValue, args
            string callee = TransUtils.GetCanonicalConstructorName(contract);
            List<BoogieExpr> inputs = new List<BoogieExpr>()
            {
                tmpVarIdentExpr,
                new BoogieIdentifierExpr("this"),
                msgValueIdentExpr,
            };
            foreach (Expression arg in node.Arguments)
            {
                BoogieExpr argument = TranslateExpr(arg);
                inputs.Add(argument);
            }
            // assume DType[tmp] == A
            BoogieMapSelect dtypeMapSelect = new BoogieMapSelect(new BoogieIdentifierExpr("DType"), tmpVarIdentExpr);
            BoogieIdentifierExpr contractIdent = new BoogieIdentifierExpr(contract.Name);
            BoogieExpr dtypeAssumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, dtypeMapSelect, contractIdent);
            currentStmtList.AddStatement(new BoogieAssumeCmd(dtypeAssumeExpr));
            // The assume DType[tmp] == A is before the call as the constructor may do a dynamic 
            // dispatch and the DType[tmp] is unconstrained before the call
            List<BoogieIdentifierExpr> outputs = new List<BoogieIdentifierExpr>();
            currentStmtList.AddStatement(new BoogieCallCmd(callee, inputs, outputs));
            // lhs := tmp;
            currentStmtList.AddStatement(new BoogieAssignCmd(lhs, tmpVarIdentExpr));
            return;
        }

        private void TranslateStructConstructor(FunctionCall node, BoogieExpr lhs)
        {
            var structString = node.TypeDescriptions.TypeString.Split(' ')[1];

            // define a local variable to temporarily hold the object
            BoogieTypedIdent freshAllocTmpId = context.MakeFreshTypedIdent(BoogieType.Ref);
            BoogieLocalVariable allocTmpVar = new BoogieLocalVariable(freshAllocTmpId);
            boogieToLocalVarsMap[currentBoogieProc].Add(allocTmpVar);

            // define a local variable to store the new msg.value
            BoogieTypedIdent freshMsgValueId = context.MakeFreshTypedIdent(BoogieType.Int);
            BoogieLocalVariable msgValueVar = new BoogieLocalVariable(freshMsgValueId);
            boogieToLocalVarsMap[currentBoogieProc].Add(msgValueVar);

            BoogieIdentifierExpr tmpVarIdentExpr = new BoogieIdentifierExpr(freshAllocTmpId.Name);
            BoogieIdentifierExpr msgValueIdentExpr = new BoogieIdentifierExpr(freshMsgValueId.Name);
            BoogieIdentifierExpr allocIdentExpr = new BoogieIdentifierExpr("Alloc");

            // suppose the statement is lhs := new A(args);

            // call tmp := FreshRefGenerator();
            currentStmtList.AddStatement(
                new BoogieCallCmd(
                    "FreshRefGenerator",
                    new List<BoogieExpr>(),
                    new List<BoogieIdentifierExpr>() { tmpVarIdentExpr }
                    ));

            // call constructor of A with this = tmp, msg.sender = this, msg.value = tmpMsgValue, args
            string callee = structString + "_ctor"; // TransUtils.GetCanonicalConstructorName(contract);
            List<BoogieExpr> inputs = new List<BoogieExpr>()
            {
                tmpVarIdentExpr,
                new BoogieIdentifierExpr("this"),
                msgValueIdentExpr,
            };
            foreach (Expression arg in node.Arguments)
            {
                BoogieExpr argument = TranslateExpr(arg);
                inputs.Add(argument);
            }
            // assume DType[tmp] == A
            BoogieMapSelect dtypeMapSelect = new BoogieMapSelect(new BoogieIdentifierExpr("DType"), tmpVarIdentExpr);
            BoogieIdentifierExpr contractIdent = new BoogieIdentifierExpr(structString); // contract.Name);
            BoogieExpr dtypeAssumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, dtypeMapSelect, contractIdent);
            currentStmtList.AddStatement(new BoogieAssumeCmd(dtypeAssumeExpr));
            // The assume DType[tmp] == A is before the call as the constructor may do a dynamic 
            // dispatch and the DType[tmp] is unconstrained before the call
            List<BoogieIdentifierExpr> outputs = new List<BoogieIdentifierExpr>();
            currentStmtList.AddStatement(new BoogieCallCmd(callee, inputs, outputs));
            // lhs := tmp;
            currentStmtList.AddStatement(new BoogieAssignCmd(lhs, tmpVarIdentExpr));
            return;
        }

        private bool IsDynamicArrayPush(FunctionCall node)
        {
            string functionName = TransUtils.GetFuncNameFromFunctionCall(node);
            if (functionName.Equals("push"))
            {
                VeriSolAssert(node.Expression is MemberAccess);
                MemberAccess memberAccess = node.Expression as MemberAccess;
                return MapArrayHelper.IsArrayTypeString(memberAccess.Expression.TypeDescriptions.TypeString);
            }
            return false;
        }

        private void TranslateDynamicArrayPush(FunctionCall node)
        {
            VeriSolAssert(node.Expression is MemberAccess);
            VeriSolAssert(node.Arguments.Count == 1);

            MemberAccess memberAccess = node.Expression as MemberAccess;
            BoogieExpr receiver = TranslateExpr(memberAccess.Expression);
            BoogieExpr element = TranslateExpr(node.Arguments[0]);

            BoogieExpr lengthMapSelect = new BoogieMapSelect(new BoogieIdentifierExpr("Length"), receiver);
            // suppose the form is a.push(e)
            // tmp := Length[this][a];
            BoogieTypedIdent tmpIdent = context.MakeFreshTypedIdent(BoogieType.Int);
            boogieToLocalVarsMap[currentBoogieProc].Add(new BoogieLocalVariable(tmpIdent));
            BoogieIdentifierExpr tmp = new BoogieIdentifierExpr(tmpIdent.Name);
            BoogieAssignCmd assignCmd = new BoogieAssignCmd(tmp, lengthMapSelect);
            currentStmtList.AddStatement(assignCmd);

            // M[this][a][tmp] := e;
            BoogieType mapKeyType = BoogieType.Int;
            BoogieType mapValType = MapArrayHelper.InferExprTypeFromTypeString(node.Arguments[0].TypeDescriptions.TypeString);
            BoogieExpr mapSelect = MapArrayHelper.GetMemoryMapSelectExpr(mapKeyType, mapValType, receiver, tmp);
            BoogieAssignCmd writeCmd = new BoogieAssignCmd(mapSelect, element);
            currentStmtList.AddStatement(writeCmd);

            // Length[this][a] := tmp + 1;
            BoogieExpr rhs = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, tmp, new BoogieLiteralExpr(1));
            BoogieAssignCmd updateLengthCmd = new BoogieAssignCmd(lengthMapSelect, rhs);
            currentStmtList.AddStatement(updateLengthCmd);
            return;
        }
        #endregion

        private bool IsExternalFunctionCall(FunctionCall node)
        {
            if (node.Expression is MemberAccess memberAccess)
            {
                if (memberAccess.Expression is Identifier identifier)
                {
                    if (identifier.Name == "this")
                    {
                        return true;
                    }
                    var contract = context.GetASTNodeById(identifier.ReferencedDeclaration) as ContractDefinition;
                    if (contract == null)
                    {
                        return true;
                    }
                } else if (memberAccess.Expression is FunctionCall)
                {
                    return true;
                } else if (memberAccess.Expression is IndexAccess)
                {
                    //a[i].foo(..)
                    return true;
                }
            }
            return false;
        }

        private ContractDefinition IsLibraryFunctionCall(FunctionCall node)
        {
            if (node.Expression is MemberAccess memberAccess)
            {
                if (memberAccess.Expression is Identifier identifier)
                {
                    var contract = context.GetASTNodeById(identifier.ReferencedDeclaration) as ContractDefinition;
                    // a Library is treated as an external function call
                    // we need to do it here as the Lib.Foo, Lib is not an expression but name of a contract
                    if (contract.ContractKind == EnumContractKind.LIBRARY)
                    {
                        return contract;
                    }
                }
            }
            return null;
        }

        private void TranslateExternalFunctionCall(FunctionCall node, List<BoogieIdentifierExpr> outParams = null)
        {
            VeriSolAssert(node.Expression is MemberAccess, $"Expecting a member access expression here {node.Expression.ToString()}");
            MemberAccess memberAccess = node.Expression as MemberAccess;
            BoogieExpr receiver = TranslateExpr(memberAccess.Expression);
            BoogieTypedIdent msgValueId = context.MakeFreshTypedIdent(BoogieType.Int);
            BoogieLocalVariable msgValueVar = new BoogieLocalVariable(msgValueId);
            boogieToLocalVarsMap[currentBoogieProc].Add(msgValueVar);

            List<BoogieExpr> arguments = new List<BoogieExpr>()
            {
                receiver,
                new BoogieIdentifierExpr("this"),
                new BoogieIdentifierExpr(msgValueId.Name),
            };

            foreach (Expression arg in node.Arguments)
            {
                BoogieExpr argument = TranslateExpr(arg);
                arguments.Add(argument);
            }

            // TODO: we need a way to determine type of receiver from "x.Foo()"
            // This additional condition is checked in the loop at this call site
            // and was the reason why the code was not abstracted into a single call
            var guard = memberAccess.Expression.ToString() == "this"; 
            TranslateDynamicDispatchCall(node, outParams, arguments, guard, receiver);

            return;
        }

        private void TranslateInternalFunctionCall(FunctionCall node, List<BoogieIdentifierExpr> outParams = null)
        {
            List<BoogieExpr> arguments = TransUtils.GetDefaultArguments();

            // a Library is treated as an external function call
            // we need to do it here as the Lib.Foo, Lib is not an expression but name of a contract
            if (IsLibraryFunctionCall(node) != null)
            {
                arguments[1] = arguments[0]; //msg.sender is also this 
            }

            foreach (Expression arg in node.Arguments)
            {
                BoogieExpr argument = TranslateExpr(arg);    
                arguments.Add(argument);
            }

            // Question: why do we have a dynamic dispatch for an internal call?
            if (node.Kind.Equals("structConstructorCall"))
            {
                // assume the structAssignment is used as: s = S(args);
                TranslateStructConstructor(node, outParams[0]);
            }
            else if (IsDynamicDispatching(node))
            {
                TranslateDynamicDispatchCall(node, outParams, arguments, true, new BoogieIdentifierExpr("this"));
            }
            else if (IsStaticDispatching(node))
            {
                ContractDefinition contract = GetStaticDispatchingContract(node);
                string functionName = TransUtils.GetFuncNameFromFunctionCall(node);
                string callee = functionName + "_" + contract.Name;
                BoogieCallCmd callCmd = new BoogieCallCmd(callee, arguments, outParams);

                currentStmtList.AddStatement(callCmd);
            }
            else
            {
                throw new SystemException($"Unknown type of internal function call: {node.Expression}");
            }
            return;
        }

        private void TranslateDynamicDispatchCall(FunctionCall node, List<BoogieIdentifierExpr> outParams, List<BoogieExpr> arguments, bool condition, BoogieExpr receiver)
        {
            ContractDefinition contractDefn;
            VariableDeclaration varDecl;
            // Solidity internally generates foo() getter for any public state 
            // variable foo in a contract. 
            if (IsGetterForPublicVariable(node, out varDecl, out contractDefn))
            {
                BoogieExpr lhs = new BoogieMapSelect(new BoogieIdentifierExpr("DType"), receiver);
                BoogieExpr rhs = new BoogieIdentifierExpr(contractDefn.Name);
                BoogieExpr guard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, lhs, rhs);
                currentStmtList.AddStatement(new BoogieAssumeCmd(guard));
                VeriSolAssert(outParams.Count == 1, $"Do not support getters for tuples yet {node.ToString()} ");
                string varMapName = TransUtils.GetCanonicalStateVariableName(varDecl, context);
                BoogieMapSelect mapSelect = new BoogieMapSelect(new BoogieIdentifierExpr(varMapName), arguments[0]);
                currentStmtList.AddStatement(new BoogieAssignCmd(outParams[0], mapSelect));
                return;
            }

            Dictionary<ContractDefinition, FunctionDefinition> dynamicTypeToFuncMap;
            string signature = TransUtils.InferFunctionSignature(context, node);
            VeriSolAssert(context.HasFuncSignature(signature), $"Cannot find signature: {signature}");
            dynamicTypeToFuncMap = context.GetAllFuncDefinitions(signature);
            VeriSolAssert(dynamicTypeToFuncMap.Count > 0);

            BoogieIfCmd ifCmd = null;
            BoogieExpr lastGuard = null;
            BoogieCallCmd lastCallCmd = null;
            // generate a single if-then-else statement
            foreach (ContractDefinition dynamicType in dynamicTypeToFuncMap.Keys)
            {
                //ignore the ones those who do not derive from the current contract
                if (condition && !dynamicType.LinearizedBaseContracts.Contains(currentContract.Id))
                    continue;

                FunctionDefinition function = dynamicTypeToFuncMap[dynamicType];
                string callee = TransUtils.GetCanonicalFunctionName(function, context);

                BoogieExpr lhs = new BoogieMapSelect(new BoogieIdentifierExpr("DType"), receiver);
                BoogieExpr rhs = new BoogieIdentifierExpr(dynamicType.Name);
                BoogieExpr guard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, lhs, rhs);
                lastGuard = guard;
                BoogieCallCmd callCmd = new BoogieCallCmd(callee, arguments, outParams);
                lastCallCmd = callCmd;
                BoogieStmtList thenBody = BoogieStmtList.MakeSingletonStmtList(callCmd);
                BoogieStmtList elseBody = ifCmd == null ? null : BoogieStmtList.MakeSingletonStmtList(ifCmd);

                ifCmd = new BoogieIfCmd(guard, thenBody, elseBody);
            }

            // optimization: if there is only 1 type that we replace the if with a assume
            if (dynamicTypeToFuncMap.Keys.Count == 1)
            {
                // currentStmtList.AddStatement(new BoogieAssumeCmd(lastGuard));
                currentStmtList.AddStatement(lastCallCmd);
            }
            else
            {
                currentStmtList.AddStatement(ifCmd);
            }
        }

        private bool IsGetterForPublicVariable(FunctionCall node, out VariableDeclaration var, out ContractDefinition contractDefinition)
        {
            var = null;
            contractDefinition = null;
            if (node.Expression is MemberAccess memberAccess)
            {
                VeriSolAssert(memberAccess.ReferencedDeclaration != null);
                var contractTypeStr = memberAccess.Expression.TypeDescriptions.TypeString;

                if (!context.HasStateVarName(memberAccess.MemberName))
                {
                    return false;
                }
                contractDefinition = context.GetContractByName(contractTypeStr.Substring("contract ".Length));
                VeriSolAssert(contractDefinition != null, $"Expecting a contract {contractTypeStr} to exist in context");

                var = context.GetStateVarByDynamicType(memberAccess.MemberName, contractDefinition);
                return true;
            }

            return false;
        }

        private bool IsStaticDispatching(FunctionCall node)
        {
            if (node.Expression is MemberAccess memberAccess)
            {
                if (memberAccess.Expression is Identifier ident)
                {
                    if (context.GetASTNodeById(ident.ReferencedDeclaration) is ContractDefinition)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private ContractDefinition GetStaticDispatchingContract(FunctionCall node)
        {
            VeriSolAssert(node.Expression is MemberAccess);
            MemberAccess memberAccess = node.Expression as MemberAccess;

            Identifier contractId = memberAccess.Expression as Identifier;
            VeriSolAssert(contractId != null, $"Unknown contract name: {memberAccess.Expression}");

            ContractDefinition contract = context.GetASTNodeById(contractId.ReferencedDeclaration) as ContractDefinition;
            VeriSolAssert(contract != null);
            return contract;
        }

        private bool IsDynamicDispatching(FunctionCall node)
        {
            return node.Expression is Identifier;
        }

        private bool IsTypeCast(FunctionCall node)
        {
            return node.Kind.Equals("typeConversion");
        }

        private void TranslateTypeCast(FunctionCall node, BoogieExpr lhs)
        {
            VeriSolAssert(node.Kind.Equals("typeConversion"));
            VeriSolAssert(node.Arguments.Count == 1);
            VeriSolAssert(node.Arguments[0] is Identifier || node.Arguments[0] is MemberAccess || node.Arguments[0] is Literal || node.Arguments[0] is IndexAccess,
                "Argument to a typecast has to be an identifier, memberAccess, indexAccess or Literal");

            // target: lhs := T(expr);
            BoogieExpr exprToCast = TranslateExpr(node.Arguments[0]);

            if (node.Expression is Identifier) // cast to user defined types
            {
                Identifier contractId = node.Expression as Identifier;
                ContractDefinition contract = context.GetASTNodeById(contractId.ReferencedDeclaration) as ContractDefinition;
                VeriSolAssert(contract != null);

                // assume (DType[var] == T);
                BoogieMapSelect dtype = new BoogieMapSelect(new BoogieIdentifierExpr("DType"), exprToCast);
                BoogieExpr assumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, dtype, new BoogieIdentifierExpr(contract.Name));
                currentStmtList.AddStatement(new BoogieAssumeCmd(assumeExpr));
                // lhs := expr;
                currentStmtList.AddStatement(new BoogieAssignCmd(lhs, exprToCast));
                return;
            }
            else if (node.Expression is ElementaryTypeNameExpression elemType) // cast to elementary types
            {
                if (elemType.TypeName.Equals("address"))
                {
                    //try to do a best-effort to detect address(0) 
                    if (exprToCast is BoogieLiteralExpr blit)
                    {
                        if (blit.ToString().Equals("0"))
                        {
                            currentStmtList.AddStatement(new BoogieAssignCmd(lhs, new BoogieIdentifierExpr("null")));
                            return;
                        }
                    }
                }
                // lhs := expr;
                currentStmtList.AddStatement(new BoogieAssignCmd(lhs, exprToCast));
                return;
            } 
            else
            {
                throw new SystemException($"Unknown type cast: {node.Expression}");
            }
        }

        private void VeriSolAssert(bool cond, string message = "")
        {
            if (!cond)
            {
                var contractName = currentContract != null ? currentContract.Name : "Unknown";
                var funcName = currentFunction != null ? currentFunction.Name : "Unknown";
                Console.WriteLine($"Translation Error!! Contract {contractName}, Function {funcName}:: {message}");
            }
        }

        public override bool Visit(UnaryOperation node)
        {
            BoogieExpr expr = TranslateExpr(node.SubExpression);

            BoogieUnaryOperation.Opcode op;
            switch (node.Operator)
            {
                case "-":
                    op = BoogieUnaryOperation.Opcode.NEG;
                    break;
                case "!":
                    op = BoogieUnaryOperation.Opcode.NOT;
                    break;
                default:
                    op = BoogieUnaryOperation.Opcode.UNKNOWN;
                    throw new SystemException($"Unknwon unary operator: {node.Operator}");
            }

            BoogieUnaryOperation unaryExpr = new BoogieUnaryOperation(op, expr);
            currentExpr = unaryExpr;

            return false;
        }

        public override bool Visit(BinaryOperation node)
        {
            BoogieExpr leftExpr = TranslateExpr(node.LeftExpression);
            BoogieExpr rightExpr = TranslateExpr(node.RightExpression);

            BoogieBinaryOperation.Opcode op;
            switch (node.Operator)
            {
                case "+":
                    op = BoogieBinaryOperation.Opcode.ADD;
                    break;
                case "-":
                    op = BoogieBinaryOperation.Opcode.SUB;
                    break;
                case "*":
                    op = BoogieBinaryOperation.Opcode.MUL;
                    break;
                case "/":
                    op = BoogieBinaryOperation.Opcode.DIV;
                    break;
                case "%":
                    op = BoogieBinaryOperation.Opcode.MOD;
                    break;
                case "==":
                    op = BoogieBinaryOperation.Opcode.EQ;
                    break;
                case "!=":
                    op = BoogieBinaryOperation.Opcode.NEQ;
                    break;
                case ">":
                    op = BoogieBinaryOperation.Opcode.GT;
                    break;
                case ">=":
                    op = BoogieBinaryOperation.Opcode.GE;
                    break;
                case "<":
                    op = BoogieBinaryOperation.Opcode.LT;
                    break;
                case "<=":
                    op = BoogieBinaryOperation.Opcode.LE;
                    break;
                case "&&":
                    op = BoogieBinaryOperation.Opcode.AND;
                    break;
                case "||":
                    op = BoogieBinaryOperation.Opcode.OR;
                    break;
                default:
                    op = BoogieBinaryOperation.Opcode.UNKNOWN;
                    throw new SystemException($"Unknown binary operator: {node.Operator}");
            }

            BoogieBinaryOperation binaryExpr = new BoogieBinaryOperation(op, leftExpr, rightExpr);
            currentExpr = binaryExpr;

            return false;
        }

        public override bool Visit(Conditional node)
        {
            BoogieExpr guard = TranslateExpr(node.Condition);
            BoogieExpr thenExpr = TranslateExpr(node.TrueExpression);
            BoogieExpr elseExpr = TranslateExpr(node.FalseExpression);

            BoogieITE iteExpr = new BoogieITE(guard, thenExpr, elseExpr);
            currentExpr = iteExpr;

            return false;
        }

        public override bool Visit(IndexAccess node)
        {
            Expression baseExpression = node.BaseExpression;
            Expression indexExpression = node.IndexExpression;

            BoogieType indexType = MapArrayHelper.InferExprTypeFromTypeString(indexExpression.TypeDescriptions.TypeString);
            BoogieExpr indexExpr = TranslateExpr(indexExpression);

            // the baseExpression has an array or mapping type
            BoogieType baseKeyType = MapArrayHelper.InferKeyTypeFromTypeString(baseExpression.TypeDescriptions.TypeString);
            BoogieType baseValType = MapArrayHelper.InferValueTypeFromTypeString(baseExpression.TypeDescriptions.TypeString);
            BoogieExpr baseExpr = null;
            if (node.BaseExpression is Identifier identifier)
            {
                baseExpr = TranslateExpr(identifier);
            }
            else if (node.BaseExpression is IndexAccess indexAccess)
            {
                baseExpr = TranslateExpr(indexAccess);
            }
            else
            {
                throw new SystemException($"Unknown base in index access: {node.BaseExpression}");
            }

            BoogieExpr indexAccessExpr = new BoogieMapSelect(baseExpr, indexExpr);
            currentExpr = MapArrayHelper.GetMemoryMapSelectExpr(baseKeyType, baseValType, baseExpr, indexExpr);
            return false;
        }

        public override bool Visit(UsingForDirective node)
        {
            throw new NotSupportedException(node.ToString());
        }

        public override bool Visit(InlineAssembly node)
        {
            throw new NotSupportedException(node.ToString());
        }
    }

    static class QVarGenerator
    {
        public static BoogieIdentifierExpr NewQVar(int level, int pos)
        {
            return new BoogieIdentifierExpr($"__i__{level}_{pos}");
        }
    }
}
