
namespace SolToBoogie
{
    using SolidityAST;

    /**
     * Collect all constructor definitions and put them in the translator context.
     */
    public class ConstructorCollector : BasicASTVisitor
    {
        private TranslatorContext context;

        public ConstructorCollector(TranslatorContext context)
        {
            this.context = context;
        }

        public override bool Visit(ContractDefinition node)
        {
            foreach (ASTNode child in node.Nodes)
            {
                if (child is FunctionDefinition function)
                {
                    if (function.IsConstructor)
                    {
                        context.AddConstructorToContract(node, function);
                    }
                }
            }
            return false;
        }
    }
}
