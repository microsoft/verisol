// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Text.RegularExpressions;
    using BoogieAST;
    using SolidityAST;

    public static class Extensions
    {
        public static bool IsInt(this TypeDescription typeDescription)
        {
            return typeDescription.TypeString.StartsWith("int", StringComparison.CurrentCulture);
        }

        public static bool IsIntWSize(this TypeDescription typeDescription, out uint sz)
        {
            string typeStr = typeDescription.TypeString;
            if (!typeStr.StartsWith("int", StringComparison.CurrentCulture))
            {
                //Console.WriteLine($"IsIntWSize: not int type: {typeStr}");
                sz = 0;
                return false;
            }

            //Console.WriteLine($"IsIntWSize: int type: {typeStr}");
            try
            {
                if (typeStr.Equals("int") || (typeStr.Contains("const")))
                {
                    sz = 256;
                }
                else
                {
                    sz = uint.Parse(GetNumberFromEnd(typeStr));
                }
                //Console.WriteLine("int, intKK or int_const type, size is {0}", sz);
            }
            catch (Exception e)
            {
                Console.WriteLine($"VeriSol translation error in IsIntWSize: unknown intXX type: {e.Message}");
                sz = 0;
                return false;
            }

            return true;
        }
        public static bool IsUint(this TypeDescription typeDescription)
        {
            return !typeDescription.IsDynamicArray() && !typeDescription.IsStaticArray()
                && typeDescription.TypeString.StartsWith("uint", StringComparison.CurrentCulture);
        }

        public static string GetNumberFromEnd(string text)
        {
            int i = text.Length - 1;
            while (i >= 0)
            {
                if (!char.IsNumber(text[i])) break;
                i--;
            }
            return text.Substring(i + 1);
        }
        public static bool IsUintWSize(this TypeDescription typeDescription, out uint sz)
        {
            string typeStr = typeDescription.TypeString;
            if (!typeStr.StartsWith("uint", StringComparison.CurrentCulture))
            {
                //Console.WriteLine($"IsUintWSize: not uint type: {typeStr}");
                sz = 0;
                return false;
            }

            //Console.WriteLine($"IsUintWSize: uint type: {typeStr}");
            try
            {
                if (typeStr.Equals("uint") || (typeStr.Contains("const")))
                {
                    sz = 256;
                }
                else
                {
                    sz = uint.Parse(GetNumberFromEnd(typeStr));
                }

                //Console.WriteLine("uint, uintKK or uint_const type, size is {0}", sz);
            }
            catch (Exception e)
            {
                Console.WriteLine($"VeriSol translation error in IsUintWSize: unknown uintXX type: {e.Message}");
                sz = 0;
                return false;
            }

            return true;
        }
        
        public static bool IsBool(this TypeDescription typeDescription)
        {
            return typeDescription.TypeString.StartsWith("bool", StringComparison.CurrentCulture);
        }

        public static bool IsString(this TypeDescription typeDescription)
        {
            return typeDescription.TypeString.StartsWith("string", StringComparison.CurrentCulture);
        }

        // TODO: Provide a better way to check for dynamic array type
        public static bool IsDynamicArray(this TypeDescription typeDescription)
        {
            var match = Regex.Match(typeDescription.TypeString, @".*\[\]").Success;
            return match; 
        }

        // TODO: Provide a better way to check for array type
        public static bool IsStaticArray(this TypeDescription typeDescription)
        {
            var match = Regex.Match(typeDescription.TypeString, @".*\[\d+\]").Success;
            return match;
        }

    }

    public static class TransUtils
    {
        public static BoogieType GetBoogieTypeFromSolidityTypeName(TypeName type)
        {
            if (type is ElementaryTypeName elementaryType)
            {
                string typeString = elementaryType.TypeDescriptions.TypeString;
                switch (typeString)
                {
                    case "uint256":
                    case "uint248":
                    case "uint240":
                    case "uint232":
                    case "uint224":
                    case "uint216":
                    case "uint208":
                    case "uint200":
                    case "uint192":
                    case "uint184":
                    case "uint176":
                    case "uint168":
                    case "uint160":
                    case "uint152":
                    case "uint144":
                    case "uint136":
                    case "uint128":
                    case "uint120":
                    case "uint112":
                    case "uint104":
                    case "uint96":
                    case "uint88":
                    case "uint80":
                    case "uint72":
                    case "uint64":
                    case "uint56":
                    case "uint48":
                    case "uint40":
                    case "uint32":
                    case "uint24":
                    case "uint16":
                    case "uint8":
                    case "int256":
                    case "int248":
                    case "int240":
                    case "int232":
                    case "int224":
                    case "int216":
                    case "int208":
                    case "int200":
                    case "int192":
                    case "int184":
                    case "int176":
                    case "int168":
                    case "int160":
                    case "int152":
                    case "int144":
                    case "int136":
                    case "int128":
                    case "int120":
                    case "int112":
                    case "int104":
                    case "int96":
                    case "int88":
                    case "int80":
                    case "int72":
                    case "int64":
                    case "int56":
                    case "int48":
                    case "int40":
                    case "int32":
                    case "int24":
                    case "int16":
                    case "int8":
                    case "uint":
                    case "int":
                        return BoogieType.Int;
                    case "bool":
                        return BoogieType.Bool;
                    case "string":
                        return BoogieType.Int;
                    case "address":
                    case "address payable":
                        return BoogieType.Ref;
                    case "bytes32":
                    case "bytes":
                        return BoogieType.Int;
                    default:
                        throw new SystemException($"Unknown elementary type name: {type}");
                }
            }
            else if (type is UserDefinedTypeName udt)
            {
                string typeString = udt.TypeDescriptions.TypeString;
                if (typeString.StartsWith("enum "))
                {
                    // model enum type using integers
                    return BoogieType.Int;
                }
                else if (typeString.StartsWith("contract "))
                {
                    string contractName = typeString.Substring("contract ".Length);
                    // model contract type using Ref
                    return BoogieType.Ref;
                }
                else if (typeString.StartsWith("struct "))
                {
                    string contractName = typeString.Substring("struct ".Length);
                    return BoogieType.Ref;
                }
                else
                {
                    throw new SystemException($"Unknown type name: {typeString}");
                }
            }
            else if (type is Mapping mapping)
            {
                return BoogieType.Ref;
            }
            else if (type is ArrayTypeName array)
            {
                return BoogieType.Ref;
            }
            else
            {
                throw new SystemException($"Unknown type name: {type}");
            }
        }

        public static string GetCanonicalVariableName(VariableDeclaration varDecl, TranslatorContext context)
        {
            return varDecl.StateVariable ? GetCanonicalStateVariableName(varDecl, context) : GetCanonicalLocalVariableName(varDecl);
        }

        public static string GetCanonicalLocalVariableName(VariableDeclaration varDecl)
        {
            return varDecl.Name + "_s" + varDecl.Scope.ToString();
        }

        public static string GetCanonicalStateVariableName(VariableDeclaration varDecl, TranslatorContext context)
        {
            Debug.Assert(varDecl.StateVariable, $"{varDecl.Name} is not a state variable");

            Dictionary<VariableDeclaration, ContractDefinition> varToContractMap = context.StateVarToContractMap;
            Debug.Assert(varToContractMap.ContainsKey(varDecl), $"Cannot find state variable: {varDecl.Name}");
            return varDecl.Name + "_" + varToContractMap[varDecl].Name;
        }

        public static string GetCanonicalFunctionName(FunctionDefinition funcDef, TranslatorContext context)
        {
            ContractDefinition contract = context.GetContractByFunction(funcDef);
            return funcDef.Name + "_" + contract.Name;
        }

        public static string GetCanonicalConstructorName(ContractDefinition contract)
        {
            return contract.Name + "_" + contract.Name;
        }

        public static int GetEnumValueIndex(EnumDefinition enumDef, string value)
        {
            List<EnumValue> members = enumDef.Members;
            for (int i = 0; i < members.Count; ++i)
            {
                if (members[i].Name.Equals(value))
                {
                    return i;
                }
            }
            throw new SystemException($"Unknown value {value} in enum definition {enumDef}");
        }

        public static string ComputeFunctionSignature(FunctionDefinition funcDef)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(funcDef.Name).Append("(");
            ParameterList parameterList = funcDef.Parameters;
            Debug.Assert(parameterList != null && parameterList.Parameters != null);
            if (parameterList.Parameters.Count > 0)
            {
                foreach (VariableDeclaration varDecl in parameterList.Parameters)
                {
                    // use TypeDescriptions.TypeString instead of TypeName
                    // when a nested struct/Enum is declared TypeName may not have the contractprefix (Foo.EnumType), 
                    // but TypeDescriptions has the entire prefix. This is useful as a function may be declared as (EnumType a)
                    // and the TypeName does not have the context. 
                    builder.Append(varDecl.TypeDescriptions.TypeString).Append(", ");
                }
                builder.Length -= 2;
            }
            builder.Append(")");
            return builder.ToString();
        }

        public static string ComputeEventSignature(EventDefinition eventDef)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(eventDef.Name).Append("(");
            ParameterList parameterList = eventDef.Parameters;
            Debug.Assert(parameterList != null && parameterList.Parameters != null);
            if (parameterList.Parameters.Count > 0)
            {
                foreach (VariableDeclaration varDecl in parameterList.Parameters)
                {
                    // use TypeDescriptions.TypeString instead of TypeName
                    // when a nested struct/Enum is declared TypeName may not have the contractprefix (Foo.EnumType), 
                    // but TypeDescriptions has the entire prefix. This is useful as a function may be declared as (EnumType a)
                    // and the TypeName does not have the context. 
                    builder.Append(varDecl.TypeDescriptions.TypeString).Append(", ");
                }
                builder.Length -= 2;
            }
            builder.Append(")");
            return builder.ToString();
        }

        public static string InferFunctionSignature(TranslatorContext context, FunctionCall node)
        {
            Debug.Assert(node.Arguments != null);

            if (node.Expression is MemberAccess memberAccess)
            {
                Debug.Assert(memberAccess.ReferencedDeclaration != null);
                FunctionDefinition function = context.GetASTNodeById(memberAccess.ReferencedDeclaration.Value) as FunctionDefinition;
                Debug.Assert(function != null, $"Could not find function {node.ToString()}");
                StringBuilder builder = new StringBuilder();
                builder.Append(function.Name).Append("(");
                if (function.Parameters.Parameters != null && function.Parameters.Parameters.Count > 0)
                {
                    foreach (VariableDeclaration varDecl in function.Parameters.Parameters)
                    {
                        builder.Append(varDecl.TypeDescriptions.TypeString).Append(", ");
                    }
                    builder.Length -= 2;
                }
                builder.Append(")");
                return builder.ToString();
            }
            else
            {
                string functionName = GetFuncNameFromFunctionCall(node);
                StringBuilder builder = new StringBuilder();
                builder.Append(functionName).Append("(");
                if (node.Arguments.Count > 0)
                {
                    foreach (Expression argument in node.Arguments)
                    {
                        string typeString = argument.TypeDescriptions.TypeString;
                        if (typeString.StartsWith("int_const"))
                        {
                            typeString = "int256";
                        }
                        if (typeString.StartsWith("uint_const"))
                        {
                            typeString = "int256";
                        }
                        if (typeString.StartsWith("string") || typeString.StartsWith("literal_string"))
                        {
                            typeString = "string";
                        }
                        if (typeString.StartsWith("bytes "))
                        {
                            typeString = "bytes"; //"bytes storage ref"
                        }
                        if (typeString.Contains(" memory")) //"struct Foo memory"
                        {
                            typeString = typeString.Substring(0, typeString.IndexOf(" memory"));
                        }
                        if (typeString.Contains(" storage"))
                        {
                            typeString = typeString.Substring(0, typeString.IndexOf(" storage"));
                        }
                        if (typeString.Contains(" payable"))
                        {
                            typeString = typeString.Substring(0, typeString.IndexOf(" payable")); //address payable
                        }
                        builder.Append(typeString).Append(", ");
                    }
                    builder.Length -= 2;
                }
                builder.Append(")");
                return builder.ToString();
            }
        }

        public static string InferExpressionType(Expression expression)
        {
            Debug.Assert(expression.TypeDescriptions != null, $"Null type description for {expression}");
            string typeString = expression.TypeDescriptions.TypeString;
            if (typeString.Equals("bool"))
            {
                return "bool";
            }
            else if (typeString.StartsWith("uint"))
            {
                return "uint";
            }
            else if (typeString.StartsWith("int"))
            {
                return "int";
            }
            else if (typeString.Equals("address") || typeString.Equals("address payable"))
            {
                return "address";
            }
            else
            {
                throw new SystemException($"Unknown type string {typeString} for expression {expression}");
            }
        }

        public static string GetFuncNameFromFunctionCall(FunctionCall node)
        {
            if (node.Expression is FunctionCall funcCall)
            {
                if (funcCall.Expression is MemberAccess memberAccess)
                {
                    return GetFuncNameFromFuncCallExpr(memberAccess.Expression);
                }
                else
                {
                    return GetFuncNameFromFuncCallExpr(funcCall.Expression);
                }
            }
            else
            {
                return GetFuncNameFromFuncCallExpr(node.Expression);
            }
        }

        private static string GetFuncNameFromFuncCallExpr(Expression expr)
        {
            string functionName = null;
            if (expr is Identifier ident)
            {
                functionName = ident.Name;
            }
            else if (expr is ElementaryTypeNameExpression elemExpr)
            {
                functionName = elemExpr.TypeName;
            }
            else if (expr is MemberAccess memberAccess)
            {
                functionName = memberAccess.MemberName;
            }
            else if (expr is FunctionCall funcCall)
            {
                functionName = GetFuncNameFromFunctionCall(funcCall);
            }
            else
            {
                throw new SystemException($"Unknown form of function call expr: {expr}");
            }
            Debug.Assert(functionName != null);
            return functionName;
        }

        public static Tuple<string, int> GenerateSourceInfoAnnotation(ASTNode node, TranslatorContext context)
        {
            return (Tuple.Create<string,int>(context.GetAbsoluteSourcePathOfASTNode(node), context.GetLineNumberOfASTNode(node)));
        }

        public static List<BoogieVariable> GetDefaultInParams()
        {
            return new List<BoogieVariable>()
            {
                // add a parameter for this object
                new BoogieFormalParam(new BoogieTypedIdent("this", BoogieType.Ref)),
                // add a parameter for msg.sender
                new BoogieFormalParam(new BoogieTypedIdent("msgsender_MSG", BoogieType.Ref)),
                // add a parameter for msg.value
                new BoogieFormalParam(new BoogieTypedIdent("msgvalue_MSG", BoogieType.Int)),
            };
        }

        public static List<BoogieExpr> GetDefaultArguments()
        {
            return new List<BoogieExpr>()
            {
                new BoogieIdentifierExpr("this"),
                new BoogieIdentifierExpr("msgsender_MSG"),
                new BoogieIdentifierExpr("msgvalue_MSG"),
            };
        }
    }
}
