// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Linq;

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
            context.UsingMap[currentContract] = new Dictionary<UserDefinedTypeName, List<TypeName>>();
            return true;
        }

        public override void EndVisit(ContractDefinition node)
        {
            currentContract = null;
        }

        public override bool Visit(UsingForDirective node)
        {
            if (context.UsingMap[currentContract].ContainsKey(node.LibraryName))
            {
                context.UsingMap[currentContract][node.LibraryName].Add(node.TypeName);
            }
            else
            {
                context.UsingMap[currentContract][node.LibraryName] = new List<TypeName>() {node.TypeName};
            }
            return true;
        }
    }
}
