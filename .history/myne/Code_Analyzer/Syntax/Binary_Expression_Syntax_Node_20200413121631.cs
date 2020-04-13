using System.Collections.Generic;

namespace myne.Code_Analyzer.Syntax
{
    public sealed class Binary_Expression_Syntax_Node : Expression_Syntax_Node
    {
        public Binary_Expression_Syntax_Node(Expression_Syntax_Node left_token, Syntax_Tokens_Set operator_token, Expression_Syntax_Node right_token)
        {
            Left_Token = left_token;
            Operator_Token = operator_token;
            Right_Token = right_token;
        }

        public override Syntax_Kind_of_Token Kind_Of_Token => Syntax_Kind_of_Token.Binary_Expression;
        public Expression_Syntax_Node Left_Token { get; }
        public Syntax_Tokens_Set Operator_Token { get; }
        public Expression_Syntax_Node Right_Token { get; }

        public override IEnumerable<Syntax_Node> GetChildren()
        {
            yield return Left_Token;
            yield return Operator_Token;
            yield return Right_Token;
        }
    }
}