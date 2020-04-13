using System.Collections.Generic;

namespace myne.Code_Analyzer
{
    public sealed class Literal_Expression_Syntax_Node : Expression_Syntax_Node
    {
        public Literal_Expression_Syntax_Node(Syntax_Tokens_Set literal_token_object)
        {
            Literals_Token_Object = literal_token_object;
        }
        public override Syntax_Kind_of_Token Kind_Of_Token => Syntax_Kind_of_Token.Literal_Expression;

        public Syntax_Tokens_Set Literals_Token_Object { get; }

        public override IEnumerable<Syntax_Node> GetChildren()
        {
            yield return Literals_Token_Object;
        }
    }
}