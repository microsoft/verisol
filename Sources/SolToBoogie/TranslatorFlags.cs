using System;
using System.Collections.Generic;
using System.Text;
using SolidityAST;

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
            UseModularArithmetic = false;
            NoUnsignedAssumesFlag = false;
            NoHarness = false; 
            GenerateInlineAttributes = true;
            ModelReverts = false;
            InstrumentGas = false;
            ModelOfStubs = "skip"; //default
            InlineDepthForBoogie = 4;
            PerformContractInferce = false;
            DoModSetAnalysis = false;
            RemoveScopeInVarName = false;
            InstrumentSums = false;
            NoBoogieHarness = false;
            CreateMainHarness = false;
            NoCustomTypes = false;
            OmitAssumeFalseForDynDispatch = false;
            NoTxnsFromContract = true;
            RunAliasAnalysis = false;
            UseMultiDim = false;
            TxnsOnFields = false;
            NoNonlinearArith = false;
            UseNumericOperators = false;
            LazyAllocNoMod = false;
            QuantFreeAllocs = false;
            LazyNestedAlloc = false;
            PrePostHarness = false;
            GenerateGetters = false;
            GenerateERC20Spec = false;
            SliceFunctionNames = new HashSet<string>();
            SliceFunctions = new HashSet<FunctionDefinition>();
            SliceModifiers = new HashSet<ModifierDefinition>();
            PerformFunctionSlice = false;
            AssemblyAsHavoc = false;
        }

        public bool PerformFunctionSlice { get; set; }
        
        public bool AssemblyAsHavoc { get; set; }
        
        public HashSet<ModifierDefinition> SliceModifiers { get; set; }
        public HashSet<FunctionDefinition> SliceFunctions { get; set; }
        
        public HashSet<String> SliceFunctionNames { get; set; }
        public bool UseNumericOperators { get; set; }
        public bool NoNonlinearArith { get; set; }
        
        public bool TxnsOnFields { get; set; }
        
        public bool RunAliasAnalysis { get; set; }
        
        public bool NoTxnsFromContract { get; set; }
        
        public bool NoCustomTypes { get; set; }
        
        public bool CreateMainHarness { get; set; }
        
        public bool NoBoogieHarness { get; set; }
        
        public bool InstrumentSums { get; set; }
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
        /// Do not emit underflow/overflow assumes for unsigned operations x
        /// </summary>
        public bool UseModularArithmetic { get; set; }
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
        /// Model revert logic when an exception happens.
        /// </summary>
        public bool ModelReverts { get; set; }
        /// <summary>
        /// Instrument contracts with gas consumption.
        /// </summary>
        public bool InstrumentGas { get; set; }
        /// <summary>
        /// Do not generate quantified axioms for allocation statements.
        /// </summary>
        public bool QuantFreeAllocs { get; set; }
        /// <summary>
        /// Allocate nested maps lazily, i.e., eliminate HavocAllocMany
        /// </summary>
        public bool LazyNestedAlloc { get; set; }
        
        public bool LazyAllocNoMod { get; set; }

        /// <summary>
        /// Remove assume false for the default branch of dyn dispatch
        /// </summary>
        public bool OmitAssumeFalseForDynDispatch { get; set; }

        // models of stubs for unknown procedures and fallbacks
        public string ModelOfStubs { get; set; }
        public bool ModelStubsAsHavocs() { return ModelOfStubs.Equals("havoc"); }
        public bool ModelStubsAsSkips() { return ModelOfStubs.Equals("skip"); }
        public bool ModelStubsAsCallbacks() { return ModelOfStubs.Equals("callback"); }

        public bool ModelStubsAsMultipleCallbacks() { return ModelOfStubs.Equals("multipleCallbacks"); }

        // this translates to /inlineDepth:k when calling Boogie with /contractInfer
        public int InlineDepthForBoogie { get; set; }
        public bool PerformContractInferce { get; set; }

        // Do ModSet analysis on the Boogie program.
        public bool DoModSetAnalysis { get; set; }

        // remove the scoping from variable name (risky and not exposed)
        public bool RemoveScopeInVarName { get; set; }
        
        public bool PrePostHarness { get; set; }
        
        public bool UseMultiDim { get; set; }
        
        public bool GenerateGetters { get; set; }
        
        public bool GenerateERC20Spec { get; set; }
    }
}
