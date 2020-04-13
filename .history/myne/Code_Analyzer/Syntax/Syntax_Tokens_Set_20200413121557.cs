using System.Collections.Generic;
using System.Linq;

namespace myne.Code_Analyzer
{
    public sealed class Syntax_Tokens_Set : Syntax_Node
    {
        public Syntax_Tokens_Set(Syntax_Kind_of_Token kind_Of_Token, int position, string text, object value)
        {
            Kind_Of_Token = kind_Of_Token;
            Position = position;
            Text = text;
            Value = value;
        }

        public override Syntax_Kind_of_Token Kind_Of_Token { get; }
        public int Position { get; }
        public string Text { get; }
        public object Value { get; }

        public override IEnumerable<Syntax_Node> GetChildren()
        {
            return Enumerable.Empty<Syntax_Node>();
        }
    }
}