// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using SolidityAnalysis;

namespace SolToBoogie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using BoogieAST;
    using SolidityAST;

    public class BoogieTranslator
    {
            // set of method@contract pairs whose translatin is skipped
        public BoogieAST Translate(AST solidityAST, HashSet<Tuple<string, string>> ignoredMethods, TranslatorFlags _translatorFlags = null, String entryPointContract = "")
        {
            bool generateInlineAttributesInBpl = _translatorFlags.GenerateInlineAttributes;

            SourceUnitList sourceUnits = solidityAST.GetSourceUnits();
            
            TranslatorContext context = new TranslatorContext(solidityAST, ignoredMethods, generateInlineAttributesInBpl, _translatorFlags, entryPointContract);
            context.IdToNodeMap = solidityAST.GetIdToNodeMap();
            context.SourceDirectory = solidityAST.SourceDirectory;
            // collect the absolute source path and line number for each AST node
            SourceInfoCollector sourceInfoCollector = new SourceInfoCollector(context);
            sourceUnits.Accept(sourceInfoCollector);

            // de-sugar the solidity AST
            // will modify the AST
            SolidityDesugaring desugaring = new SolidityDesugaring(context);
            sourceUnits.Accept(desugaring);

            // collect all contract definitions
            ContractCollector contractCollector = new ContractCollector(context);
            sourceUnits.Accept(contractCollector);

            // collect all sub types for each contract
            InheritanceCollector inheritanceCollector = new InheritanceCollector(context);
            inheritanceCollector.Collect();

            // collect explicit state variables
            StateVariableCollector stateVariableCollector = new StateVariableCollector(context);
            sourceUnits.Accept(stateVariableCollector);

            // resolve state variable declarations and determine the visible ones for each contract
            StateVariableResolver stateVariableResolver = new StateVariableResolver(context);
            stateVariableResolver.Resolve();

            // collect mappings and arrays
            MapArrayCollector mapArrayCollector = new MapArrayCollector(context);
            sourceUnits.Accept(mapArrayCollector);

            // collect constructor definitions
            ConstructorCollector constructorCollector = new ConstructorCollector(context);
            sourceUnits.Accept(constructorCollector);

            // collect explicit function and event definitions
            FunctionEventCollector functionEventCollector = new FunctionEventCollector(context);
            sourceUnits.Accept(functionEventCollector);

            // resolve function and event definitions and determine the actual definition for a dynamic type
            FunctionEventResolver functionEventResolver = new FunctionEventResolver(context);
            functionEventResolver.Resolve();

            // Generate map helper
            MapArrayHelper mapHelper = new MapArrayHelper(context, solidityAST);
            
            // add types, gobal ghost variables, and axioms
            GhostVarAndAxiomGenerator generator = new GhostVarAndAxiomGenerator(context, mapHelper);
            generator.Generate();

            // collect modifiers information
            ModifierCollector modifierCollector = new ModifierCollector(context);
            sourceUnits.Accept(modifierCollector);

            // collect all using using definitions
            UsingCollector usingCollector = new UsingCollector(context);
            sourceUnits.Accept(usingCollector);

            if (context.TranslateFlags.PerformFunctionSlice)
            {
                FunctionDependencyCollector depCollector = new FunctionDependencyCollector(context, entryPointContract, context.TranslateFlags.SliceFunctionNames);
                context.TranslateFlags.SliceFunctions = depCollector.GetFunctionDeps();
                context.TranslateFlags.SliceModifiers = depCollector.getModifierDeps();
            }
            
            // translate procedures
            ProcedureTranslator procTranslator = new ProcedureTranslator(context, mapHelper, generateInlineAttributesInBpl);
            sourceUnits.Accept(procTranslator);

            // generate fallbacks
            FallbackGenerator fallbackGenerator = new FallbackGenerator(context);
            fallbackGenerator.Generate();

            // generate harness for each contract
            if (!context.TranslateFlags.NoHarness)
            {
                HarnessGenerator harnessGenerator = new HarnessGenerator(context, procTranslator.ContractInvariants);
                harnessGenerator.Generate();
            }

            if (context.TranslateFlags.ModelReverts)
            {
                RevertLogicGenerator reverGenerator = new RevertLogicGenerator(context);
                reverGenerator.Generate();
            }

            if (context.TranslateFlags.DoModSetAnalysis)
            {
                ModSetAnalysis modSetAnalysis = new ModSetAnalysis(context);
                modSetAnalysis.PerformModSetAnalysis();
            }

            if (context.TranslateFlags.GenerateERC20Spec)
            {
                ERC20SpecGenerator specGen = new ERC20SpecGenerator(context, solidityAST, entryPointContract);
                specGen.GenerateSpec();
            }

            return new BoogieAST(context.Program);
        }
    }
}
