
namespace SolToBoogie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using BoogieAST;
    using SolidityAST;
 
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
                    case "int256":
                        return BoogieType.Int;
                    case "bool":
                        return BoogieType.Bool;
                    case "string":
                        return BoogieType.Int;
                    case "address":
                        return BoogieType.Ref;
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
                    builder.Append(varDecl.TypeName).Append(", ");
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
                StringBuilder builder = new StringBuilder();
                builder.Append(function.Name).Append("(");
                if (function.Parameters.Parameters != null && function.Parameters.Parameters.Count > 0)
                {
                    foreach (VariableDeclaration varDecl in function.Parameters.Parameters)
                    {
                        builder.Append(varDecl.TypeName).Append(", ");
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
                        if (typeString.StartsWith("string") || typeString.StartsWith("literal_string"))
                        {
                            typeString = "string";
                        }
                        Debug.Assert(typeString.Equals("string") || typeString.Equals("uint256") || typeString.Equals("int256")
                                || typeString.Equals("address"));
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
            else if (typeString.Equals("address"))
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

        public static BoogieAssertCmd GenerateSourceInfoAnnotation(ASTNode node, TranslatorContext context)
        {
            List<BoogieAttribute> attributes = new List<BoogieAttribute>()
            {
                new BoogieAttribute("first"),
                new BoogieAttribute("sourceFile", "\"" + context.GetAbsoluteSourcePathOfASTNode(node) + "\""),
                new BoogieAttribute("sourceLine", context.GetLineNumberOfASTNode(node)),
            };
            BoogieAssertCmd assertCmd = new BoogieAssertCmd(new BoogieLiteralExpr(true), attributes);
            return assertCmd;
        }
    }
}
