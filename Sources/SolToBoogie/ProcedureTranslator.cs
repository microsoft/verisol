// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Numerics;
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
        private BoogieCallCmd currentPostlude = null;

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
                else
                {
                    child.Accept(this);
                }
            }

            return false;
        }

        private void TranslateStateVarDeclaration(VariableDeclaration varDecl)
        {
            Debug.Assert(varDecl.StateVariable, $"{varDecl} is not a state variable");

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
            Debug.Assert(node.IsConstructor || node.Modifiers.Count <= 1, "Multiple Modifiers are not supported yet");
            Debug.Assert(currentContract != null);

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

            // local variables and function body
            boogieToLocalVarsMap[currentBoogieProc] = new List<BoogieVariable>();

            // TODO: each local array variable should be distinct and 0 initialized


            BoogieStmtList procBody = new BoogieStmtList();

            if (node.Modifiers.Count == 1)
            {
                // insert call to modifier prelude
                if (context.ModifierToBoogiePreImpl.ContainsKey(node.Modifiers[0].ModifierName.Name))
                {
                    List<BoogieExpr> arguments = TransUtils.GetDefaultArguments();
                    if (node.Modifiers[0].Arguments != null)
                        arguments.AddRange(node.Modifiers[0].Arguments.ConvertAll(TranslateExpr));
                    string callee = node.Modifiers[0].ModifierName.ToString() + "_pre";
                    BoogieCallCmd callCmd = new BoogieCallCmd(callee, arguments, null);
                    procBody.AddStatement(callCmd);
                }

                // insert call to modifier postlude
                if (context.ModifierToBoogiePostImpl.ContainsKey(node.Modifiers[0].ModifierName.Name))
                {
                    List<BoogieExpr> arguments = TransUtils.GetDefaultArguments();
                    if (node.Modifiers[0].Arguments != null)
                        arguments.AddRange(node.Modifiers[0].Arguments.ConvertAll(TranslateExpr));
                    string callee = node.Modifiers[0].ModifierName.ToString() + "_post";
                    BoogieCallCmd callCmd = new BoogieCallCmd(callee, arguments, null);
                    currentPostlude = callCmd;
                }
            }

            procBody.AppendStmtList(TranslateStatement(node.Body));

            // add modifier postlude call if function body has no return
            if (currentPostlude != null)
            {
                procBody.AddStatement(currentPostlude);
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

            // generate real constructors
            if (node.IsConstructor)
            {
                GenerateConstructorWithBaseCalls(node, inParams);
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
                        BoogieAssignCmd assignCmd = new BoogieAssignCmd(lhs, new BoogieLiteralExpr(false));
                        stmtList.AddStatement(assignCmd);
                    }
                    else if (elementaryType.TypeDescriptions.TypeString.Equals("string")) 
                    {
                        string x = "";
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
                        BoogieAssignCmd assignCmd = new BoogieAssignCmd(lhs, new BoogieLiteralExpr(BigInteger.Zero));
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
                Debug.Assert(baseContract != null);

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
            Debug.Assert(ctor.IsConstructor, $"{ctor.Name} is not a constructor");

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

            // no local variables for constructor
            List<BoogieVariable> localVars = new List<BoogieVariable>();
            BoogieStmtList ctorBody = new BoogieStmtList();

            List<int> baseContractIds = new List<int>(contract.LinearizedBaseContracts);
            baseContractIds.Reverse();
            foreach (int id in baseContractIds)
            {
                ContractDefinition baseContract = context.GetASTNodeById(id) as ContractDefinition;
                Debug.Assert(baseContract != null);

                string callee = TransUtils.GetCanonicalConstructorName(baseContract) + "_NoBaseCtor";
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
                    foreach (BoogieVariable param in inParams)
                    {
                        inputs.Add(new BoogieIdentifierExpr(param.TypedIdent.Name));
                    }
                }
                BoogieCallCmd callCmd = new BoogieCallCmd(callee, inputs, outputs);
                ctorBody.AddStatement(callCmd);
            }

            BoogieImplementation implementation = new BoogieImplementation(procName, inParams, outParams, localVars, ctorBody);
            context.Program.AddDeclaration(implementation);
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
            foreach (VariableDeclaration parameter in node.Parameters)
            {
                string name = null;
                if (String.IsNullOrEmpty(parameter.Name))
                {
                    name = "__ret";
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
        private BoogieStmtList currentStmtList;
        private BoogieStmtList currentAuxStmtList; //these are statements generated due to nested calls etc.

        public BoogieStmtList TranslateStatement(Statement node)
        {
            currentStmtList = null;
            currentAuxStmtList = null;
            node.Accept(this);
            Debug.Assert(currentStmtList != null);

            // add source file path and line number
            BoogieAssertCmd annotationCmd = TransUtils.GenerateSourceInfoAnnotation(node, context);
            BoogieStmtList annotatedStmtList = BoogieStmtList.MakeSingletonStmtList(annotationCmd);
            if (currentAuxStmtList != null)
            {
                annotatedStmtList.AppendStmtList(currentAuxStmtList);
                currentAuxStmtList = null;
            }
            annotatedStmtList.AppendStmtList(currentStmtList);
            currentStmtList = annotatedStmtList;

            return currentStmtList;
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
            currentStmtList = new BoogieStmtList();
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
                Debug.Assert(node.Declarations.Count == 1, "Invalid multiple variable declarations");

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
                currentStmtList = BoogieStmtList.MakeSingletonStmtList(havocCmd);
            }
            return false;
        }

        public override bool Visit(Assignment node)
        {
            BoogieExpr lhs = TranslateExpr(node.LeftHandSide);
         
            if (node.RightHandSide is FunctionCall funcCall)
            {
                if (funcCall.Expression is NewExpression)
                {
                    // assume the new expression is used as: obj = new Class(args);
                    currentStmtList = TranslateNewStatement(funcCall, lhs);
                }
                else if (IsKeccakFunc(funcCall))
                {
                    currentStmtList = TranslateKeccakFuncCall(funcCall.Arguments[0], lhs);
                }
                else if (IsTypeCast(funcCall))
                {
                    // assume the type cast is used as: obj = C(var);
                    currentStmtList = TranslateTypeCast(funcCall, lhs);
                }
                else // normal function calls
                {
                    // assume it is used as: x = foo(args);
                    // Debug.Assert(lhs is BoogieIdentifierExpr, $"LHS is not identifier: {node.LeftHandSide}");

                    List<BoogieIdentifierExpr> outParams = new List<BoogieIdentifierExpr>();
                    outParams.Add(lhs as BoogieIdentifierExpr);

                    TranslateFunctionCalls(funcCall, outParams);
                }
            }
            else
            {
                BoogieExpr rhs = TranslateExpr(node.RightHandSide);
                BoogieStmtList stmtList = new BoogieStmtList();
                switch (node.Operator)
                {
                    case "=":
                        stmtList.AddStatement(new BoogieAssignCmd(lhs, rhs));
                        break;
                    case "+=":
                        stmtList.AddStatement(new BoogieAssignCmd(lhs, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, lhs, rhs)));
                        break;
                    case "-=":
                        stmtList.AddStatement(new BoogieAssignCmd(lhs, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, lhs, rhs)));
                        break;
                    case "*=":
                        stmtList.AddStatement(new BoogieAssignCmd(lhs, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.MUL, lhs, rhs)));
                        break;
                    case "/=":
                        stmtList.AddStatement(new BoogieAssignCmd(lhs, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.DIV, lhs, rhs)));
                        break;
                    default:
                        throw new SystemException($"Unknown assignment operator: {node.Operator}");
                }
                currentStmtList = stmtList;
            }

            var lhsType = node.LeftHandSide.TypeDescriptions != null ?
                node.LeftHandSide.TypeDescriptions :
                (node.RightHandSide.TypeDescriptions != null ?
                    node.RightHandSide.TypeDescriptions : null);

            if (lhsType != null)
            {
                //REFACTOR!
                if (lhsType.TypeString.StartsWith("uint") || lhsType.TypeString.StartsWith("int") || lhsType.TypeString.StartsWith("string ") || lhsType.TypeString.StartsWith("bytes"))
                {
                    var callCmd = new BoogieCallCmd("boogie_si_record_sol2Bpl_int", new List<BoogieExpr>() { lhs }, new List<BoogieIdentifierExpr>());
                    callCmd.Attributes = new List<BoogieAttribute>();
                    callCmd.Attributes.Add(new BoogieAttribute("cexpr", $"\"{node.LeftHandSide.ToString()}\""));
                    currentStmtList.AddStatement(callCmd);
                }
                if (lhsType.TypeString.Equals("address"))
                {
                    var callCmd = new BoogieCallCmd("boogie_si_record_sol2Bpl_ref", new List<BoogieExpr>() { lhs }, new List<BoogieIdentifierExpr>());
                    callCmd.Attributes = new List<BoogieAttribute>();
                    callCmd.Attributes.Add(new BoogieAttribute("cexpr", $"\"{node.LeftHandSide.ToString()}\""));
                    currentStmtList.AddStatement(callCmd);
                }
                if (lhsType.TypeString.Equals("bool"))
                {
                    var callCmd = new BoogieCallCmd("boogie_si_record_sol2Bpl_bool", new List<BoogieExpr>() { lhs }, new List<BoogieIdentifierExpr>());
                    callCmd.Attributes = new List<BoogieAttribute>();
                    callCmd.Attributes.Add(new BoogieAttribute("cexpr", $"\"{node.LeftHandSide.ToString()}\""));
                    currentStmtList.AddStatement(callCmd);
                }
            }



            return false;
        }

        private BoogieStmtList TranslateInBuiltFunction(FunctionCall funcCall, BoogieExpr lhs)
        {
            throw new NotImplementedException();
        }

        private void TranslateFunctionCalls(FunctionCall funcCall, List<BoogieIdentifierExpr> outParams)
        {
            if (IsExternalFunctionCall(funcCall))
            {
                currentStmtList = TranslateExternalFunctionCall(funcCall, outParams);
            }
            else
            {
                currentStmtList = TranslateInternalFunctionCall(funcCall, outParams);
            }
        }

        public override bool Visit(Return node)
        {
            if (node.Expression == null)
            {
                BoogieReturnCmd returnCmd = new BoogieReturnCmd();
                if (currentPostlude == null)
                {
                    currentStmtList = BoogieStmtList.MakeSingletonStmtList(returnCmd);
                }
                else
                {
                    currentStmtList = BoogieStmtList.MakeSingletonStmtList(currentPostlude);
                    currentStmtList.AddStatement(returnCmd);
                }
            }
            else
            {
                if (currentFunction.ReturnParameters.Length() != 1)
                {
                    throw new NotImplementedException("Cannot handle multiple return parameters");
                }

                VariableDeclaration retVarDecl = currentFunction.ReturnParameters.Parameters[0];
                string retVarName = String.IsNullOrEmpty(retVarDecl.Name) ?
                    "__ret" :
                    TransUtils.GetCanonicalLocalVariableName(retVarDecl);
                BoogieIdentifierExpr retVar = new BoogieIdentifierExpr(retVarName);
                BoogieExpr expr = TranslateExpr(node.Expression);
                BoogieAssignCmd assignCmd = new BoogieAssignCmd(retVar, expr);
                currentStmtList = BoogieStmtList.MakeSingletonStmtList(assignCmd);

                if (currentPostlude != null)
                {
                    currentStmtList.AddStatement(currentPostlude);
                }
                // add a return command, in case the original return expr is in the middle of the function body
                currentStmtList.AddStatement(new BoogieReturnCmd());
            }
            return false;
        }

        public override bool Visit(Throw node)
        {
            BoogieAssumeCmd assumeCmd = new BoogieAssumeCmd(new BoogieLiteralExpr(false));
            currentStmtList = BoogieStmtList.MakeSingletonStmtList(assumeCmd);
            return false;
        }

        public override bool Visit(IfStatement node)
        {
            BoogieExpr guard = TranslateExpr(node.Condition);
            BoogieStmtList auxStmtList = new BoogieStmtList();
            if (currentAuxStmtList!=null)
            {
                auxStmtList.AppendStmtList(currentAuxStmtList);
            }

            BoogieStmtList thenBody = TranslateStatement(node.TrueBody);
            BoogieStmtList elseBody = null;
            if (node.FalseBody != null)
            {
                elseBody = TranslateStatement(node.FalseBody);
            }
            BoogieIfCmd ifCmd = new BoogieIfCmd(guard, thenBody, elseBody);

            currentStmtList = new BoogieStmtList();
            currentStmtList.AppendStmtList(auxStmtList);
            currentStmtList.AddStatement(ifCmd);
            return false;
        }

        public override bool Visit(WhileStatement node)
        {
            BoogieExpr guard = TranslateExpr(node.Condition);
            BoogieStmtList body = TranslateStatement(node.Body);

            BoogieWhileCmd whileCmd = new BoogieWhileCmd(guard, body);

            currentStmtList = BoogieStmtList.MakeSingletonStmtList(whileCmd);
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

            currentStmtList = stmtList;
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

            currentStmtList = stmtList;
            return false;
        }

        public override bool Visit(Break node)
        {
            BoogieBreakCmd breakCmd = new BoogieBreakCmd();
            currentStmtList = BoogieStmtList.MakeSingletonStmtList(breakCmd);
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
                Debug.Assert(!(unaryOperation.SubExpression is UnaryOperation));

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
                    Debug.Assert(typeStr.StartsWith("int") || typeStr.StartsWith("uint") || typeStr.Equals("bool") || typeStr.StartsWith("string "),
                        $"Only handle delete for scalars, found {typeStr}");
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
                    currentStmtList = BoogieStmtList.MakeSingletonStmtList(assignCmd);
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
                if (((FunctionCall) expr).Expression is ElementaryTypeNameExpression)
                {
                    currentExpr = TranslateExpr(((FunctionCall) expr).Arguments[0]);
                }
                else
                {
                    throw new NotImplementedException("Cannot handle non-elementary type cast");
                }
            }
            else
            {
                expr.Accept(this);
                Debug.Assert(currentExpr != null);
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
                // assumption: numbers statrting in 0x are addresses
                if (node.Value.StartsWith("0x") || node.Value.StartsWith("0X"))
                {
                    BigInteger num = BigInteger.Parse(node.Value.Substring(2), NumberStyles.AllowHexSpecifier);
                    if (num == BigInteger.Zero)
                    {
                        currentExpr = new BoogieIdentifierExpr("null");
                    }
                    else
                    {
                        currentExpr = new BoogieFuncCallExpr("ConstantToRef", new List<BoogieExpr>() { new BoogieLiteralExpr(num) });
                    }
                }
                else
                {
                    BigInteger num = BigInteger.Parse(node.Value);
                    currentExpr = new BoogieLiteralExpr(num);
                }
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

        public override bool Visit(Identifier node)
        {
            if (node.Name.Equals("this"))
            {
                currentExpr = new BoogieIdentifierExpr("this");
            }
            else // explicitly defined identifiers
            {
                Debug.Assert(context.HasASTNodeId(node.ReferencedDeclaration), $"Unknown node: {node}");
                VariableDeclaration varDecl = context.GetASTNodeById(node.ReferencedDeclaration) as VariableDeclaration;
                Debug.Assert(varDecl != null);

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

                Debug.Assert(context.HasASTNodeId(identifier.ReferencedDeclaration), $"Unknown node: {identifier}");
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
            Debug.Assert(node.MemberName.Equals("length"));

            BoogieExpr indexExpr = TranslateExpr(node.Expression);
            var mapSelect = new BoogieMapSelect(new BoogieIdentifierExpr("Length"), indexExpr);
            return mapSelect;
        }

        public override bool Visit(FunctionCall node)
        {
            Debug.Assert(!(node.Expression is NewExpression), $"new expressions should be handled in assignment");

            string functionName = TransUtils.GetFuncNameFromFunctionCall(node);

            if (functionName.Equals("assert"))
            {
                Debug.Assert(node.Arguments.Count == 1);
                BoogieExpr predicate = TranslateExpr(node.Arguments[0]);
                BoogieAssertCmd assertCmd = new BoogieAssertCmd(predicate);
                currentStmtList = BoogieStmtList.MakeSingletonStmtList(assertCmd);
            }
            else if (functionName.Equals("require"))
            {
                Debug.Assert(node.Arguments.Count == 1 || node.Arguments.Count == 2);
                BoogieExpr predicate = TranslateExpr(node.Arguments[0]);
                BoogieAssumeCmd assumeCmd = new BoogieAssumeCmd(predicate);
                currentStmtList = BoogieStmtList.MakeSingletonStmtList(assumeCmd);
            }
            else if (functionName.Equals("revert"))
            {
                Debug.Assert(node.Arguments.Count == 0 || node.Arguments.Count == 1);
                BoogieAssumeCmd assumeCmd = new BoogieAssumeCmd(new BoogieLiteralExpr(false));
                currentStmtList = BoogieStmtList.MakeSingletonStmtList(assumeCmd);
            }
            else if (IsTypeCast(node))
            {
                // handled in assignment
                throw new SystemException("Type cast is handled in assignment, use temporaries to break up nested expression");
            }
            else if (IsKeccakFunc(node))
            {
                // handled only in assignments
                throw new SystemException("Keccak256 only handled in assignment, use temporaries to break up nested expression");
            }
            else if (context.HasEventNameInContract(currentContract, functionName))
            {
                // generate empty statement list to ignore the event call
                currentStmtList = new BoogieStmtList();
            }
            else if (functionName.Equals("call"))
            {
                currentStmtList = TranslateCallStatement(node);
            }
            else if (functionName.Equals("send") || functionName.Equals("delegatecall"))
            {
                throw new NotImplementedException(functionName);
            }
            else if (IsDynamicArrayPush(node))
            {
                currentStmtList = TranslateDynamicArrayPush(node);
            }
            else if (IsExternalFunctionCall(node))
            {
                // external function calls

                // HACK: this is the way to identify the return type is void and hence don't need temporary variable
                if (node.TypeDescriptions.TypeString != "tuple()")
                {
                    var outParams = new List<BoogieIdentifierExpr>();

                    var boogieTypeCall = MapArrayHelper.InferExprTypeFromTypeString(node.TypeDescriptions.TypeString);
                    var tmpVar = new BoogieLocalVariable(context.MakeFreshTypedIdent(boogieTypeCall));
                    boogieToLocalVarsMap[currentBoogieProc].Add(tmpVar);

                    var tmpVarExpr = new BoogieIdentifierExpr(tmpVar.Name);
                    outParams.Add(tmpVarExpr);
                    currentAuxStmtList = TranslateExternalFunctionCall(node, outParams);
                    currentExpr = tmpVarExpr;
                }
                else
                {
                    currentStmtList = TranslateExternalFunctionCall(node);
                }
            }
            else // internal function calls
            {
                // HACK: this is the way to identify the return type is void and hence don't need temporary variable
                if (node.TypeDescriptions.TypeString != "tuple()")
                {
                    // internal function calls
                    var outParams = new List<BoogieIdentifierExpr>();

                    var boogieTypeCall = MapArrayHelper.InferExprTypeFromTypeString(node.TypeDescriptions.TypeString);
                    var tmpVar = new BoogieLocalVariable(context.MakeFreshTypedIdent(boogieTypeCall));
                    boogieToLocalVarsMap[currentBoogieProc].Add(tmpVar);

                    var tmpVarExpr = new BoogieIdentifierExpr(tmpVar.Name);
                    outParams.Add(tmpVarExpr);
                    currentAuxStmtList = TranslateInternalFunctionCall(node, outParams);
                    currentExpr = tmpVarExpr;
                }
                else
                {
                    currentStmtList = TranslateInternalFunctionCall(node);
                }

            }
            return false;
        }

        private bool IsKeccakFunc(FunctionCall node)
        {
            if (node.Expression is Identifier ident)
            {
                return ident.Name.Equals("keccak256");
            }
            return false;
        }

        private BoogieStmtList TranslateKeccakFuncCall(Expression expression, BoogieExpr lhs)
        {
            var boogieExpr = TranslateExpr(expression);
            var boogieStmtList = new BoogieStmtList();
            var keccakExpr = new BoogieFuncCallExpr("keccak256", new List<BoogieExpr>() { boogieExpr });
            boogieStmtList.AddStatement(new BoogieAssignCmd(lhs, keccakExpr));
            return boogieStmtList;
        }

        private BoogieStmtList TranslateCallStatement(FunctionCall node)
        {
            BoogieStmtList stmtList = new BoogieStmtList();
            stmtList.AddStatement(new BoogieSkipCmd(node.ToString()));
            throw new NotImplementedException();
        }

        private BoogieStmtList TranslateNewStatement(FunctionCall node, BoogieExpr lhs)
        {
            Debug.Assert(node.Expression is NewExpression);
            NewExpression newExpr = node.Expression as NewExpression;
            Debug.Assert(newExpr.TypeName is UserDefinedTypeName);
            UserDefinedTypeName udt = newExpr.TypeName as UserDefinedTypeName;

            ContractDefinition contract = context.GetASTNodeById(udt.ReferencedDeclaration) as ContractDefinition;
            Debug.Assert(contract != null);

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
            BoogieStmtList stmtList = new BoogieStmtList();

            // call tmp := FreshRefGenerator();
            stmtList.AddStatement(
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
            stmtList.AddStatement(new BoogieAssumeCmd(dtypeAssumeExpr));
            // The assume DType[tmp] == A is before the call as the constructor may do a dynamic 
            // dispatch and the DType[tmp] is unconstrained before the call
            List<BoogieIdentifierExpr> outputs = new List<BoogieIdentifierExpr>();
            stmtList.AddStatement(new BoogieCallCmd(callee, inputs, outputs));
            // lhs := tmp;
            stmtList.AddStatement(new BoogieAssignCmd(lhs, tmpVarIdentExpr));
            return stmtList;
        }

        private bool IsDynamicArrayPush(FunctionCall node)
        {
            string functionName = TransUtils.GetFuncNameFromFunctionCall(node);
            if (functionName.Equals("push"))
            {
                Debug.Assert(node.Expression is MemberAccess);
                MemberAccess memberAccess = node.Expression as MemberAccess;
                return MapArrayHelper.IsArrayTypeString(memberAccess.Expression.TypeDescriptions.TypeString);
            }
            return false;
        }

        private BoogieStmtList TranslateDynamicArrayPush(FunctionCall node)
        {
            Debug.Assert(node.Expression is MemberAccess);
            Debug.Assert(node.Arguments.Count == 1);

            MemberAccess memberAccess = node.Expression as MemberAccess;
            BoogieExpr receiver = TranslateExpr(memberAccess.Expression);
            BoogieExpr element = TranslateExpr(node.Arguments[0]);

            BoogieExpr lengthMapSelect = new BoogieMapSelect(new BoogieIdentifierExpr("Length"), receiver);
            // suppose the form is a.push(e)
            BoogieStmtList stmtList = new BoogieStmtList();
            // tmp := Length[this][a];
            BoogieTypedIdent tmpIdent = context.MakeFreshTypedIdent(BoogieType.Int);
            boogieToLocalVarsMap[currentBoogieProc].Add(new BoogieLocalVariable(tmpIdent));
            BoogieIdentifierExpr tmp = new BoogieIdentifierExpr(tmpIdent.Name);
            BoogieAssignCmd assignCmd = new BoogieAssignCmd(tmp, lengthMapSelect);
            stmtList.AddStatement(assignCmd);

            // M[this][a][tmp] := e;
            BoogieType mapKeyType = BoogieType.Int;
            BoogieType mapValType = MapArrayHelper.InferExprTypeFromTypeString(node.Arguments[0].TypeDescriptions.TypeString);
            BoogieExpr mapSelect = MapArrayHelper.GetMemoryMapSelectExpr(mapKeyType, mapValType, receiver, tmp);
            BoogieAssignCmd writeCmd = new BoogieAssignCmd(mapSelect, element);
            stmtList.AddStatement(writeCmd);

            // Length[this][a] := tmp + 1;
            BoogieExpr rhs = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, tmp, new BoogieLiteralExpr(1));
            BoogieAssignCmd updateLengthCmd = new BoogieAssignCmd(lengthMapSelect, rhs);
            stmtList.AddStatement(updateLengthCmd);
            return stmtList;
        }

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
                    if (!(context.GetASTNodeById(identifier.ReferencedDeclaration) is ContractDefinition))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private BoogieStmtList TranslateExternalFunctionCall(FunctionCall node, List<BoogieIdentifierExpr> outParams = null)
        {
            BoogieStmtList stmtList = new BoogieStmtList();

            Debug.Assert(node.Expression is MemberAccess);
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

            string signature = TransUtils.InferFunctionSignature(context, node);
            Debug.Assert(context.HasFuncSignature(signature), $"Cannot find signature: {signature}");

            Dictionary<ContractDefinition, FunctionDefinition> dynamicTypeToFuncMap = context.GetAllFuncDefinitions(signature);
            Debug.Assert(dynamicTypeToFuncMap.Count > 0);

            BoogieIfCmd ifCmd = null;
            BoogieExpr lastGuard = null;
            BoogieCallCmd lastCallCmd = null;

            // generate a single if-then-else statement
            foreach (ContractDefinition dynamicType in dynamicTypeToFuncMap.Keys)
            {

                //ignore the ones those who do not derive from the current contract
                // TODO: we need a way to determine type of receiver from "x.Foo()"
                if (memberAccess.Expression.ToString() == "this" && 
                    !dynamicType.LinearizedBaseContracts.Contains(currentContract.Id))
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
                stmtList.AddStatement(new BoogieAssumeCmd(lastGuard));
                stmtList.AddStatement(lastCallCmd);
            }
            else
            {
                stmtList.AddStatement(ifCmd);
            }

            return stmtList;
        }

        private BoogieStmtList TranslateInternalFunctionCall(FunctionCall node, List<BoogieIdentifierExpr> outParams = null)
        {
            List<BoogieExpr> arguments = TransUtils.GetDefaultArguments();

            foreach (Expression arg in node.Arguments)
            {
                BoogieExpr argument = TranslateExpr(arg);
                arguments.Add(argument);
            }

            BoogieStmtList stmtList = new BoogieStmtList();


            // Question: why do we have a dynamic dispatch for an internal call?
            if (IsDynamicDispatching(node))
            {
                string signature = TransUtils.InferFunctionSignature(context, node);
                Debug.Assert(context.HasFuncSignature(signature), $"Cannot find signature: {signature}");

                Dictionary<ContractDefinition, FunctionDefinition> dynamicTypeToFuncMap = context.GetAllFuncDefinitions(signature);
                Debug.Assert(dynamicTypeToFuncMap.Count > 0);

                BoogieIfCmd ifCmd = null;
                BoogieExpr lastGuard = null;
                BoogieCallCmd lastCallCmd = null;
                // generate a single if-then-else statement
                foreach (ContractDefinition dynamicType in dynamicTypeToFuncMap.Keys)
                {
                    //ignore the ones those who do not derive from the current contract
                    if (!dynamicType.LinearizedBaseContracts.Contains(currentContract.Id))
                        continue;

                    FunctionDefinition function = dynamicTypeToFuncMap[dynamicType];
                    string callee = TransUtils.GetCanonicalFunctionName(function, context);

                    BoogieExpr lhs = new BoogieMapSelect(new BoogieIdentifierExpr("DType"), new BoogieIdentifierExpr("this"));
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
                    stmtList.AddStatement(new BoogieAssumeCmd(lastGuard));
                    stmtList.AddStatement(lastCallCmd);
                }
                else
                {
                    stmtList.AddStatement(ifCmd);
                }
            }
            else if (IsStaticDispatching(node))
            {
                ContractDefinition contract = GetStaticDispatchingContract(node);
                string functionName = TransUtils.GetFuncNameFromFunctionCall(node);
                string callee = functionName + "_" + contract.Name;
                BoogieCallCmd callCmd = new BoogieCallCmd(callee, arguments, outParams);

                stmtList.AddStatement(callCmd);
            }
            else
            {
                throw new SystemException($"Unknown type of internal function call: {node.Expression}");
            }
            return stmtList;
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
            Debug.Assert(node.Expression is MemberAccess);
            MemberAccess memberAccess = node.Expression as MemberAccess;

            Identifier contractId = memberAccess.Expression as Identifier;
            Debug.Assert(contractId != null, $"Unknown contract name: {memberAccess.Expression}");

            ContractDefinition contract = context.GetASTNodeById(contractId.ReferencedDeclaration) as ContractDefinition;
            Debug.Assert(contract != null);
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

        private BoogieStmtList TranslateTypeCast(FunctionCall node, BoogieExpr lhs)
        {
            Debug.Assert(node.Kind.Equals("typeConversion"));
            Debug.Assert(node.Arguments.Count == 1);
            Debug.Assert(node.Arguments[0] is Identifier || node.Arguments[0] is MemberAccess);

            // target: lhs := T(expr);
            BoogieExpr exprToCast = TranslateExpr(node.Arguments[0]);

            if (node.Expression is Identifier) // cast to user defined types
            {
                Identifier contractId = node.Expression as Identifier;
                ContractDefinition contract = context.GetASTNodeById(contractId.ReferencedDeclaration) as ContractDefinition;
                Debug.Assert(contract != null);

                BoogieStmtList stmtList = new BoogieStmtList();
                // assume (DType[var] == T);
                BoogieMapSelect dtype = new BoogieMapSelect(new BoogieIdentifierExpr("DType"), exprToCast);
                BoogieExpr assumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, dtype, new BoogieIdentifierExpr(contract.Name));
                stmtList.AddStatement(new BoogieAssumeCmd(assumeExpr));
                // lhs := expr;
                stmtList.AddStatement(new BoogieAssignCmd(lhs, exprToCast));
                return stmtList;
            }
            else if (node.Expression is ElementaryTypeNameExpression) // cast to elementary types
            {
                BoogieStmtList stmtList = new BoogieStmtList();
                // lhs := expr;
                stmtList.AddStatement(new BoogieAssignCmd(lhs, exprToCast));
                return stmtList;
            }
            else
            {
                throw new SystemException($"Unknown type cast: {node.Expression}");
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
