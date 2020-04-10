// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System;
    using System.Text.RegularExpressions;
    using BoogieAST;

    public class MapArrayHelper
    {
        private static Regex mappingRegex = new Regex(@"mapping\((\w+)\s*\w*\s*=>\s*(.+)\)$");
        //mapping(string => uint) appears as mapping(string memory => uint)
        private static Regex arrayRegex = new Regex(@"(.+)\[\w*\] (storage ref|storage pointer|memory|calldata)$");
        // mapping (uint => uint[]) does not have storage/memory in Typestring
        // private static Regex arrayRegex = new Regex(@"(.+)\[\w*\]$");

        public static string GetMemoryMapName(BoogieType keyType, BoogieType valType)
        {
            return "M_" + keyType.ToString() + "_" + valType.ToString();
        }

        public static BoogieExpr GetMemoryMapSelectExpr(BoogieType mapKeyType, BoogieType mapValType, BoogieExpr baseExpr, BoogieExpr indexExpr)
        {
            string mapName = GetMemoryMapName(mapKeyType, mapValType);
            BoogieIdentifierExpr mapIdent = new BoogieIdentifierExpr(mapName);
            BoogieMapSelect mapSelectExpr = new BoogieMapSelect(mapIdent, baseExpr);
            mapSelectExpr = new BoogieMapSelect(mapSelectExpr, indexExpr);
            return mapSelectExpr;
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

        public static bool IsMappingTypeString(string typeString)
        {
            return mappingRegex.IsMatch(typeString);
        }

        public static bool IsArrayTypeString(string typeString)
        {
            return arrayRegex.IsMatch(typeString);
        }
    }
}
