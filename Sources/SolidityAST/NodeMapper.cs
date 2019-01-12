// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolidityAST
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class NodeMapper : BasicASTVisitor
    {
        private readonly ASTNode root;

        private Dictionary<int, ASTNode> idToNodeMap;

        public NodeMapper(ASTNode root)
        {
            this.root = root;
            idToNodeMap = new Dictionary<int, ASTNode>();
            root.Accept(this);
        }

        public Dictionary<int, ASTNode> GetIdToNodeMap()
        {
            return idToNodeMap;
        }

        protected override bool CommonVisit(ASTNode node)
        {
            int id = node.Id;
            Debug.Assert(!idToNodeMap.ContainsKey(id), $"Duplicated node ID: {id}");
            idToNodeMap[id] = node;
            return true;
        }
    }
 }
