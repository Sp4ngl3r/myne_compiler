using System.Collections.Generic;

namespace myne.Code_Analyzer.Syntax
{
    public sealed class Assignment_Expression_Syntax_Node : Expression_Syntax_Node
    {
        public Assignment_Expression_Syntax_Node(Syntax_Tokens_Set identifier_token,Syntax_Tokens_Set equals_token,Expression_Syntax_Node expression)
        {
            Identifier_Token = identifier_token;
            Equals_Token = equals_token;
            Expression = expression;
        }

        public Syntax_Tokens_Set Identifier_Token { get; }
        public Syntax_Tokens_Set Equals_Token { get; }
        public Expression_Syntax_Node Expression { get; }
        public override Syntax_Kind_of_Token Kind_Of_Token => Syntax_Kind_of_Token.Assignment_Expression;
        public override IEnumerable<Syntax_Node> GetChildren()
        {
            yield return Identifier_Token;
            yield return Equals_Token;
            yield return Expression;
        }
    }
}