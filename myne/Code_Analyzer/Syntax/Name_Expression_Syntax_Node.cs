using System.Collections.Generic;

namespace myne.Code_Analyzer.Syntax
{
    public sealed class Name_Expression_Syntax_Node : Expression_Syntax_Node
    {
        public Name_Expression_Syntax_Node(Syntax_Tokens_Set identifier_token)
        {
            Identifier_Token = identifier_token;
        }

        public Syntax_Tokens_Set Identifier_Token { get; }

        public override Syntax_Kind_of_Token Kind_Of_Token => Syntax_Kind_of_Token.Name_Expression;

        public override IEnumerable<Syntax_Node> GetChildren()
        {
            yield return Identifier_Token;
        }
    }
}