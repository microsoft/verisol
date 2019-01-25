// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System.IO;
    using System.Collections.Generic;
    using SolidityAST;

    public class SourceInfoCollector : BasicASTVisitor
    {
        // require the SourceDirectory field is filled
        private TranslatorContext context;

        // current source unit the visitor is visiting
        private SourceUnit currentSourceUnit;

        //from srcFile name to the int list of "\n" positions
        private Dictionary<string, List<int>> DictLineBreaks = new Dictionary<string, List<int>>();

        public SourceInfoCollector(TranslatorContext context)
        {
            this.context = context;
        }

        public override bool Visit(SourceUnitList sourceUnits)
        {
            string srcPath = context.SourceDirectory;
            foreach (KeyValuePair<string, SourceUnit> entry in sourceUnits.FilenameToSourceUnitMap)
            {
                string srcFileName = Path.Combine(srcPath, entry.Key);
                List<int> LineBreaks = computeLineBreaks(srcFileName);
                DictLineBreaks.Add(entry.Key, LineBreaks);
            }
            return true;
        }

        public override bool Visit(SourceUnit node)
        {
            currentSourceUnit = node;
            return true;
        }

        protected override void CommonEndVisit(ASTNode node)
        {
            if (!(node is SourceUnitList))
            {
                string relativePath = currentSourceUnit.AbsolutePath;
                string absolutePath = Path.Combine(context.SourceDirectory, relativePath);

                string srcInfo = node.Src;
                string[] tokens = srcInfo.Split(':');
                int startPosition = int.Parse(tokens[0]);
                int lineNumber = MapToLineNumber(relativePath, startPosition);

                context.AddSourceInfoForASTNode(node, absolutePath, lineNumber);
            }
        }

        public int MapToLineNumber(string srcFilePathName, int position)
        {
            srcFilePathName = srcFilePathName.Replace("\\", "/"/*, System.StringComparison.CurrentCulture*/);
            List<int> LineBreaks = DictLineBreaks[srcFilePathName];

            //ToDo: Does ConcurrencyExplorer expect the line number to start from 0 or 1?
            int lineNumber = 0;  //if the first line number is 0, then this statement should be "int lineNumber = -1"; 
            foreach (var lineStart in LineBreaks)
            {
                if (lineStart > position)
                    break;
                else
                    lineNumber++;
            }
            return lineNumber;
        }

        private List<int> computeLineBreaks(string filePath)
        {
            List<int> LineBreaks = new List<int>();
            int CharCount = 0;
            LineBreaks.Add(0);
            StreamReader file = new StreamReader(filePath);
            string src = file.ReadToEnd();
            file.Close();

            // About newline chars in UNIX and Windows:
            // The only valid new chars are \r\n and \n, but not \r or \n\r
            // The following code does make sure that if there are \r or \n\r in the file, it doesn't cause infinite looping.
            // However, the code is either not properly translated or ConcurrencyExplorer shows highlight multiple lines at each step.
            // The valid chars \r\n and \n can be mixed in a sol file.
            int pos_r, pos_n;
            do
            {
                pos_r = src.IndexOf('\r');
                pos_n = src.IndexOf('\n');
                if (pos_r >= 0 && (pos_r < pos_n || pos_n < 0))
                {
                    CharCount += pos_r + 1;
                    LineBreaks.Add(CharCount);
                    if (pos_r + 1 == pos_n)
                    {
                        CharCount++;
                        pos_r++;
                    }
                    src = src.Substring(pos_r + 1);
                }
                else if (pos_n >= 0 && (pos_n < pos_r || pos_r < 0))
                {
                    CharCount += pos_n + 1;
                    LineBreaks.Add(CharCount);
                    if (pos_n + 1 == pos_r)
                    {
                        CharCount++;
                        pos_n++;
                    }
                    src = src.Substring(pos_n + 1);
                }
            } while (pos_r >= 0 || pos_n >= 0);
            LineBreaks.Add(CharCount + src.Length);
            return LineBreaks;
        }
    }
}
