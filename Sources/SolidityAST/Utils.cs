// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolidityAST
{
    using System.Diagnostics;
    using System.Collections.Generic;

    public class Utils
    {
        public static void AcceptList<T>(List<T> list, IASTVisitor visitor) where T : ASTNode
        {
            Debug.Assert(list != null);
            foreach (T element in list)
            {
                element.Accept(visitor);
            }
        }
    }
}
