using System;

namespace myne.Code_Analyzer
{
    public sealed class Evaluator
    {
        private readonly Expression_Syntax_Node _root_node;

        public Evaluator(Expression_Syntax_Node root_node)
        {
            _root_node = root_node;
        }

        public int Evaluate()
        {
            return Evaluate_Expression(_root_node);
        }

        private int Evaluate_Expression(Expression_Syntax_Node node)
        {
            if (node is Number_Expression_Syntax_Node n)
                return (int)n.Numbers_Token_Object.Value;

            if (node is Binary_Expression_Syntax_Node b)
            {
                var left = Evaluate_Expression(b.Left_Token);
                var right = Evaluate_Expression(b.Right_Token);

                if (b.Operator_Token.Kind_Of_Token == Syntax_Kind_of_Token.Plus_Token)
                    return left + right;

                else if (b.Operator_Token.Kind_Of_Token == Syntax_Kind_of_Token.Minus_Token)
                    return left - right;

                else if (b.Operator_Token.Kind_Of_Token == Syntax_Kind_of_Token.Star_Token)
                    return left * right;

                else if (b.Operator_Token.Kind_Of_Token == Syntax_Kind_of_Token.Slash_Token)
                    return left / right;

                else
                    throw new Exception($"Unexpected binary operator{b.Operator_Token.Kind_Of_Token}");
            }

            if (node is Parenthesized_Expression_Syntax_Node p)
                return Evaluate_Expression(p.Expression);

            throw new Exception($"Invalid Node {node.Kind_Of_Token}");
        }
    }
}