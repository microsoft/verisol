// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolidityCFG
{
    using System.Collections.Generic;
    using System.Text;
    using SolidityAST;

    public class CFG
    {
    }

    public class FunctionCFG
    {
        public CFGNode Entry { get; set; }

        public CFGNode Exit { get; set; }

        public FunctionCFG()
        {
            Entry = null;
            Exit = null;
        }
    }

    public class CFGNode
    {
        public int Id { get; private set; }

        public List<CFGNode> Entries { get; private set; }
        
        public List<CFGNode> Exits { get; private set; }
        
        public BasicBlock Block { get; private set; }

        public CFGNode(int id)
        {
            Id = id;
            Entries = new List<CFGNode>();
            Exits = new List<CFGNode>();
            Block = new BasicBlock();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Id: ").Append(Id);
            builder.Append(", Entries: [");
            foreach (CFGNode entry in Entries)
            {
                builder.Append(entry.Id).Append(", ");
            }
            if (Entries.Count > 0)
            {
                builder.Length -= 2;
            }

            builder.Append("], Exits: [");
            foreach (CFGNode exit in Exits)
            {
                builder.Append(exit.Id).Append(", ");
            }
            if (Exits.Count > 0)
            {
                builder.Length -= 2;
            }
            builder.Append("], Block: ").Append(Block);
            return builder.ToString();
        }
    }

    public class BasicBlock
    {
        private List<Expression> expressions;

        // predicate based on which to split the control flow
        private Expression predicate;

        public BasicBlock()
        {
            expressions = new List<Expression>();
            predicate = null;
        }

        public List<Expression> GetExpressions()
        {
            return expressions;
        }

        public void AddExpression(Expression expression)
        {
            expressions.Add(expression);
        }

        public Expression GetPredicate()
        {
            return predicate;
        }

        public void SetPredicate(Expression predicate)
        {
            this.predicate = predicate;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            foreach (Expression expression in expressions)
            {
                builder.Append(expression).Append(", ");
            }
            if (expressions.Count > 0)
            {
                builder.Length -= 2;
            }
            builder.Append("]");
            if (predicate != null)
            {
                builder.Append(" Pred: ").Append(predicate);
            }
            return builder.ToString();
        }
    }

}
