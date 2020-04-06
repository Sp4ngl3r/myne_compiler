using System.Collections.Generic;

namespace myne.Code_Analyzer
{
    public abstract class Syntax_Node
    {
        public abstract Syntax_Kind_of_Token Kind_Of_Token { get; }

        public abstract IEnumerable<Syntax_Node> GetChildren();
    }
}