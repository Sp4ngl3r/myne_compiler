using System.Collections.Generic;

namespace myne.Code_Analyzer.Syntax
{
    public sealed class Literal_Expression_Syntax_Node : Expression_Syntax_Node
    {
        public Literal_Expression_Syntax_Node(Syntax_Tokens_Set literal_token)
            : this(literal_token, literal_token.Value)
        {

        }

        public Literal_Expression_Syntax_Node(Syntax_Tokens_Set literal_token, object value)
        {
            Literal_Token = literal_token;
            Value = value;
        }
        public override Syntax_Kind_of_Token Kind_Of_Token => Syntax_Kind_of_Token.Literal_Expression;
        public Syntax_Tokens_Set Literal_Token { get; }
        public object Value { get; }

        public override IEnumerable<Syntax_Node> GetChildren()
        {
            yield return Literal_Token;
        }
    }
}