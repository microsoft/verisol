// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace BoogieAST
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class BoogieAST
    {
        private BoogieASTNode root;

        public BoogieAST(BoogieASTNode root)
        {
            this.root = root;
        }

        public BoogieASTNode GetRoot()
        {
            return root;
        }
    }

    public abstract class BoogieASTNode
    {
    }

    public abstract class BoogieDeclaration : BoogieASTNode
    {
        public List<BoogieAttribute> Attributes { get; set; }
    }

    public class BoogieProgram : BoogieASTNode
    {
        public List<BoogieDeclaration> Declarations { get; set; }

        public BoogieProgram()
        {
            Declarations = new List<BoogieDeclaration>();
        }

        public void AddDeclaration(BoogieDeclaration declaration)
        {
            Declarations.Add(declaration);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (BoogieDeclaration declaration in Declarations)
            {
                builder.Append(declaration);
            }
            return builder.ToString();
        }
    }

    public abstract class BoogieNamedDecl : BoogieDeclaration
    {
        public string Name { get; set; }
    }

    public class BoogieTypeCtorDecl : BoogieNamedDecl
    {
        private BoogieType EquivType;
        
        public BoogieTypeCtorDecl(string name)
        {
            this.Name = name;
            this.EquivType = null;
        }
        
        public BoogieTypeCtorDecl(string name, BoogieType equivType)
        {
            this.Name = name;
            this.EquivType = equivType;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (EquivType == null)
            {
                builder.Append("type ").Append(Name).AppendLine(";");
            }
            else
            {
                builder.Append("type ").Append(Name).Append(" = ").Append(EquivType).AppendLine(";");
            }
            
            return builder.ToString();
        }
    }

    public class BoogieAttribute : BoogieASTNode
    {
        public string Key { get; private set; }

        public List<object> Values { get; private set; }

        public BoogieAttribute(string key)
        {
            this.Key = key;
            this.Values = new List<object>();
        }

        public BoogieAttribute(string key, int value)
        {
            this.Key = key;
            this.Values = new List<object>() { value };
        }

        public BoogieAttribute(string key, bool value)
        {
            this.Key = key;
            this.Values = new List<object>() { value };
        }

        public BoogieAttribute(string key, string value)
        {
            this.Key = key;
            this.Values = new List<object>() { value };
        }

        public BoogieAttribute(string key, List<object> values)
        {
            this.Key = key;
            this.Values = values;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{:").Append(Key);
            foreach (object value in Values)
            {
                builder.Append(" ");
                if (value.Equals(true))
                {
                    builder.Append("true");
                }
                else if (value.Equals(false))
                {
                    builder.Append("false");
                }
                else
                {
                    builder.Append(value);
                }
            }
            builder.Append("}");
            return builder.ToString();
        }
    }

    public abstract class BoogieDeclWithFormals : BoogieNamedDecl
    {
        public List<BoogieVariable> InParams;

        public List<BoogieVariable> OutParams;

        public List<BoogieGlobalVariable> ModSet;
     }

    public class BoogieProcedure : BoogieDeclWithFormals
    {
        List<BoogieExpr> preConditions;
        List<BoogieExpr> postConditions;
        public BoogieProcedure(string name, List<BoogieVariable> inParams, List<BoogieVariable> outParams, List<BoogieAttribute> attributes = null, List<BoogieGlobalVariable> modSet = null, List<BoogieExpr> pre = null, List<BoogieExpr> post = null)
        {
            this.Name = name;
            this.InParams = inParams;
            this.OutParams = outParams;
            this.Attributes = attributes;
            this.ModSet = modSet;
            this.preConditions = pre;
            this.postConditions = post;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("procedure ");
            if (Attributes != null)
            {
                foreach (BoogieAttribute attribute in Attributes)
                {
                    builder.Append(attribute).Append(" ");
                }
            }
            builder.Append(Name).Append("(");
            if (InParams.Count > 0)
            {
                foreach (BoogieVariable inParam in InParams)
                {
                    builder.Append(inParam).Append(", ");
                }
                builder.Length -= 2;
            }
            builder.Append(")");
            if (OutParams.Count > 0)
            {
                builder.Append(" returns (");
                foreach (BoogieVariable outParam in OutParams)
                {
                    builder.Append(outParam).Append(", ");
                }
                builder.Length -= 2;
                builder.Append(")");
            }
            builder.AppendLine(";");
            if (ModSet != null)
            {
                foreach(var m in ModSet)
                {
                    builder.AppendLine($"modifies {m.Name};");
                }
            }
            if (preConditions != null)
            {
                foreach (var e in preConditions)
                {
                    builder.AppendLine($"requires({e.ToString()});");
                }
            }
            if (postConditions != null)
            {
                foreach (var e in postConditions)
                {
                    builder.AppendLine($"ensures({e.ToString()});");
                }
            }
            return builder.ToString();
        }
        public void AddPreConditions(List<BoogieExpr> pres)
        {
            if (preConditions == null)
                preConditions = new List<BoogieExpr>();
            preConditions.AddRange(pres);
        }
        public void AddPostConditions(List<BoogieExpr> posts)
        {
            if (postConditions == null)
                postConditions = new List<BoogieExpr>();
            postConditions.AddRange(posts);
        }

    }

    public class BoogieFunction : BoogieDeclWithFormals
    {
        public BoogieFunction(string name, List<BoogieVariable> inParams, List<BoogieVariable> outParams, List<BoogieAttribute> attributes = null)
        {
            this.Name = name;
            this.InParams = inParams;
            this.OutParams = outParams;
            this.Attributes = attributes;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("function ");
            if (Attributes != null)
            {
                foreach (BoogieAttribute attribute in Attributes)
                {
                    builder.Append(attribute).Append(" ");
                }
            }
            builder.Append(Name).Append("(");
            if (InParams.Count > 0)
            {
                foreach (BoogieVariable inParam in InParams)
                {
                    builder.Append(inParam).Append(", ");
                }
                builder.Length -= 2;
            }
            builder.Append(")");
            if (OutParams.Count > 0)
            {
                builder.Append(" returns (");
                foreach (BoogieVariable outParam in OutParams)
                {
                    builder.Append(outParam).Append(", ");
                }
                builder.Length -= 2;
                builder.Append(")");
            }
            builder.AppendLine(";");
            return builder.ToString();
        }

    }

    public class BoogieAxiom : BoogieDeclaration
    {
        BoogieExpr BExpr;
        public BoogieAxiom(BoogieExpr bExpr)
        {
            BExpr = bExpr;
        }

        public override string ToString()
        {
            return $"\naxiom({BExpr.ToString()});\n";
        }
    }

    public class BoogieImplementation : BoogieDeclWithFormals
    {
        public List<BoogieVariable> LocalVars { get; set; }

        public BoogieStmtList StructuredStmts { get; set; }

        public BoogieImplementation(string name, List<BoogieVariable> inParams, List<BoogieVariable> outParams, List<BoogieVariable> localVars, BoogieStmtList stmts, List<BoogieAttribute> attributes = null)
        {
            this.Name = name;
            this.InParams = inParams;
            this.OutParams = outParams;
            this.LocalVars = localVars;
            this.StructuredStmts = stmts;
            this.Attributes = attributes;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("implementation ");
            if (Attributes != null)
            {
                foreach (BoogieAttribute attribute in Attributes)
                {
                    builder.Append(attribute).Append(" ");
                }
            }
                
            builder.Append(Name).Append("(");
            if (InParams.Count > 0)
            {
                foreach (BoogieVariable inParam in InParams)
                {
                    builder.Append(inParam).Append(", ");
                }
                builder.Length -= 2;
            }
            builder.Append(")");
            if (OutParams.Count > 0)
            {
                builder.Append(" returns (");
                foreach (BoogieVariable outParam in OutParams)
                {
                    builder.Append(outParam).Append(", ");
                }
                builder.Length -= 2;
                builder.Append(")");
            }
            builder.AppendLine().AppendLine("{");
            // local variables
            foreach (BoogieVariable localVar in LocalVars)
            {
                builder.Append("var ").Append(localVar).AppendLine(";");
            }
            // statements
            builder.Append(StructuredStmts);
            builder.AppendLine("}").AppendLine();
            return builder.ToString();
        }
    }

    public abstract class BoogieType : BoogieASTNode
    {
        public static readonly BoogieType Int = new BoogieBasicType(BoogieBasicType.SimpleType.INT);
        public static readonly BoogieType Bool = new BoogieBasicType(BoogieBasicType.SimpleType.BOOL);
        public static readonly BoogieType Ref = new BoogieCtorType("Ref");
    }

    public class BoogieBasicType : BoogieType
    {
        public enum SimpleType
        {
            BOOL,
            INT,
        }

        public readonly SimpleType Type;

        public BoogieBasicType(SimpleType type)
        {
            this.Type = type;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case SimpleType.INT:
                    return "int";
                case SimpleType.BOOL:
                    return "bool";
                default:
                    throw new SystemException($"Unknown simple type: {Type}");
            }
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is BoogieBasicType basicType)
            {
                return Type == basicType.Type;
            }
            return false;
        }
    }

    public class BoogieCtorType : BoogieType
    {
        public string Name { get; set; }

        public BoogieCtorType(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is BoogieCtorType ctorType)
            {
                return Name.Equals(ctorType.Name);
            }
            return false;
        }
    }

    public class BoogieMapType : BoogieType
    {
        public List<BoogieType> Arguments { get; set; }

        public BoogieType Result { get; set; }

        public BoogieMapType(List<BoogieType> arguments, BoogieType result)
        {
            Debug.Assert(arguments != null && arguments.Count > 0);
            this.Arguments = arguments;
            this.Result = result;
        }

        public BoogieMapType(BoogieType argument, BoogieType result)
        {
            this.Arguments = new List<BoogieType>();
            this.Arguments.Add(argument);
            this.Result = result;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            foreach (BoogieType argType in Arguments)
            {
                builder.Append(argType).Append(", ");
            }
            builder.Length -= 2;
            builder.Append("]");
            builder.Append(Result);
            return builder.ToString();
        }
    }

    public class BoogieTypedIdent : BoogieASTNode
    {
        public string Name { get; set; }

        public BoogieType Type { get; set; }

        public BoogieTypedIdent(string name, BoogieType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Name).Append(": ").Append(Type);
            return builder.ToString();
        }
    }

    public abstract class BoogieVariable : BoogieNamedDecl
    {
        public BoogieTypedIdent TypedIdent { get; set; }

        public override string ToString()
        {
            return TypedIdent.ToString();
        }
    }

    public class BoogieConstant : BoogieVariable
    {
        public readonly bool Unique;

        public BoogieConstant(BoogieTypedIdent typedIdent, bool unique = false)
        {
            this.TypedIdent = typedIdent;
            this.Unique = unique;
            this.Name = typedIdent.Name;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("const ");
            if (Unique)
            {
                builder.Append("unique ");
            }
            if (Attributes != null)
            {
                foreach (BoogieAttribute attribute in Attributes)
                {
                    builder.Append(attribute).Append(" ");
                }
            }
            builder.Append(TypedIdent).AppendLine(";");
            return builder.ToString();
        }
    }

    public class BoogieGlobalVariable : BoogieVariable
    {
        public BoogieGlobalVariable(BoogieTypedIdent typedIdent)
        {
            this.TypedIdent = typedIdent;
            this.Name = typedIdent.Name;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("var ");
            if (Attributes != null)
            {
                foreach (BoogieAttribute attribute in Attributes)
                {
                    builder.Append(attribute).Append(" ");
                }
            }
            builder.Append(TypedIdent).AppendLine(";");
            return builder.ToString();
        }
    }

    public class BoogieLocalVariable : BoogieVariable
    {
        public BoogieLocalVariable(BoogieTypedIdent typedIdent)
        {
            this.TypedIdent = typedIdent;
            this.Name = typedIdent.Name;
        }
    }

    public class BoogieFormalParam : BoogieVariable
    {
        public BoogieFormalParam(BoogieTypedIdent typedIdent)
        {
            this.TypedIdent = typedIdent;
            this.Name = typedIdent.Name;
        }
    }

    public class BoogieStmtList : BoogieASTNode
    {
        public List<BoogieBigBlock> BigBlocks { get; private set; }

        public BoogieStmtList()
        {
            BigBlocks = new List<BoogieBigBlock>();
            BigBlocks.Add(new BoogieBigBlock());
        }

        public int StatementCount()
        {
            Debug.Assert(BigBlocks.Count == 1);
            return BigBlocks[0].SimpleCmds.Count;
        }

        public static BoogieStmtList MakeSingletonStmtList(BoogieCmd cmd)
        {
            BoogieStmtList stmtList = new BoogieStmtList();
            stmtList.BigBlocks[0].AddStatement(cmd);
            return stmtList;
        }

        public void AddStatement(BoogieCmd cmd)
        {
            Debug.Assert(BigBlocks.Count == 1);
            BigBlocks[0].AddStatement(cmd);
        }

        public void AppendStmtList(BoogieStmtList stmtList)
        {
            Debug.Assert(stmtList.BigBlocks.Count == 1);
            foreach (BoogieCmd cmd in stmtList.BigBlocks[0].SimpleCmds)
            {
                BigBlocks[0].AddStatement(cmd);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (BoogieBigBlock bigBlock in BigBlocks)
            {
                builder.Append(bigBlock);
            }
            return builder.ToString();
        }
    }

    public class BoogieBigBlock : BoogieASTNode
    {
        public string Label { get; private set; }

        public List<BoogieCmd> SimpleCmds { get; set; }

        public BoogieBigBlock(string label = null)
        {
            SimpleCmds = new List<BoogieCmd>();
        }

        public void AddStatement(BoogieCmd statement)
        {
            SimpleCmds.Add(statement);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (BoogieCmd cmd in SimpleCmds)
            {
                builder.Append(cmd);
            }
            return builder.ToString();
        }
    }

    public abstract class BoogieCmd : BoogieASTNode
    {
        public List<BoogieAttribute> Attributes { get; set; }
    }

    public class BoogieAssignCmd : BoogieCmd
    {
        public BoogieExpr Lhs { get; set; }

        public BoogieExpr Rhs { get; set; }

        public BoogieAssignCmd(BoogieExpr lhs, BoogieExpr rhs)
        {
            this.Lhs = lhs;
            this.Rhs = rhs;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Lhs).Append(" := ").Append(Rhs).AppendLine(";");
            return builder.ToString();
        }
    }

    public class BoogieCallCmd : BoogieCmd
    {
        public string Callee { get; set; }

        public List<BoogieExpr> Ins { get; set; }

        public List<BoogieIdentifierExpr> Outs { get; set; }

        public BoogieCallCmd(string callee, List<BoogieExpr> ins, List<BoogieIdentifierExpr> outs)
        {
            this.Callee = callee;
            this.Ins = ins;
            this.Outs = outs;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("call ");

            if (Attributes != null)
            {
                foreach (BoogieAttribute attr in Attributes)
                {
                    builder.Append($" {{:{attr.Key} {attr.Values.FirstOrDefault()}}} ");
                }
            }

            if (Outs != null && Outs.Count > 0)
            {
                foreach (BoogieIdentifierExpr outVar in Outs)
                {
                    builder.Append(outVar).Append(", ");
                }
                builder.Length -= 2;
                builder.Append(" := ");
            }
            builder.Append(Callee).Append("(");
            if (Ins != null && Ins.Count > 0)
            {
                foreach (BoogieExpr inExpr in Ins)
                {
                    builder.Append(inExpr).Append(", ");
                }
                builder.Length -= 2;
            }
            builder.AppendLine(");");
            return builder.ToString();
        }
    }

    public abstract class BoogiePredicateCmd : BoogieCmd
    {
        public BoogieExpr Expr { get; set; }
    }

    public class BoogieAssertCmd : BoogiePredicateCmd
    {
        public BoogieAssertCmd(BoogieExpr expr, List<BoogieAttribute> attributes = null)
        {
            this.Expr = expr;
            this.Attributes = attributes;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("assert ");
            if (Attributes != null)
            {
                foreach (BoogieAttribute attribute in Attributes)
                {
                    builder.Append(attribute).Append(" ");
                }
            }
            builder.Append("(").Append(Expr).AppendLine(");");
            return builder.ToString();
        }
    }

    public class BoogieAssumeCmd : BoogiePredicateCmd
    {
        public BoogieAssumeCmd(BoogieExpr expr)
        {
            this.Expr = expr;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("assume (").Append(Expr).AppendLine(");");
            return builder.ToString();
        }
    }

    public class BoogieLoopInvCmd : BoogiePredicateCmd
    {
        public BoogieLoopInvCmd(BoogieExpr expr)
        {
            this.Expr = expr;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("invariant ").Append(Expr).AppendLine(";");
            return builder.ToString();
        }
    }

    public abstract class BoogieTransferCmd : BoogieCmd
    {
        // left empty
    }

    public class BoogieReturnCmd : BoogieTransferCmd
    {
        public override string ToString()
        {
            return "return;" + Environment.NewLine;
        }
    }

    public class BoogieReturnExprCmd : BoogieReturnCmd
    {
        public BoogieExpr Expr { get; set; }

        public BoogieReturnExprCmd(BoogieExpr expr)
        {
            this.Expr = expr;
            throw new NotSupportedException("return " + expr.ToString());
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("return ").Append(Expr).AppendLine(";");
            return builder.ToString();
        }
    }

    public class BoogieGotoCmd : BoogieTransferCmd
    {
        public List<string> LabelNames { get; set; }

        public BoogieGotoCmd(string labelName)
        {
            Debug.Assert(labelName != null);
            this.LabelNames = new List<string>()
            {
                labelName
            };
        }

        public BoogieGotoCmd(List<string> labelNames)
        {
            Debug.Assert(labelNames != null && labelNames.Count > 0);
            this.LabelNames = labelNames;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("goto ");
            foreach (string label in LabelNames)
            {
                builder.Append(label).Append(", ");
            }
            builder.Length -= 2;
            builder.AppendLine(";");
            return builder.ToString();
        }
    }

    public class BoogieHavocCmd : BoogieCmd
    {
        public List<BoogieIdentifierExpr> Vars { get; set; }

        public BoogieHavocCmd(BoogieIdentifierExpr ident)
        {
            this.Vars = new List<BoogieIdentifierExpr>();
            this.Vars.Add(ident);
        }

        public BoogieHavocCmd(List<BoogieIdentifierExpr> vars)
        {
            this.Vars = vars;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("havoc ");
            foreach (BoogieIdentifierExpr variable in Vars)
            {
                builder.Append(variable).Append(", ");
            }
            builder.Length -= 2;
            builder.AppendLine(";");
            return builder.ToString();
        }
    }

    public class BoogieSkipCmd : BoogieCmd
    {
        public string Comment { get; set; }

        public BoogieSkipCmd(string comment = null)
        {
            this.Comment = comment;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("// skip");
            if (Comment != null)
            {
                builder.Append(": ").Append(Comment);
            }
            builder.AppendLine();
            return builder.ToString();
        }
    }

    public class BoogieCommentCmd : BoogieCmd
    {
        public string Comment { get; set; }

        public BoogieCommentCmd(string comment)
        {
            this.Comment = comment;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("// ").AppendLine(Comment);
            return builder.ToString();
        }
    }

    public abstract class BoogieStructuredCmd : BoogieCmd
    {
    }
    
    public class BoogieWildcardExpr : BoogieExpr
    {
        public override string ToString()
        {
            return "*";
        }
    }

    public class BoogieIfCmd : BoogieStructuredCmd
    {
        public BoogieExpr Guard { get; set; }

        public BoogieStmtList ThenBody { get; set; }

        public BoogieStmtList ElseBody { get; set; }

        public BoogieIfCmd(BoogieExpr guard, BoogieStmtList thenBody, BoogieStmtList elseBody)
        {
            this.Guard = guard;
            this.ThenBody = thenBody;
            this.ElseBody = elseBody;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("if (").Append(Guard).AppendLine(") {");
            builder.Append(ThenBody);
            if (ElseBody == null)
            {
                builder.AppendLine("}");
            }
            else if (ElseBody.StatementCount() == 1 && ElseBody.BigBlocks[0].SimpleCmds[0] is BoogieIfCmd)
            {
                builder.Append("} else ").Append(ElseBody);
            }
            else
            {
                builder.AppendLine("} else {");
                builder.Append(ElseBody).AppendLine("}");
            }
            return builder.ToString();
        }
    }

    public class BoogieWhileCmd : BoogieStructuredCmd
    {
        public BoogieExpr Guard { get; set; }

        public List<BoogiePredicateCmd> Invariants { get; set; }

        public BoogieStmtList Body { get; set; }

        public BoogieWhileCmd(BoogieExpr guard, BoogieStmtList body, List<BoogieExpr> invariants = null)
        {
            this.Guard = guard;
            this.Body = body;
            this.Invariants =
                invariants == null ?
                new List<BoogiePredicateCmd>() :
                invariants.Select(x => new BoogieLoopInvCmd(x)).ToList<BoogiePredicateCmd>();
        }

        public BoogieWhileCmd(BoogieExpr guard, BoogieStmtList body, List<BoogiePredicateCmd> invariants)
        {
            this.Guard = guard;
            this.Body = body;
            this.Invariants = invariants;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("while (").Append(Guard).AppendLine(")");
            if (Invariants != null)
            {
                foreach (BoogiePredicateCmd predCmd in Invariants)
                {
                    builder.Append("  ").Append(predCmd);
                }
            }
            builder.AppendLine("{");
            builder.Append(Body);
            builder.AppendLine("}");
            return builder.ToString();
        }
    }

    public class BoogieBreakCmd : BoogieStructuredCmd
    {
        public string Label { get; set; }

        public BoogieBreakCmd(string label = null)
        {
            this.Label = label;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("break");
            if (Label != null)
            {
                builder.Append(" ").Append(Label);
            }
            builder.AppendLine(";");
            return builder.ToString();
        }
    }

    public class BoogieExpr : BoogieASTNode
    {
    }

    public class BoogieLiteralExpr : BoogieExpr
    {
        public object Val { get; private set; }

        public BoogieLiteralExpr(bool b)
        {
            this.Val = b;
        }

        public BoogieLiteralExpr(BigInteger num)
        {
            this.Val = num;
        }

        public override string ToString()
        {
            if (Val is bool)
            {
                return (bool)Val ? "true" : "false";
            }
            else
            {
                return Val.ToString();
            }
        }
    }

    public class BoogieIdentifierExpr : BoogieExpr
    {
        public string Name { get; set; }

        public BoogieIdentifierExpr(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class BoogieMapSelect : BoogieExpr
    {
        public BoogieExpr BaseExpr { get; set; }

        public List<BoogieExpr> Arguments { get; set; }

        public BoogieMapSelect(BoogieExpr baseExpr, List<BoogieExpr> arguments)
        {
            Debug.Assert(arguments != null && arguments.Count > 0);
            this.BaseExpr = baseExpr;
            this.Arguments = arguments;
        }

        public BoogieMapSelect(BoogieExpr baseExpr, BoogieExpr indexExpr)
        {
            this.BaseExpr = baseExpr; ;
            this.Arguments = new List<BoogieExpr>();
            this.Arguments.Add(indexExpr);
        }

        public override string ToString()
        {
            Debug.Assert(Arguments.Count >= 1);
            StringBuilder builder = new StringBuilder();
            builder.Append(BaseExpr).Append("[");
            foreach (BoogieExpr argument in Arguments)
            {
                builder.Append(argument).Append(", ");
            }
            builder.Length -= 2;
            builder.Append("]");
            return builder.ToString();
        }
    }

    public class BoogieMapUpdate : BoogieExpr
    {
        public BoogieExpr BaseExpr { get; set; }

        public List<BoogieExpr> Arguments { get; set; }

        public BoogieExpr Value { get; set; }

        public BoogieMapUpdate(BoogieExpr baseExpr, List<BoogieExpr> arguments, BoogieExpr value)
        {
            Debug.Assert(arguments != null && arguments.Count > 0);
            this.BaseExpr = baseExpr;
            this.Arguments = arguments;
            this.Value = value;
        }

        public BoogieMapUpdate(BoogieExpr baseExpr, BoogieExpr indexExpr, BoogieExpr value)
        {
            this.BaseExpr = baseExpr; ;
            this.Arguments = new List<BoogieExpr>();
            this.Arguments.Add(indexExpr);
            this.Value = value;
        }

        public override string ToString()
        {
            Debug.Assert(Arguments.Count >= 1);
            StringBuilder builder = new StringBuilder();
            builder.Append(BaseExpr).Append("[");
            foreach (BoogieExpr argument in Arguments)
            {
                builder.Append(argument).Append(", ");
            }
            builder.Length -= 2;
            builder.Append(" := ");
            builder.Append(Value + "]");
            return builder.ToString();
        }
    }


    public class BoogieUnaryOperation : BoogieExpr
    {
        public enum Opcode
        {
            NEG,
            NOT,
            UNKNOWN,
        }

        public Opcode Op { get; set; }

        public BoogieExpr Expr { get; set; }

        public BoogieUnaryOperation(Opcode op, BoogieExpr expr)
        {
            this.Op = op;
            this.Expr = expr;
        }

        public static string OpcodeToString(Opcode op)
        {
            switch (op)
            {
                case Opcode.NEG:
                    return "-";
                case Opcode.NOT:
                    return "!";
                default:
                    throw new SystemException($"Unknown opcode: {op}");
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(OpcodeToString(Op)).Append("(").Append(Expr).Append(")");
            return builder.ToString();
        }
    }

    public class BoogieBinaryOperation : BoogieExpr
    {
        public enum Opcode
        {
            ADD,
            SUB,
            MUL,
            DIV,
            MOD,
            EQ,
            NEQ,
            GT,
            GE,
            LT,
            LE,
            AND,
            OR,
            IMP,
            IFF,
            UNKNOWN,
        }

        public static bool USE_ARITH_OPS { get; set; }
        
        public Opcode Op { get; set; }

        public BoogieExpr Lhs { get; set; }

        public BoogieExpr Rhs { get; set; }

        public BoogieBinaryOperation(Opcode op, BoogieExpr lhs, BoogieExpr rhs)
        {
            this.Op = op;
            this.Lhs = lhs;
            this.Rhs = rhs;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("(").Append(Lhs).Append(") ");
            builder.Append(OpcodeToString(Op));
            builder.Append(" (").Append(Rhs).Append(")");
            return builder.ToString();
        }

        public static string OpcodeToString(Opcode op)
        {
            switch (op)
            {
                case Opcode.ADD:
                    return "+";
                case Opcode.SUB:
                    return "-";
                case Opcode.MUL:
                    return "*";
                case Opcode.DIV:
                    return USE_ARITH_OPS ? "/" : "div";
                case Opcode.MOD:
                    return USE_ARITH_OPS ? "%" : "mod";
                case Opcode.EQ:
                    return "==";
                case Opcode.NEQ:
                    return "!=";
                case Opcode.GT:
                    return ">";
                case Opcode.GE:
                    return ">=";
                case Opcode.LT:
                    return "<";
                case Opcode.LE:
                    return "<=";
                case Opcode.AND:
                    return "&&";
                case Opcode.OR:
                    return "||";
                case Opcode.IMP:
                    return "==>";
                case Opcode.IFF:
                    return "<==>";
                default:
                    throw new SystemException($"Unknown opcode: {op}");
            }
        }
    }

    public class BoogieITE : BoogieExpr
    {
        public BoogieExpr Guard { get; set; }

        public BoogieExpr ThenExpr { get; set; }

        public BoogieExpr ElseExpr { get; set; }

        public BoogieITE(BoogieExpr guard, BoogieExpr thenExpr, BoogieExpr elseExpr)
        {
            this.Guard = guard;
            this.ThenExpr = thenExpr;
            this.ElseExpr = elseExpr;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("if ").Append(Guard);
            builder.Append(" then (").Append(ThenExpr);
            builder.Append(") else (").Append(ElseExpr).Append(")");
            return builder.ToString();
        }
    }

    public class BoogieQuantifiedExpr : BoogieExpr
    {
        public bool IsForall { get; set; }

        public List<BoogieIdentifierExpr> QVars { get; set; }

        public List<BoogieType> QVarTypes { get; set; }

        public BoogieExpr BodyExpr { get; set; }

        // let us limit us to a single trigger only {e1, e2, ... }
        public List<BoogieExpr> Trigger { get; set; }

        public BoogieQuantifiedExpr(bool isForall, List<BoogieIdentifierExpr> qvars, List<BoogieType> qvarTypes, BoogieExpr bodyExpr, List<BoogieExpr> trigger = null)
        {
            this.IsForall = isForall;
            this.QVars = qvars;
            this.QVarTypes = qvarTypes;
            Debug.Assert(QVars.Count != 0, "Need at least one bound variable");
            Debug.Assert(QVarTypes.Count == QVars.Count, "Need at least one bound variable");
            this.BodyExpr = bodyExpr;
            this.Trigger = trigger;
        }

        public override string ToString()
        {
            var quantifierString = IsForall ? "forall " : "exists";
            var results = QVars.Zip(QVarTypes, (x, y) => x + ":" + y.ToString());
            var qVarsString = string.Join(", ", results);
            var triggerString = ""; 
            if (Trigger != null && Trigger.Count > 0)
                triggerString = "{" + string.Join(", ", Trigger.Select(x => x.ToString())) + "}";
            return $"{quantifierString} {qVarsString} :: {triggerString} ({BodyExpr.ToString()})"; 
        }
    }

    public class BoogieFuncCallExpr : BoogieExpr
    {
        public string Function { get; set;}

        public List<BoogieExpr> Arguments { get; set;}

        public BoogieFuncCallExpr(string function, List<BoogieExpr> arguments)
        {
            this.Function = function;
            this.Arguments = arguments;
        }

        public override string ToString()
        {
            var argString = string.Join(", ", Arguments.Select(x => x.ToString()));
            return $"{Function}({argString})";
        }
    }
    public class BoogieTupleExpr : BoogieExpr
    {
        public List<BoogieExpr> Arguments { get; set; }

        public BoogieTupleExpr(List<BoogieExpr> arguments)
        {
            this.Arguments = arguments;
        }

        public override string ToString()
        {
            var argString = string.Join(", ", Arguments.Select(x => x.ToString()));
            return $"({argString})";
        }
    }

}
