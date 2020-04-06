using System.Collections.Generic;

namespace myne.Code_Analyzer
{
    public sealed class Number_Expression_Syntax_Node : Expression_Syntax_Node
    {
        public Number_Expression_Syntax_Node(Syntax_Tokens_Set numbers_token_object)
        {
            Numbers_Token_Object = numbers_token_object;
        }
        public override Syntax_Kind_of_Token Kind_Of_Token => Syntax_Kind_of_Token.Number_Expression;

        public Syntax_Tokens_Set Numbers_Token_Object { get; }

        public override IEnumerable<Syntax_Node> GetChildren()
        {
            yield return Numbers_Token_Object;
        }
    }
}