using System.Collections.Generic;

namespace myne.Code_Analyzer.Syntax
{
    public sealed class Parenthesized_Expression_Syntax_Node : Expression_Syntax_Node
    {
        public Parenthesized_Expression_Syntax_Node(Syntax_Tokens_Set open_parenthesis_token, Expression_Syntax_Node expression, Syntax_Tokens_Set closed_parenthesis_token)
        {
            Open_Parenthesis_Token = open_parenthesis_token;
            Expression = expression;
            Closed_Parenthesis_Token = closed_parenthesis_token;
        }

        public override Syntax_Kind_of_Token Kind_Of_Token => Syntax_Kind_of_Token.Parenthesized_Expression;
        public Syntax_Tokens_Set Open_Parenthesis_Token { get; }
        public Expression_Syntax_Node Expression { get; }
        public Syntax_Tokens_Set Closed_Parenthesis_Token { get; }



        public override IEnumerable<Syntax_Node> GetChildren()
        {
            yield return Open_Parenthesis_Token;
            yield return Expression;
            yield return Closed_Parenthesis_Token;
        }
    }
}