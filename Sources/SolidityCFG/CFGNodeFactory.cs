
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
