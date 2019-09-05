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
            ModelReverts = true;
        }
        /// <summary>
        /// Omit printing sourceFile/sourceLine information
        /// </summary>
        public bool NoSourceLineInfoFlag { get; set; }
        /// <summary>
        /// Omit printing instrumentation to print data values in counterexample trace
        /// </summary>
        public bool NoDataValuesInfoFlag{ get; set; }
        /// <summary>
        /// Do not print axioms (except for allocation)
        /// </summary>
        public bool NoAxiomsFlag { get; set; }
        /// <summary>
        /// Do not emit x >= 0 for unsigned expressions x
        /// </summary>
        public bool NoUnsignedAssumesFlag { get; set; }
        /// <summary>
        /// Omit generating Corral and Boogie harness
        /// </summary>
        public bool NoHarness { get; set; }
        /// <summary>
        /// When true, add {:inline 1} attribute, 
        /// can be expensive for recursive procedure analysis by Corral
        /// </summary>
        public bool GenerateInlineAttributes{ get; set; }
        /// <summary>
        /// Model revery logic when an exception happens.
        /// </summary>
        public bool ModelReverts { get; set; }
    }
}
