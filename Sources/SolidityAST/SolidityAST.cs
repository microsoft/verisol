// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolidityAST
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using JsonSubTypes;
    using System.IO;
    using System.Linq;

    public enum EnumContractKind
    {
        CONTRACT,
        LIBRARY,
        INTERFACE,
    }

    public enum EnumVisibility
    {
        PUBLIC,
        EXTERNAL,
        INTERNAL,
        PRIVATE,
        DEFAULT,
    }

    public enum EnumLocation
    {
        DEFAULT,
        STORAGE,
        MEMORY,
        CALLDATA,
    }

    public enum EnumStateMutability
    {
        PURE,
        VIEW,
        NONPAYABLE,
        PAYABLE,
    }

    public class AST
    {
        private readonly SourceUnitList sourceUnits;

        private readonly Dictionary<int, ASTNode> idToNodeMap;

        public string SourceDirectory { get; private set; }

        public AST(CompilerOutput compilerOutput, string sourceDirectory = null)
        {
            SourceDirectory = sourceDirectory;
            sourceUnits = BuildSourceUnitList(compilerOutput);
            NodeMapper mapper = new NodeMapper(sourceUnits);
            idToNodeMap = mapper.GetIdToNodeMap();    
        }

        private SourceUnitList BuildSourceUnitList(CompilerOutput compilerOutput)
        {
            SourceUnitList sourceUnitList = new SourceUnitList();
            foreach (string filename in compilerOutput.Sources.Keys)
            {
                SoliditySourceFile soliditySourceFile = compilerOutput.Sources[filename];
                Debug.Assert(soliditySourceFile.Ast is SourceUnit);
                sourceUnitList.AddSourceUnit(filename, soliditySourceFile.Ast as SourceUnit);
            }
            return sourceUnitList;
        }

        public SourceUnitList GetSourceUnits()
        {
            return sourceUnits;
        }

        public Dictionary<int, ASTNode> GetIdToNodeMap()
        {
            return idToNodeMap;
        }

        public ASTNode GetASTNodeByID(int id)
        {
            return idToNodeMap[id];
        }
    }

    public class SourceUnitList : ASTNode
    {
        public Dictionary<string, SourceUnit> FilenameToSourceUnitMap { get; private set; }

        public SourceUnitList()
        {
            // no Id and Src assigned by compiler
            Id = -1;
            Src = null;
            NodeType = "SourceUnitList";
            FilenameToSourceUnitMap = new Dictionary<string, SourceUnit>();
        }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                foreach (string filename in FilenameToSourceUnitMap.Keys)
                {
                    FilenameToSourceUnitMap[filename].Accept(visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public void AddSourceUnit(string filename, SourceUnit sourceUnit)
        {
            FilenameToSourceUnitMap.Add(filename, sourceUnit);
        }

        public bool ContainsFilename(string filename)
        {
            return FilenameToSourceUnitMap.ContainsKey(filename);
        }

        public SourceUnit GetSourceUnitByFilename(string filename)
        {
            return FilenameToSourceUnitMap[filename];
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (string filename in FilenameToSourceUnitMap.Keys)
            {
                SourceUnit sourceUnit = FilenameToSourceUnitMap[filename];
                builder.Append(sourceUnit);
            }
            return builder.ToString();
        }
    }

    [JsonConverter(typeof(JsonSubtypes), "NodeType")]
    [JsonSubtypes.KnownSubType(typeof(SourceUnit), "SourceUnit")]
    [JsonSubtypes.KnownSubType(typeof(PragmaDirective), "PragmaDirective")]
    [JsonSubtypes.KnownSubType(typeof(UsingForDirective), "UsingForDirective")]
    [JsonSubtypes.KnownSubType(typeof(ImportDirective), "ImportDirective")]
    [JsonSubtypes.KnownSubType(typeof(ContractDefinition), "ContractDefinition")]
    [JsonSubtypes.KnownSubType(typeof(FunctionDefinition), "FunctionDefinition")]
    [JsonSubtypes.KnownSubType(typeof(ModifierDefinition), "ModifierDefinition")]
    [JsonSubtypes.KnownSubType(typeof(EventDefinition), "EventDefinition")]
    [JsonSubtypes.KnownSubType(typeof(StructDefinition), "StructDefinition")]
    [JsonSubtypes.KnownSubType(typeof(EnumDefinition), "EnumDefinition")]
    [JsonSubtypes.KnownSubType(typeof(VariableDeclaration), "VariableDeclaration")]
    public abstract class ASTNode
    {
        public int Id { get; set; }
        
        public int GasCost { get; set; }

        public string NodeType { get; set; }

        public string Src { get; set; }

        public abstract void Accept(IASTVisitor visitor);

        public abstract T Accept<T>(IASTGenericVisitor<T> visitor);
    }

    public class SourceUnit : ASTNode
    {
        public string AbsolutePath { get; set; }

        public Dictionary<string, List<int>> ExportedSymbols { get; set; }

        public List<ASTNode> Nodes { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Utils.AcceptList(Nodes, visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (Nodes != null)
            {
                foreach (ASTNode node in Nodes)
                {
                    builder.Append(node).AppendLine().AppendLine();
                }
            }
            return builder.ToString();
        }
    }

    public class PragmaDirective : ASTNode
    {
        public List<string> Literals { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("pragma ");
            foreach (string literal in Literals)
            {
                builder.Append(literal);
            }
            builder.Append(";");
            return builder.ToString();
        }
    }

    public class UsingForDirective : ASTNode
    {
        public UserDefinedTypeName LibraryName { get; set; }

        public TypeName TypeName { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                LibraryName.Accept(visitor);
                if (TypeName != null)
                {
                    TypeName.Accept(visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("using ").Append(LibraryName.Name).Append(" for ").Append(TypeName).AppendLine();
            return builder.ToString();
        }
    }

    public abstract class Declaration : ASTNode
    {
        public int Scope { get; set; }
    }

    public class ImportDirective : Declaration
    {
        public string AbsolutePath { get; set; }

        public string File { get; set; }

        public int SourceUnit { get; set; }

        public List<object> SymbolAliases { get; set; }

        public string UnitAlias { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("import \"").Append(File).AppendLine("\";");
            return builder.ToString();
        }
    }

    public class ContractDefinition : Declaration
    {
        public List<InheritanceSpecifier> BaseContracts { get; set; }

        public List<int> ContractDependencies { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnumContractKind ContractKind { get; set; }

        public string Documentation { get; set; }

        public bool FullyImplemented { get; set; }

        public List<int> LinearizedBaseContracts { get; set; }

        public string Name { get; set; }

        public List<ASTNode> Nodes { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Utils.AcceptList(BaseContracts, visitor);
                Utils.AcceptList(Nodes, visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(ContractKind).Append(" ").Append(Name).Append(" ");
            if (BaseContracts.Count > 0)
            {
                builder.Append("is ");
            }
            foreach (InheritanceSpecifier contract in BaseContracts)
            {
                builder.Append(contract).Append(" ");
            }
            builder.AppendLine("{");
            foreach (ASTNode node in Nodes)
            {
                builder.Append(node).AppendLine();
            }
            builder.Append("}");
            return builder.ToString();
        }
    }

    public class InheritanceSpecifier : ASTNode
    {
        public List<Expression> Arguments { get; set; }

        public UserDefinedTypeName BaseName { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                BaseName.Accept(visitor);
                if (Arguments != null)
                {
                    Utils.AcceptList(Arguments, visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(BaseName);
            if (Arguments != null)
            {
                builder.Append("(");
                foreach (Expression argument in Arguments)
                {
                    builder.Append(argument).Append(", ");
                }
                builder.Length -= 2;
                builder.Append(")");
            }
            return builder.ToString();
        }
    }

    public abstract class CallableDeclaration : Declaration
    {
        public ParameterList Parameters { get; set; }
        
        public ParameterList ReturnParameters { get; set; }
    }

    public class FunctionDefinition : CallableDeclaration
    {
        public Block Body { get; set; }

        public bool Constant { get; set; }

        public string Documentation { get; set; }

        public bool Implemented { get; set; }

        public bool IsConstructor { get; set; }

        public bool IsFallback{ get; set; }

        public bool IsDeclaredConst { get; set; }

        public List<ModifierInvocation> Modifiers { get; set; }

        public string Name { get; set; }

        public bool Payable { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnumStateMutability StateMutability { get; set; }

        public object SuperFunction { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnumVisibility Visibility { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Parameters.Accept(visitor);
                if (ReturnParameters != null)
                {
                    ReturnParameters.Accept(visitor);
                }
                Utils.AcceptList(Modifiers, visitor);
                if (Body != null)
                {
                    Body.Accept(visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (IsConstructor && string.IsNullOrEmpty(Name))
            {
                //will be dead code in solc 0.5.x
                builder.Append("constructor ");
            }
            else
            {
                builder.Append("function ").Append(Name);
            }
            builder.Append("(").Append(Parameters).Append(")");
            builder.Append(" ").Append(Visibility);
            if (StateMutability != EnumStateMutability.NONPAYABLE)
            {
                builder.Append(" ").Append(StateMutability);
            }
            foreach (ModifierInvocation modifier in Modifiers)
            {
                builder.Append(" ").Append(modifier);
            }
            if (ReturnParameters.Length() > 0)
            {
                builder.Append(" returns (").Append(ReturnParameters).Append(")");
            }
            builder.Append(" ").Append(Body);
            return builder.ToString();
        }
    }

    public class ParameterList : ASTNode
    {
        public List<VariableDeclaration> Parameters { get; set; }

        public int Length()
        {
            return Parameters.Count;
        }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Utils.AcceptList(Parameters, visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (VariableDeclaration paramemter in Parameters)
            {
                builder.Append(paramemter).Append(", ");
            }
            if (Parameters.Count > 0) builder.Length -= 2;
            return builder.ToString();
        }
    }

    public class ModifierDefinition : CallableDeclaration
    {
        public Block Body { get; set; }

        public string Documentation { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnumVisibility Visibility { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Parameters.Accept(visitor);
                Body.Accept(visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("modifier ").Append(Name);
            builder.Append("(").Append(Parameters).Append(")");
            builder.Append(Body);
            return builder.ToString();
        }
    }

    public class ModifierInvocation : ASTNode
    {
        public Identifier ModifierName { get; set; }

        public List<Expression> Arguments { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                ModifierName.Accept(visitor);
                if (Arguments != null)
                {
                    Utils.AcceptList(Arguments, visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(ModifierName);
            if (Arguments != null)
            {
                builder.Append("(");
                foreach (Expression argument in Arguments)
                {
                    builder.Append(argument).Append(", ");
                }
                builder.Length -= 2;
                builder.Append(")");
            }
            return builder.ToString();
        }
    }

    public class EventDefinition : CallableDeclaration
    {
        public string Name { get; set; }

        public string Documentation { get; set; }

        public bool Anonymous { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Parameters.Accept(visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("event ").Append(Name).Append("(").Append(Parameters).AppendLine(");");
            return builder.ToString();
        }
    }

    public class StructDefinition : Declaration
    {
        public string CanonicalName { get; set; }

        public string Name { get; set; }

        public List<VariableDeclaration> Members { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnumVisibility Visibility { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Utils.AcceptList(Members, visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("struct ").Append(Name).AppendLine(" {");
            foreach (VariableDeclaration member in Members)
            {
                builder.Append("  ").Append(member).AppendLine(";");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }

    public class EnumDefinition : Declaration
    {
        public string CanonicalName { get; set; }

        public string Name { get; set; }

        public List<EnumValue> Members { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Utils.AcceptList(Members, visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("enum ").Append(Name).AppendLine(" {");
            foreach (EnumValue member in Members)
            {
                builder.Append("  ").Append(member).AppendLine(",");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }

    public class EnumValue : Declaration
    {
        public string Name { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class VariableDeclaration : Declaration
    {
        public bool Constant { get; set; }

        public bool Indexed { get; set; }

        public string Name { get; set; }

        public bool StateVariable { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnumLocation StorageLocation { get; set; }

        public TypeDescription TypeDescriptions { get; set; }
        
        public TypeName TypeName { get; set; }

        public Expression Value { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnumVisibility Visibility { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                if (TypeName != null)
                {
                    TypeName.Accept(visitor);
                }
                if (Value != null)
                {
                    Value.Accept(visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(TypeName).Append(" ");
            if (Indexed)
            {
                builder.Append("indexed ");
            }
            if (Visibility != EnumVisibility.INTERNAL)
            {
                builder.Append(Visibility).Append(" ");
            }
            if ((TypeName is ArrayTypeName || TypeName is UserDefinedTypeName) && StorageLocation == EnumLocation.MEMORY)
            {
                builder.Append("memory ");
            }
            builder.Append(Name);
            if (Value != null)
            {
                builder.Append(" = ").Append(Value);
            }
            return builder.ToString();
        }
    }

    [JsonConverter(typeof(JsonSubtypes), "NodeType")]
    [JsonSubtypes.KnownSubType(typeof(ElementaryTypeName), "ElementaryTypeName")]
    [JsonSubtypes.KnownSubType(typeof(UserDefinedTypeName), "UserDefinedTypeName")]
    [JsonSubtypes.KnownSubType(typeof(Mapping), "Mapping")]
    [JsonSubtypes.KnownSubType(typeof(ArrayTypeName), "ArrayTypeName")]
    public abstract class TypeName : ASTNode
    {
        // left empty
    }

    public class ElementaryTypeName : TypeName
    {
        public TypeDescription TypeDescriptions { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return TypeDescriptions.ToString();
        }
    }

    public class UserDefinedTypeName : TypeName
    {
        // TODO: type
        public string ContractScope { get; set; }

        public string Name { get; set; }

        public int ReferencedDeclaration { get; set; }

        public TypeDescription TypeDescriptions { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Mapping : TypeName
    {
        public ElementaryTypeName KeyType { get; set; }

        public TypeName ValueType { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                KeyType.Accept(visitor);
                ValueType.Accept(visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("mapping(").Append(KeyType).Append(" => ").Append(ValueType).Append(")");
            return builder.ToString();
        }
    }

    public class ArrayTypeName : TypeName
    {
        public TypeName BaseType { get; set; }

        public Expression Length { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                BaseType.Accept(visitor);
                if (Length != null)
                {
                    Length.Accept(visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(BaseType);
            builder.Append("[");
            if (Length != null)
            {
                builder.Append(Length);
            }
            builder.Append("]");
            return builder.ToString();
        }
    }

    [JsonConverter(typeof(JsonSubtypes), "NodeType")]
    [JsonSubtypes.KnownSubType(typeof(Block), "Block")]
    [JsonSubtypes.KnownSubType(typeof(PlaceholderStatement), "PlaceholderStatement")]
    [JsonSubtypes.KnownSubType(typeof(IfStatement), "IfStatement")]
    [JsonSubtypes.KnownSubType(typeof(WhileStatement), "WhileStatement")]
    [JsonSubtypes.KnownSubType(typeof(DoWhileStatement), "DoWhileStatement")]
    [JsonSubtypes.KnownSubType(typeof(ForStatement), "ForStatement")]
    [JsonSubtypes.KnownSubType(typeof(Continue), "Continue")]
    [JsonSubtypes.KnownSubType(typeof(Break), "Break")]
    [JsonSubtypes.KnownSubType(typeof(Return), "Return")]
    [JsonSubtypes.KnownSubType(typeof(Throw), "Throw")]
    [JsonSubtypes.KnownSubType(typeof(EmitStatement), "EmitStatement")]
    [JsonSubtypes.KnownSubType(typeof(VariableDeclarationStatement), "VariableDeclarationStatement")]
    [JsonSubtypes.KnownSubType(typeof(InlineAssembly), "InlineAssembly")]
    [JsonSubtypes.KnownSubType(typeof(ExpressionStatement), "ExpressionStatement")]
    public abstract class Statement : ASTNode
    {
        public string Documnetation { get; set; }
    }

    public abstract class BreakableStatement : Statement
    {
        // left empty
    }

    public class Block : Statement
    {
        public int Scope { get; set; }

        public List<Statement> Statements { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Utils.AcceptList(Statements, visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("{");
            foreach (Statement statement in Statements)
            {
                builder.Append("  ").Append(statement);
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }

    public class PlaceholderStatement : Statement
    {
        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return "_;" + Environment.NewLine;
        }
    }

    public class IfStatement : Statement
    {
        public Expression Condition { get; set; }

        public Statement TrueBody { get; set; }

        public Statement FalseBody { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Condition.Accept(visitor);
                TrueBody.Accept(visitor);
                if (FalseBody != null)
                {
                    FalseBody.Accept(visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("if (").Append(Condition).Append(")");
            builder.Append(TrueBody);
            if (FalseBody != null) builder.Append(" else ").Append(FalseBody);
            return builder.ToString();
        }
    }

    public class WhileStatement : BreakableStatement
    {
        public Expression Condition { get; set; }

        public Statement Body { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Condition.Accept(visitor);
                Body.Accept(visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("while (").Append(Condition).Append(") ").Append(Body);
            return builder.ToString();
        }
    }

    public class DoWhileStatement : BreakableStatement
    {
        public Expression Condition { get; set; }

        public Statement Body { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Body.Accept(visitor);
                Condition.Accept(visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("do ").Append(Body);
            builder.Append("while (").Append(Condition).AppendLine(");");
            return builder.ToString();
        }
    }

    public class ForStatement : BreakableStatement
    {
        public int Scope { get; set; }

        public Statement InitializationExpression { get; set; }

        public Expression Condition { get; set; }

        public ExpressionStatement LoopExpression { get; set; }

        public Statement Body { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                if (InitializationExpression != null)
                {
                    InitializationExpression.Accept(visitor);
                }
                if (Condition != null)
                {
                    Condition.Accept(visitor);
                }
                Body.Accept(visitor);
                if (LoopExpression != null)
                {
                    LoopExpression.Accept(visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("for (").Append(InitializationExpression);
            builder.Append(Condition).Append("; ");
            builder.Append(LoopExpression).Append(") ").Append(Body);
            return builder.ToString();
        }
    }

    public class Continue : Statement
    {
        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return "continue;" + Environment.NewLine;
        }
    }

    public class Break : Statement
    {
        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return "break;" + Environment.NewLine;
        }
    }

    public class Return : Statement
    {
        public Expression Expression { get; set; }

        public int FunctionReturnParameters { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                if (Expression != null)
                {
                    Expression.Accept(visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("return ").Append(Expression).AppendLine(";");
            return builder.ToString();
        }
    }

    public class Throw : Statement
    {
        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return "throw;" + Environment.NewLine;
        }
    }

    public class EmitStatement : Statement
    {
        public FunctionCall EventCall { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                EventCall.Accept(visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("emit ").Append(EventCall).Append(";");
            return builder.ToString();
        }
    }

    public class VariableDeclarationStatement : Statement
    {
        public List<int?> Assignments { get; set; }

        public List<VariableDeclaration> Declarations { get; set; }

        public Expression InitialValue { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                //Debug.Assert(Declarations.Count == 1);
                //Utils.AcceptList(Declarations, visitor);
                foreach (VariableDeclaration varDecl in Declarations)
                {
                    if (varDecl != null)
                    {
                        varDecl.Accept(visitor);
                    }
                }
                if (InitialValue != null)
                {
                    InitialValue.Accept(visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            //Debug.Assert(Declarations.Count == 1, $"Multiple variable declarations: {Declarations.Count}");
            if (Declarations.Count > 1)
            {
                builder.Append($"({String.Join(", ", Declarations)})");
            }
            else
            {
                builder.Append(Declarations[0]);
            }
            
            if (InitialValue != null)
            {
                builder.Append(" = ").Append(InitialValue);
            }
            builder.AppendLine(";");
            return builder.ToString();
        }
    }

    public class ExternalReference
    {
        public int declaration { get; set; }
        public bool isOffset { get; set; }
        public bool isSlot { get; set; }
        public String src { get; set; }
        public int valueSize { get; set; }
    }
    public class InlineAssembly : Statement
    {
        //public List<object> ExternalReferences { get; set; }
        public List<Dictionary<string, ExternalReference>> ExternalReferences { get; set; }

        public string Operations { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("assembly");
            builder.Append(Operations);
            builder.AppendLine();
            return builder.ToString();
        }
    }

    public class ExpressionStatement : Statement
    {
        public Expression Expression { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                if (Expression != null)
                {
                    Expression.Accept(visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Expression).AppendLine(";");
            return builder.ToString();
        }
    }

    [JsonConverter(typeof(JsonSubtypes), "NodeType")]
    [JsonSubtypes.KnownSubType(typeof(Literal), "Literal")]
    [JsonSubtypes.KnownSubType(typeof(Identifier), "Identifier")]
    [JsonSubtypes.KnownSubType(typeof(ElementaryTypeNameExpression), "ElementaryTypeNameExpression")]
    [JsonSubtypes.KnownSubType(typeof(UnaryOperation), "UnaryOperation")]
    [JsonSubtypes.KnownSubType(typeof(BinaryOperation), "BinaryOperation")]
    [JsonSubtypes.KnownSubType(typeof(Conditional), "Conditional")]
    [JsonSubtypes.KnownSubType(typeof(Assignment), "Assignment")]
    [JsonSubtypes.KnownSubType(typeof(TupleExpression), "TupleExpression")]
    [JsonSubtypes.KnownSubType(typeof(FunctionCall), "FunctionCall")]
    [JsonSubtypes.KnownSubType(typeof(NewExpression), "NewExpression")]
    [JsonSubtypes.KnownSubType(typeof(MemberAccess), "MemberAccess")]
    [JsonSubtypes.KnownSubType(typeof(IndexAccess), "IndexAccess")]
    public abstract class Expression : ASTNode
    {
        public List<TypeDescription> ArgumentTypes { get; set; }

        public bool IsConstant { get; set; }

        public bool IsPure { get; set; }

        public bool IsLValue { get; set; }

        public bool LValueRequested { get; set; }

        public TypeDescription TypeDescriptions { get; set; }
    }

    public abstract class PrimaryExpression : Expression
    {
        // left empty
    }

    public class Literal : PrimaryExpression
    {
        // TODO: switch to enum type
        public string Subdenomination { get; set; }

        public string HexValue { get; set; }

        public string Kind { get; set; }

        public string Value { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            if (Kind.Equals("string"))
            {
                return "\'" + Value + "\'";
            }
            else
            {
                return Value;
            }
        }
    }

    public class Identifier : PrimaryExpression
    {
        public string Name { get; set; }

        public List<int> OverloadedDeclarations { get; set; }

        public int ReferencedDeclaration { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ElementaryTypeNameExpression : PrimaryExpression
    {
        public string TypeName { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return TypeName;
        }
    }

    public class UnaryOperation : Expression
    {
        // TODO: switch to enum type
        public string Operator { get; set; }

        public Expression SubExpression { get; set; }

        public bool Prefix { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                SubExpression.Accept(visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (Prefix) builder.Append(Operator);
            builder.Append(SubExpression);
            if (!Prefix) builder.Append(Operator);
            return builder.ToString();
        }
    }

    public class BinaryOperation : Expression
    {
        public TypeDescription CommonType { get; set; }

        public Expression LeftExpression { get; set; }

        // TODO: switch to enum type
        public string Operator { get; set; }

        public Expression RightExpression { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                LeftExpression.Accept(visitor);
                RightExpression.Accept(visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(LeftExpression).Append(" ").Append(Operator).Append(" ").Append(RightExpression);
            return builder.ToString();
        }
    }

    public class Conditional : Expression
    {
        public Expression Condition { get; set; }

        public Expression TrueExpression { get; set; }

        public Expression FalseExpression { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Condition.Accept(visitor);
                TrueExpression.Accept(visitor);
                FalseExpression.Accept(visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Condition).Append(" ? ").Append(TrueExpression).Append(" : ").Append(FalseExpression);
            return builder.ToString();
        }
    }

    public class Assignment : Expression
    {
        public Expression LeftHandSide { get; set; }

        public string Operator { get; set; }

        public Expression RightHandSide { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                LeftHandSide.Accept(visitor);
                RightHandSide.Accept(visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(LeftHandSide).Append(" ").Append(Operator).Append(" ").Append(RightHandSide);
            return builder.ToString();
        }
    }

    public class TupleExpression : Expression
    {
        public List<Expression> Components { get; set; }

        public bool IsInlineArray { get; set; }

        public int Length()
        {
            return Components.Count;
        }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                if (Components != null)
                {
                    //HACK: to support (x, y, _) we need to skip null
                    List<Expression> nonNullComponents = Components.Where(x => x != null).ToList();
                    Utils.AcceptList(/*Components*/ nonNullComponents, visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            Debug.Assert(Components.Count > 0);
            StringBuilder builder = new StringBuilder();
            builder.Append("(");
            foreach (Expression component in Components)
            {
                builder.Append(component).Append(", ");
            }
            builder.Length -= 2;
            builder.Append(")");
            return builder.ToString();
        }
    }

    public class FunctionCall : Expression
    {
        public List<Expression> Arguments { get; set; }

        // implicit msg.value of a function call
        // can be modified by foo.value(x)(args)
        public Expression MsgValue { get; set; }

        // implicit gas budget of a function call
        // can be modified by foo.gas(x)(args)
        public Expression MsgGas { get; set; }

        public Expression Expression { get; set; }

        public List<string> Names { get; set; }

        public string Kind { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Expression.Accept(visitor);
                Utils.AcceptList(Arguments, visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Expression);
            builder.Append("(");
            foreach (Expression argument in Arguments)
            {
                builder.Append(argument).Append(", ");
            }
            if (Arguments.Count > 0) builder.Length -= 2;
            builder.Append(")");
            return builder.ToString();
        }
    }

    public class NewExpression : Expression
    {
        public TypeName TypeName { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                TypeName.Accept(visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("new ");
            builder.Append(TypeName);
            return builder.ToString();
        }
    }

    public class MemberAccess : Expression
    {
        public Expression Expression { get; set; }

        public string MemberName { get; set; }

        // The member may not have a referenced declaration
        // as it can be an implicit function like `push` for dynamic arrays
        public int? ReferencedDeclaration;

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                Expression.Accept(visitor);
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Expression).Append(".").Append(MemberName);
            return builder.ToString();
        }
    }

    public class IndexAccess : Expression
    {
        public Expression BaseExpression { get; set; }

        public Expression IndexExpression { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            if (visitor.Visit(this))
            {
                BaseExpression.Accept(visitor);
                if (IndexExpression != null)
                {
                    IndexExpression.Accept(visitor);
                }
            }
            visitor.EndVisit(this);
        }

        public override T Accept<T>(IASTGenericVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(BaseExpression).Append("[").Append(IndexExpression).Append("]");
            return builder.ToString();
        }
    }

    public class TypeDescription
    {
        public string TypeIndentifier { get; set; }

        public string TypeString { get; set; }

        public override string ToString()
        {
            return TypeString;
        }
    }
}
