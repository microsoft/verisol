
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
            string line;
            int CharCount = 0, LineCount = 0;
            LineBreaks.Add(0);
            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                CharCount++;
                CharCount += line.Length;
                LineBreaks.Add(CharCount);
                LineCount++;
            }
            file.Close();
            return LineBreaks;
        }
    }
}
