
namespace SolToBoogie
{
    using BoogieAST;
    using SolidityAST;

    public class BoogieTranslator
    {
        public BoogieAST Translate(AST solidityAST)
        {
            SourceUnitList sourceUnits = solidityAST.GetSourceUnits();

            TranslatorContext context = new TranslatorContext();
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

            // add types, gobal ghost variables, and axioms
            GhostVarAndAxiomGenerator generator = new GhostVarAndAxiomGenerator(context);
            generator.Generate();

            // translate procedures
            ProcedureTranslator procTranslator = new ProcedureTranslator(context);
            sourceUnits.Accept(procTranslator);

            // generate harness for each contract
            HarnessGenerator harnessGenerator = new HarnessGenerator(context);
            harnessGenerator.Generate();

            return new BoogieAST(context.Program);
        }
    }
}
