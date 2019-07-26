// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System;
    using System.Collections.Generic;
    using BoogieAST;
    using SolidityAST;

    public class HoudiniHelper
    {
        // returns a map from integer id to an atomic Boogie predicate
        public static Dictionary<int, BoogieExpr> GenerateHoudiniVarMapping(ContractDefinition contract, TranslatorContext context)
        {
            Dictionary<int, BoogieExpr> ret = new Dictionary<int, BoogieExpr>();
            HashSet<VariableDeclaration> stateVars = context.GetVisibleStateVarsByContract(contract);

            // collect all state variables of type address
            List<VariableDeclaration> addressVariables = new List<VariableDeclaration>();
            foreach (VariableDeclaration stateVar in stateVars)
            {
                if (stateVar.TypeName is ElementaryTypeName elementaryType)
                {
                    if (elementaryType.TypeDescriptions.TypeString.Equals("address") ||
                        elementaryType.TypeDescriptions.TypeString.Equals("address payable"))
                    {
                        addressVariables.Add(stateVar);
                    }
                }
            }

            int id = 0;

            // equaility and disequality to null
            foreach (VariableDeclaration addressVar in addressVariables)
            {
                BoogieExpr lhs = GetBoogieExprOfStateVar(addressVar, context);
                BoogieExpr rhs = new BoogieIdentifierExpr("null");
                BoogieExpr equality = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, lhs, rhs);
                ret[++id] = equality;
                BoogieExpr disequality = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, lhs, rhs);
                ret[++id] = disequality;
            }

            // pair-wise equality and disequality
            for (int i = 0; i < addressVariables.Count; ++i)
            {
                BoogieExpr lhs = GetBoogieExprOfStateVar(addressVariables[i], context);
                for (int j = i + 1; j < addressVariables.Count; ++j)
                {
                    BoogieExpr rhs = GetBoogieExprOfStateVar(addressVariables[j], context);
                    BoogieExpr equality = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, lhs, rhs);
                    ret[++id] = equality;
                    BoogieExpr disequality = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, lhs, rhs);
                    ret[++id] = disequality;
                }
            }

            // PrintHoudiniCandidateMap(ret);
            return ret;
        }

        private static BoogieMapSelect GetBoogieExprOfStateVar(VariableDeclaration varDecl, TranslatorContext context)
        {
            string name = TransUtils.GetCanonicalStateVariableName(varDecl, context);
            BoogieMapSelect mapSelect = new BoogieMapSelect(new BoogieIdentifierExpr(name), new BoogieIdentifierExpr("this"));
            return mapSelect;
        }

        private static void PrintHoudiniCandidateMap(Dictionary<int, BoogieExpr> map)
        {
            Console.WriteLine("Houdini Candidates:");
            foreach (int key in map.Keys)
            {
                Console.WriteLine($"{key} --> {map[key]}");
            }
            Console.WriteLine();
        }
    }
}
