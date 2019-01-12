// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolidityCFG
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using SolidityAST;

    public class CFGBuilder : BasicASTVisitor
    {
        private CFGNodeFactory factory;

        private FunctionCFG currentCFG;

        private CFGNode currentNode;

        public static FunctionCFG BuildFunctionCFG(CFGNodeFactory factory, FunctionDefinition func)
        {
            FunctionCFG CFG = new FunctionCFG();
            CFG.Entry = factory.MakeNode();
            CFG.Exit = factory.MakeNode();
            CFGBuilder builder = new CFGBuilder(factory, CFG);
            builder.AppendControlFlow(func);
            ConnectControlFlow(builder.currentNode, CFG.Exit);
            return CFG;
        }

        private static void ConnectControlFlow(CFGNode from, CFGNode to)
        {
            Debug.Assert(from != null);
            Debug.Assert(to != null);
            to.Entries.Add(from);
            from.Exits.Add(to);
        }

        private CFGBuilder(CFGNodeFactory factory, FunctionCFG currentCFG)
        {
            this.factory = factory;
            this.currentCFG = currentCFG;
            this.currentNode = currentCFG.Entry;
        }

        private void AppendControlFlow(ASTNode astNode)
        {
            astNode.Accept(this);
        }

        private CFGNode CreateControlFlow(CFGNode entry, ASTNode astNode)
        {
            CFGNode temp = currentNode;
            currentNode = entry;
            AppendControlFlow(astNode);
            CFGNode exit = currentNode;
            currentNode = temp;
            return exit;
        }

        private List<CFGNode> SplitControlFlow(int num)
        {
            Debug.Assert(num > 0, $"Invalid split number: {num}");
            List<CFGNode> nodes = new List<CFGNode>();
            for (int i = 0; i < num; ++i)
            {
                CFGNode node = factory.MakeNode();
                nodes.Add(node);
                ConnectControlFlow(currentNode, node);
            }
            // set to null because we don't know what is the current node at this point
            currentNode = null;
            return nodes;
        }

        private void MergeControlFlow(List<CFGNode> nodes, CFGNode destination = null)
        {
            Debug.Assert(nodes.Count > 1, $"Invalid merge number: {nodes.Count}");
            if (destination == null)
            {
                destination = factory.MakeNode();
            }
            foreach (CFGNode node in nodes)
            {
                if (node != destination)
                {
                    ConnectControlFlow(node, destination);
                }
            }
            currentNode = destination;
        }

        protected override bool CommonVisit(ASTNode astNode)
        {
            if (astNode is Assignment)
            {
                currentNode.Block.AddExpression((Assignment) astNode);
            }
            return true;
        }

        public override bool Visit(BinaryOperation astNode)
        {
            switch(astNode.Operator)
            {
                case "==":
                case "!=":
                case ">=":
                case "<=":
                case ">":
                case "<":
                    currentNode.Block.SetPredicate(astNode);
                    break;
                case "||":
                    {
                        AppendControlFlow(astNode.LeftExpression);
                        List<CFGNode> nodes = SplitControlFlow(2);
                        nodes[1] = CreateControlFlow(nodes[1], astNode.RightExpression);
                        MergeControlFlow(nodes, nodes[0]);
                    }
                    break;
                case "&&":
                    {
                        AppendControlFlow(astNode.LeftExpression);
                        List<CFGNode> nodes = SplitControlFlow(2);
                        nodes[0] = CreateControlFlow(nodes[0], astNode.RightExpression);
                        MergeControlFlow(nodes, nodes[1]);
                    }
                    break;
                default:
                    throw new SystemException($"Unknown operator: {astNode.Operator}");
            }
            return false;
        }

        public override bool Visit(Conditional astNode)
        {
            astNode.Condition.Accept(this);
            List<CFGNode> nodes = SplitControlFlow(2);

            // create the true assignment
            nodes[0] = CreateControlFlow(nodes[0], astNode.TrueExpression);

            // create the false assignment
            nodes[1] = CreateControlFlow(nodes[1], astNode.FalseExpression);
            MergeControlFlow(nodes);
            return false;
        }

        public override bool Visit(IfStatement astNode)
        {
            if (astNode.Condition is Identifier)
            {
                Identifier identifier = (Identifier)astNode.Condition;
                currentNode.Block.SetPredicate(identifier);
            }
            else if (astNode.Condition is BinaryOperation)
            {
                astNode.Condition.Accept(this);
            }
            else
            {
                throw new SystemException($"Unknown if condition: {astNode.Condition}");
            }

            List<CFGNode> nodes = SplitControlFlow(2);
            nodes[0] = CreateControlFlow(nodes[0], astNode.TrueBody);
            if (astNode.FalseBody != null)
            {
                nodes[1] = CreateControlFlow(nodes[1], astNode.FalseBody);
                MergeControlFlow(nodes);
            }
            else
            {
                MergeControlFlow(nodes, nodes[1]);
            }
            return false;
        }

    }
}
