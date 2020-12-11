// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Primitives;
using solidityAnalysis;
using SolidityAST;

namespace SolToBoogie
{
    using System;
    using System.Text.RegularExpressions;
    using BoogieAST;

    public class MapArrayHelper
    {
        private static Regex mappingRegex = new Regex(@"mapping\((\w+)\s*\w*\s*=>\s*(.+)\)$");
        //mapping(string => uint) appears as mapping(string memory => uint)
        private static Regex arrayRegex = new Regex(@"(.+)\[\w*\]( storage ref| storage pointer| memory| calldata)?$");
        // mapping (uint => uint[]) does not have storage/memory in Typestring
        // private static Regex arrayRegex = new Regex(@"(.+)\[\w*\]$");
        
        private TranslatorContext context { get; }
        
        private AST solidityAst { get;  }
        
        public MapArrayHelper(TranslatorContext ctxt, AST solidityAst)
        {
            this.context = ctxt;
            this.solidityAst = solidityAst;
        }
        
        public Boolean notAliased(VariableDeclaration decl)
        {   
            return context.TranslateFlags.RunAliasAnalysis && context.Analysis.Alias.getResults().Contains(decl);
        }

        public static string GetCanonicalMemName(BoogieType keyType, BoogieType valType)
        {
            return "M_" + keyType.ToString() + "_" + valType.ToString();
        }

        public VariableDeclaration getDecl(Expression access)
        {
            DeclarationFinder declFinder = new DeclarationFinder(access, solidityAst);
            return declFinder.getDecl();
        }
        
        public string GetMemoryMapName(VariableDeclaration decl, BoogieType keyType, BoogieType valType)
        {
            if (notAliased(decl) && !context.TranslateFlags.UseMultiDim)
            {
                return GetCanonicalMemName(keyType, valType) + "_" + context.Analysis.Alias.getGroupName(decl);
            }

            return GetCanonicalMemName(keyType, valType);
        }

        public BoogieExpr GetMemoryMapSelectExpr(VariableDeclaration decl, BoogieType mapKeyType, BoogieType mapValType, BoogieExpr baseExpr, BoogieExpr indexExpr)
        {
            string mapName = GetMemoryMapName(decl, mapKeyType, mapValType);
            BoogieIdentifierExpr mapIdent = new BoogieIdentifierExpr(mapName);
            BoogieMapSelect mapSelectExpr = new BoogieMapSelect(mapIdent, baseExpr);
            mapSelectExpr = new BoogieMapSelect(mapSelectExpr, indexExpr);
            return mapSelectExpr;
        }
        
        public static String GetCanonicalSumName()
        {
            return "sum";
        }

        public String GetSumName(VariableDeclaration decl)
        {
            if (notAliased(decl))
            {
                return GetCanonicalSumName() + "_" + context.Analysis.Alias.getGroupName(decl);
            }

            return GetCanonicalSumName();
        }
        
        public BoogieExpr GetSumExpr(VariableDeclaration decl, BoogieExpr varExpr)
        {
            BoogieIdentifierExpr sumIdent = new BoogieIdentifierExpr(GetSumName(decl));
            BoogieMapSelect sumSelect = new BoogieMapSelect(sumIdent, varExpr);
            return sumSelect;
        }

        public static BoogieType InferExprTypeFromTupleTypeString(string typeString, int ind)
        {
            if (!typeString.StartsWith("tuple"))
            {
                return null;
            }

            int start = typeString.IndexOf("(");
            int end = typeString.IndexOf(")");

            String[] tupleTypes = typeString.Substring(start + 1, end - start - 1).Split(",");

            if (ind >= tupleTypes.Length)
            {
                return null;
            }

            return InferExprTypeFromTypeString(tupleTypes[ind]);
        }

        public static BoogieType InferExprTypeFromTypeString(string typeString)
        {
            //TODO: use TypeDescriptions::IsInt/IsStruct etc., but need typeDescription instead of typeString
            if (IsArrayTypeString(typeString))
            {
                return BoogieType.Ref;
            }
            else if (IsMappingTypeString(typeString))
            {
                return BoogieType.Ref;
            }
            else if (typeString.Equals("address") || typeString.Equals("address payable"))
            {
                return BoogieType.Ref;
            }
            else if (typeString.Equals("bool"))
            {
                return BoogieType.Bool;
            }
            else if (typeString.StartsWith("uint") && !typeString.Contains("["))
            {
                return BoogieType.Int;
            }
            else if (typeString.StartsWith("int") && !typeString.Contains("["))
            {
                return BoogieType.Int;
            }
            else if (typeString.StartsWith("byte") && !typeString.Contains("["))
            {
                return BoogieType.Int;
            }
            else if (typeString.StartsWith("contract "))
            {
                return BoogieType.Ref;
            }
            else if (typeString.StartsWith("struct "))
            {
                return BoogieType.Ref;
            }
            else if (typeString.Equals("string") || typeString.StartsWith("string "))
            {
                return BoogieType.Int; //we think of string as its hash
            }
            else if (typeString.StartsWith("literal_string "))
            {
                return BoogieType.Int; //we think of string as its hash
            }
            else
            {
                throw new SystemException($"Cannot infer from type string: {typeString}");
            }
        }

        public static BoogieType InferKeyTypeFromTypeString(string typeString)
        {
            if (mappingRegex.IsMatch(typeString))
            {
                Match match = mappingRegex.Match(typeString);
                return InferExprTypeFromTypeString(match.Groups[1].Value);
            }
            else if (arrayRegex.IsMatch(typeString))
            {
                Match match = arrayRegex.Match(typeString);
                return BoogieType.Int;
            }
            else if(typeString=="bytes calldata")
            {
                return BoogieType.Ref;
            }
            else if (typeString.StartsWith("bytes") && !typeString.Equals("bytes"))
            {
                return BoogieType.Int;
            }
            else
            {
                throw new SystemException($"Unknown type string during InferKeyTypeFromTypeString: {typeString}");
            }
        }

        public static BoogieType InferValueTypeFromTypeString(string typeString)
        {
            if (mappingRegex.IsMatch(typeString))
            {
                Match match = mappingRegex.Match(typeString);
                return InferExprTypeFromTypeString(match.Groups[2].Value);
            }
            else if (arrayRegex.IsMatch(typeString))
            {
                Match match = arrayRegex.Match(typeString);
                return InferExprTypeFromTypeString(match.Groups[1].Value);
            }
            else if (typeString == "bytes calldata")
            {
                return BoogieType.Ref;
            }
            else if (typeString.StartsWith("bytes") && !typeString.Equals("bytes"))
            {
                return BoogieType.Int;
            }
            else
            {
                throw new SystemException($"Unknown type string during InferValueTypeFromTypeString: {typeString}");
            }
        }

        public static string GetValueTypeString(string typeString)
        {
            if (mappingRegex.IsMatch(typeString))
            {
                Match match = mappingRegex.Match(typeString);
                return match.Groups[2].Value;
            }

            if (arrayRegex.IsMatch(typeString))
            {
                Match match = arrayRegex.Match(typeString);
                return match.Groups[1].Value;
            }

            return null;
        }
        
        public static string GetIndexTypeString(string typeString)
        {
            if (mappingRegex.IsMatch(typeString))
            {
                Match match = mappingRegex.Match(typeString);
                return match.Groups[1].Value;
            }

            if (arrayRegex.IsMatch(typeString))
            {
                return "uint256";
            }

            return null;
        }

        public static bool IsMappingTypeString(string typeString)
        {
            return mappingRegex.IsMatch(typeString);
        }

        public static bool IsArrayTypeString(string typeString)
        {
            return arrayRegex.IsMatch(typeString);
        }
        
        public string GetNestedAllocName(VariableDeclaration decl, int lvl)
        {
            return "alloc_" + TransUtils.GetCanonicalVariableName(decl, context) + "_lvl" + lvl;
        }
        
        public static BoogieFuncCallExpr GetCallExprForZeroInit(BoogieType key, BoogieType value)
        {
            /*var keyStr = char.ToUpper(key.ToString()[0]) + key.ToString().Substring(1);
            var valStr = char.ToUpper(value.ToString()[0]) + value.ToString().Substring(1);*/
            return new BoogieFuncCallExpr("zero" + key.ToString() + value.ToString() + "Arr", new List<BoogieExpr>());
        }

        public static string GetSmtType(BoogieType type)
        {
            if (type.Equals(BoogieType.Bool))
            {
                return "Bool";
            }
            else if (type.Equals(BoogieType.Int))
            {
                return "Int";
            }
            else if (type.Equals(BoogieType.Ref))
            {
                return "Int";
            }
            
            throw new Exception($"Unknown BoogieType {type}");
        }

        public static BoogieFunction GenerateMultiDimZeroFunction(BoogieType keyType, BoogieType valType)
        {
            BoogieExpr boogieInit = TransUtils.GetDefaultVal(valType);
            string smtType = GetSmtType(valType);
            BoogieType mapType = new BoogieMapType(keyType, valType);
            string fnName = $"zero{keyType.ToString()}{valType.ToString()}Arr";
            
            string smtInit = boogieInit.ToString().Equals("null") ? "0" : boogieInit.ToString();
            smtInit = $"((as const (Array {GetSmtType(keyType)} {smtType})) {smtInit})";

            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", mapType));
            var smtDefinedAttr = new BoogieAttribute("smtdefined", $"\"{smtInit}\"");
            return new BoogieFunction(fnName, new List<BoogieVariable>(), new List<BoogieVariable>() {outVar}, new List<BoogieAttribute>() {smtDefinedAttr});
        }

        public static BoogieFunction GenerateMultiDimZeroFunction(VariableDeclaration decl)
        {
            TypeName curType = decl.TypeName;
            List<BoogieType> boogieType = new List<BoogieType>();

            while (curType is Mapping || curType is ArrayTypeName)
            {
                if (curType is Mapping map)
                {
                    boogieType.Insert(0, TransUtils.GetBoogieTypeFromSolidityTypeName(map.KeyType));
                    curType = map.ValueType;
                }
                else if(curType is ArrayTypeName arr)
                {
                    boogieType.Insert(0, BoogieType.Int);
                    curType = arr.BaseType;
                }
            }
            
            BoogieType valType = TransUtils.GetBoogieTypeFromSolidityTypeName(curType);
            BoogieExpr boogieInit = TransUtils.GetDefaultVal(valType);
            
            string smtType = GetSmtType(valType);
            string smtInit = boogieInit.ToString().Equals("null") ? "0" : boogieInit.ToString();
            BoogieType mapType = valType;
            string fnName = $"{valType.ToString()}Arr";

            foreach (BoogieType type in boogieType)
            {
                smtType = $"(Array {GetSmtType(type)} {smtType})";
                mapType = new BoogieMapType(type, mapType);
                smtInit = $"((as const {smtType}) {smtInit})";
                fnName = $"{type.ToString()}{fnName}";
            }

            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", mapType));
            var smtDefinedAttr = new BoogieAttribute("smtdefined", $"\"{smtInit}\"");
            return new BoogieFunction($"zero{fnName}", new List<BoogieVariable>(), new List<BoogieVariable>() {outVar}, new List<BoogieAttribute>() {smtDefinedAttr});
        }
        
        public static BoogieFunction GenerateMultiDimInitFunction(BoogieMapType type)
        {
            BoogieType curType = type;
            List<BoogieType> boogieType = new List<BoogieType>();

            while (curType is BoogieMapType map)
            {
                if (map.Arguments.Count != 1)
                {
                    throw new Exception("Boogie map must have one argument");
                }
                boogieType.Insert(0, map.Arguments[0]);
                curType = map.Result;
            }
            
            BoogieVariable arg = new BoogieFormalParam(new BoogieTypedIdent("n", curType));
            string smtInit = "n";
            string smtType = GetSmtType(curType);
            string fnName = $"{curType.ToString()}Arr";

            foreach (BoogieType dimType in boogieType)
            {
                smtType = $"(Array {GetSmtType(dimType)} {smtType})";
                smtInit = $"((as const {smtType}) {smtInit})";
                fnName = $"{dimType.ToString()}{fnName}";
            }

            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", type));
            var smtDefinedAttr = new BoogieAttribute("smtdefined", $"\"{smtInit}\"");
            return new BoogieFunction($"init{fnName}", new List<BoogieVariable>() {arg}, new List<BoogieVariable>() {outVar}, new List<BoogieAttribute>() {smtDefinedAttr});
        }
        
        public static BoogieExpr GetCallExprForInit(BoogieType curType, BoogieExpr initExpr)
        {
            if (!(curType is BoogieMapType))
            {
                return initExpr;
            }
            
            string fnName = "init";
            
            while (curType is BoogieMapType map)
            {
                fnName = $"{fnName}{String.Join("", map.Arguments)}";
                curType = map.Result;
            }
            
            fnName = $"{fnName}{curType}Arr";
            return new BoogieFuncCallExpr(fnName, new List<BoogieExpr>() {initExpr});
        }
        
        public static BoogieFuncCallExpr GetCallExprForZeroInit(BoogieType curType)
        {
            string fnName = "zero";
            
            while (curType is BoogieMapType map)
            {
                fnName = $"{fnName}{String.Join("", map.Arguments)}";
                curType = map.Result;
            }
            
            fnName = $"{fnName}{curType}Arr";
            return new BoogieFuncCallExpr(fnName, new List<BoogieExpr>());
        }

        public static BoogieFuncCallExpr GetCallExprForZeroInit(TypeName curType)
        {
            string fnName = "zero";
            
            while (curType is Mapping || curType is ArrayTypeName)
            {
                if (curType is Mapping map)
                {
                    fnName = $"{fnName}{TransUtils.GetBoogieTypeFromSolidityTypeName(map.KeyType).ToString()}";
                    curType = map.ValueType;
                }
                else if (curType is ArrayTypeName arr)
                {
                    fnName = $"{fnName}{BoogieType.Int.ToString()}";
                    curType = arr.BaseType;
                }
            }
            
            fnName = $"{fnName}{TransUtils.GetBoogieTypeFromSolidityTypeName(curType).ToString()}Arr";
            return new BoogieFuncCallExpr(fnName, new List<BoogieExpr>());
        }
        public static BoogieFuncCallExpr GetCallExprForZeroInit(VariableDeclaration decl)
        {
            return GetCallExprForZeroInit(decl.TypeName);
        }

        public static BoogieType GetMultiDimBoogieType(TypeName varType)
        {
            if (!(varType is Mapping || varType is ArrayTypeName))
            {
                return TransUtils.GetBoogieTypeFromSolidityTypeName(varType);
            }

            BoogieType resultType = null;
            if (varType is Mapping map)
            {
                BoogieType valType = GetMultiDimBoogieType(map.ValueType);
                BoogieType keyType = GetMultiDimBoogieType(map.KeyType);
                resultType = new BoogieMapType(keyType, valType);
            }
            else if (varType is ArrayTypeName arr)
            {
                BoogieType baseType = GetMultiDimBoogieType(arr.BaseType);
                resultType = new BoogieMapType(BoogieType.Int, baseType);
            }

            return resultType;
        }

        public static TypeName GetMappedType(VariableDeclaration varDecl)
        {
            TypeName curType = varDecl.TypeName;

            while (curType is Mapping || curType is ArrayTypeName)
            {
                if (curType is Mapping map)
                {
                    curType = map.ValueType;
                }
                else if (curType is ArrayTypeName arr)
                {
                    curType = arr.BaseType;
                }
            }

            return curType;
        }

        public string GetMultiDimLengthName(VariableDeclaration varDecl, int lvl)
        {
            return $"Length_{TransUtils.GetCanonicalVariableName(varDecl, context)}_lvl{lvl}";
        }

        public List<BoogieDeclaration> GetMultiDimArrayLens(VariableDeclaration decl)
        {
            List<BoogieDeclaration> lenVars = new List<BoogieDeclaration>();
            TypeName curType = decl.TypeName;

            List<BoogieType> indTypes = new List<BoogieType>() {BoogieType.Ref};

            int lvl = 0;
            while (curType is Mapping || curType is ArrayTypeName)
            {
                if (curType is Mapping map)
                {
                    curType = map.ValueType;
                    indTypes.Add(TransUtils.GetBoogieTypeFromSolidityTypeName(map.KeyType));
                }
                else if (curType is ArrayTypeName arr)
                {
                    BoogieType mapType = BoogieType.Int;
                    BoogieType initType = BoogieType.Int;
                    for (int i = indTypes.Count - 1; i >= 0; i--)
                    {
                        initType = mapType;
                        mapType = new BoogieMapType(indTypes[i], mapType);
                    }

                    if (arr.Length != null && initType is BoogieMapType lenMap)
                    {
                        BoogieFunction initFn = GenerateMultiDimInitFunction(lenMap);
                        lenVars.Add(initFn);
                    }
                    
                    BoogieGlobalVariable lenVar = new BoogieGlobalVariable(new BoogieTypedIdent(GetMultiDimLengthName(decl, lvl), mapType));
                    lenVars.Add(lenVar);
                    
                    curType = arr.BaseType;
                    indTypes.Add(BoogieType.Int);
                }

                lvl++;
            }

            return lenVars;
        }
        
        public BoogieExpr GetLength(VariableDeclaration varDecl, BoogieExpr receiver)
        {
            if (context.TranslateFlags.UseMultiDim && context.Analysis.Alias.getResults().Contains(varDecl))
            {
                BoogieExpr curExpr = receiver;
                int lvl = -1;
                List<BoogieExpr> keys = new List<BoogieExpr>();
                while (curExpr is BoogieMapSelect sel)
                {
                    curExpr = sel.BaseExpr;
                    keys.Insert(0, sel.Arguments[0]);
                    lvl++;
                }

                string lenName = GetMultiDimLengthName(varDecl, lvl);
                BoogieExpr lenExpr = new BoogieIdentifierExpr(lenName);

                foreach (BoogieExpr key in keys)
                {
                    lenExpr = new BoogieMapSelect(lenExpr, key);
                }

                return lenExpr;
            }
            
            return new BoogieMapSelect(new BoogieIdentifierExpr("Length"), receiver);
        }

        public BoogieExpr GetLength(Expression solExpr, BoogieExpr receiver)
        {
            VariableDeclaration decl = getDecl(solExpr);

            //this can happen in the case of newExpression
            if (decl == null)
            {
                return new BoogieMapSelect(new BoogieIdentifierExpr("Length"), receiver);
            }
            
            return GetLength(decl, receiver);
        }
    }
}
