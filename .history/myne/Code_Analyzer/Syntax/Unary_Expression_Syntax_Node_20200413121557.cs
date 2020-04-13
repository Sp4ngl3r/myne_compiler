using System.Collections.Generic;

namespace myne.Code_Analyzer
{
    public sealed class Unary_Expression_Syntax_Node : Expression_Syntax_Node
    {
        public Unary_Expression_Syntax_Node(Syntax_Tokens_Set operator_token, Expression_Syntax_Node operand_token)
        {
            Operator_Token = operator_token;
            Operand_token = operand_token;
        }

        public override Syntax_Kind_of_Token Kind_Of_Token => Syntax_Kind_of_Token.Unary_Expression;
        public Syntax_Tokens_Set Operator_Token { get; }
        public Expression_Syntax_Node Operand_token { get; }

        public override IEnumerable<Syntax_Node> GetChildren()
        {
            yield return Operator_Token;
            yield return Operand_token;
        }
    }
}