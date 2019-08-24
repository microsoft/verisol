using System;
using System.Collections.Generic;
using System.Text;

namespace SolToBoogie
{
    /// <summary>
    /// A set of flags to control the translation
    /// </summary>
    public class TranslatorFlags
    {
        public TranslatorFlags()
        {
            NoSourceLineInfoFlag = false;
            NoDataValuesInfoFlag = false;
            NoAxiomsFlag = false;
            NoUnsignedAssumesFlag = false;
            NoHarness = false; 
            GenerateInlineAttributes = true;
        }
        public bool NoSourceLineInfoFlag { get; set; }
        public bool NoDataValuesInfoFlag{ get; set; }
        public bool NoAxiomsFlag { get; set; }
        public bool NoUnsignedAssumesFlag { get; set; }
        public bool NoHarness { get; set; }
        public bool GenerateInlineAttributes{ get; set; }
    }
}
