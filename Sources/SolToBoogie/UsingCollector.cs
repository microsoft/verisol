// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using SolidityAST;
    using BoogieAST;
    using System.Collections.Generic;
    using System;
    using System.Diagnostics;

    /**
     * Collect all using definitions and put them in the translator context.
     */
    public class UsingCollector : BasicASTVisitor
    {
        private TranslatorContext context;
        private ContractDefinition currentContract;

        public UsingCollector(TranslatorContext context)
        {
            this.context = context;
        }

        public override bool Visit(ContractDefinition node)
        {
            currentContract = node;
            context.UsingMap[currentContract] = new Dictionary<UserDefinedTypeName, TypeName>();
            return true;
        }

        public override void EndVisit(ContractDefinition node)
        {
            currentContract = null;
        }

        public override bool Visit(UsingForDirective node)
        {
            if (node.TypeName is UserDefinedTypeName userType)
            {
                Debug.Assert(!userType.TypeDescriptions.IsContract(), $"VeriSol does not support using A for B where B is a contract name, found {userType.ToString()}");
            }
            context.UsingMap[currentContract][node.LibraryName] = node.TypeName;
            return true;
        }
    }
}
