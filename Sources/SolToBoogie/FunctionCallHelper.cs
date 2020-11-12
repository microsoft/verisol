using System;
using System.Collections.Generic;
using System.Linq;
using SolidityAST;

namespace SolToBoogie
{
    public class FunctionCallHelper
    {
        public static bool IsDynamicDispatching(FunctionCall node)
        {
            return node.Expression is Identifier;
        }

        public static bool IsTypeCast(FunctionCall node)
        {
            return node.Kind.Equals("typeConversion");
        }
        
        public static bool IsStaticDispatching(TranslatorContext context, FunctionCall node)
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
        
        public static ContractDefinition GetStaticDispatchingContract(TranslatorContext context, FunctionCall node)
        {
            VeriSolAssert(node.Expression is MemberAccess);
            MemberAccess memberAccess = node.Expression as MemberAccess;

            Identifier contractId = memberAccess.Expression as Identifier;
            VeriSolAssert(contractId != null, $"Unknown contract name: {memberAccess.Expression}");

            ContractDefinition contract = context.GetASTNodeById(contractId.ReferencedDeclaration) as ContractDefinition;
            VeriSolAssert(contract != null);
            return contract;
        }

        public static ContractDefinition GetUsedLibrary(TranslatorContext context, ContractDefinition curContract,
            MemberAccess memberAccess)
        {
            FunctionDefinition fnDef = context.GetASTNodeById(memberAccess.ReferencedDeclaration.Value) as FunctionDefinition;

            if (fnDef == null || !context.FunctionToContractMap.ContainsKey(fnDef))
            {
                return null;
            }
            
            ContractDefinition fnContract = context.GetContractByFunction(fnDef);

            Dictionary<ContractDefinition, UserDefinedTypeName> usingLibs = new Dictionary<ContractDefinition, UserDefinedTypeName>();
            List<int> contractIds = new List<int>();
            contractIds.Add(curContract.Id);
            contractIds.AddRange(curContract.LinearizedBaseContracts);
            
            foreach (int id in contractIds)
            {
                ContractDefinition baseContract = context.GetASTNodeById(id) as ContractDefinition;

                foreach (UserDefinedTypeName typeName in context.UsingMap[baseContract].Keys)
                {
                    ContractDefinition libDef = context.GetASTNodeById(typeName.ReferencedDeclaration) as ContractDefinition;
                    if (!usingLibs.ContainsKey(libDef))
                    {
                        usingLibs[libDef] = typeName;
                    }
                }
            }

            if (usingLibs.ContainsKey(fnContract))
            {
                if (memberAccess.Expression.TypeDescriptions.IsContract() &&
                    !memberAccess.Expression.TypeDescriptions.IsArray())
                {
                    //search sub-types
                    UserDefinedTypeName libType = usingLibs[fnContract];
                    String contractName = memberAccess.Expression.TypeDescriptions.TypeString.Split(" ")[1];
                    ContractDefinition contractDef = context.GetContractByName(contractName);
                    HashSet<ContractDefinition> usedBy = context.UsingMap[curContract][libType].FindAll(t =>
                        t is UserDefinedTypeName u &&
                        context.GetASTNodeById(u.ReferencedDeclaration) is ContractDefinition).Select(c =>
                        context.GetASTNodeById(((UserDefinedTypeName) (c))
                            .ReferencedDeclaration) as ContractDefinition).ToHashSet();

                    bool usesLib = usedBy.Contains(contractDef);

                    foreach (int id in contractDef.LinearizedBaseContracts)
                    {
                        ContractDefinition baseContract = context.GetASTNodeById(id) as ContractDefinition;
                        if (usedBy.Contains(baseContract))
                        {
                            usesLib = true;
                        }
                    }

                    return usesLib ? fnContract : null;
                }
                else
                {
                    return fnContract;
                }
            }

            return null;
        }
        
        public static bool IsUsingBasedLibraryCall(TranslatorContext context, ContractDefinition curContract, MemberAccess memberAccess)
        {
            // since we only permit "using A for B" for non-contract types
            // this is sufficient, but not necessary in general since non
            // contracts (including libraries) do not have support methods
            return GetUsedLibrary(context, curContract, memberAccess) != null;
        }
        
        public static ContractDefinition IsLibraryFunctionCall(TranslatorContext context, FunctionCall node)
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
        
        public static bool IsExternalFunctionCall(TranslatorContext context, FunctionCall node)
        {
            if (node.Expression is MemberAccess memberAccess)
            {
                if (memberAccess.Expression is Identifier identifier)
                {
                    if (identifier.Name == "this")
                    {
                        return true;
                    }

                    if (identifier.Name == "super")
                    {
                        return true;
                    }

                    if (!context.HasASTNodeId(identifier.ReferencedDeclaration))
                    {
                        return true;
                    }

                    var contract = context.GetASTNodeById(identifier.ReferencedDeclaration) as ContractDefinition;
                    if (contract == null)
                    { 
                        return true;
                    }
                    }
                else if (memberAccess.Expression is MemberAccess structSelect)
                {
                    //a.b.c.foo(...)
                    //TODO: do we want to check that the contract the struct variable is declared
                    // is not in the "context"? Why this isn't done for IndexAccess?
                    return true;
                }
                else if (memberAccess.Expression.ToString().Equals("msg.sender"))
                {
                    // calls can be of the form "msg.sender.call()" or "msg.sender.send()" or "msg.sender.transfer()"
                    return true;
                }
                else if (memberAccess.Expression is FunctionCall)
                {
                    // TODO: example?
                    return true;
                } else if (memberAccess.Expression is IndexAccess)
                {
                    //a[i].foo(..)
                    return true;
                }
                else if (memberAccess.Expression is TupleExpression)
                {
                    return true;
                }
            }
            return false;
        }
        
        public static bool IsImplicitFunc(FunctionCall node)
        {
            return
                IsKeccakFunc(node) ||
                IsAbiEncodePackedFunc(node) ||
                IsGasleft(node) ||
                IsTypeCast(node) ||
                IsStructConstructor(node) ||
                IsContractConstructor(node) ||
                IsAbiFunction(node);
        }
        
        public static bool IsAbiFunction(FunctionCall node)
        {
            if (node.Expression is MemberAccess member)
            {
                if (member.Expression is Identifier ident)
                {
                    if (ident.Name.Equals("abi"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsContractConstructor(FunctionCall node)
        {
            return node.Expression is NewExpression;
        }

        public static bool IsStructConstructor(FunctionCall node)
        {
            return node.Kind.Equals("structConstructorCall");
        }

        public static bool IsGasleft(FunctionCall node)
        {
            if (node.Expression is Identifier ident)
            {
                return ident.Name.Equals("gasleft") && node.Arguments.Count == 0;
            }

            return false;
        }
         
        public static bool IsKeccakFunc(FunctionCall node)
        {
            if (node.Expression is Identifier ident)
            {
                return ident.Name.Equals("keccak256");
            }
            return false;
        }
        
        public static bool IsBuiltInTransferFunc(string functionName, FunctionCall node)
        {
            if (!functionName.Equals("transfer")) return false;
            if (node.Expression is MemberAccess member)
            {
                if (member.Expression.TypeDescriptions.IsAddress())
                    return true;
            }
            return false;
        }

        public static bool IsAbiEncodePackedFunc(FunctionCall node)
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

        public static bool IsAssert(FunctionCall node)
        {
            var functionName = TransUtils.GetFuncNameFromFuncCall(node);
            return functionName.Equals("assert");
        }

        public static bool IsRequire(FunctionCall node)
        {
            var functionName = TransUtils.GetFuncNameFromFuncCall(node);
            return functionName.Equals("require");
        }

        public static bool IsRevert(FunctionCall node)
        {
            var functionName = TransUtils.GetFuncNameFromFuncCall(node);
            return functionName.Equals("revert");
        }

        public static bool IsLowLevelCall(FunctionCall node)
        {
            var functionName = TransUtils.GetFuncNameFromFuncCall(node);
            return functionName.Equals("call");
        }

        public static bool isDelegateCall(FunctionCall node)
        {
            var functionName = TransUtils.GetFuncNameFromFuncCall(node);
            return functionName.Equals("delegatecall");
        }

        public static bool isSend(FunctionCall node)
        {
            var functionName = TransUtils.GetFuncNameFromFuncCall(node);
            return functionName.Equals("send");
        }
        
        private static void VeriSolAssert(bool cond, string message = "")
        {
            if (!cond)
            {
                throw new Exception ($"{message}....");
            }
        }
    }
}