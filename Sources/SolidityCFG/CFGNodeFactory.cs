// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolidityCFG
{
    public class CFGNodeFactory
    {
        private int id;

        public CFGNodeFactory()
        {
            id = 1;
        }

        public CFGNode MakeNode()
        {
            return new CFGNode(id++);
        }
    }
}
