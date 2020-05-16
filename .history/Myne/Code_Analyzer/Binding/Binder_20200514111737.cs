using System;
using System.Collections.Generic;
using myne.Code_Analyzer.Syntax;

namespace myne.Code_Analyzer.Binding
{
    internal sealed class Binder
    {
        private readonly Diagnostic_Collection _diagnostics = new Diagnostic_Collection();

        public Diagnostic_Collection Diagnostics => _diagnostics;

        public Bound_Expression_Node Bind_Expression(Expression_Syntax_Node expression_syntax)
        {
            switch (expression_syntax.Kind_Of_Token)
            {
                case Syntax_Kind_of_Token.Literal_Expression:
                    return Bind_Literal_Expression((Literal_Expression_Syntax_Node)expression_syntax);

                case Syntax_Kind_of_Token.Unary_Expression:
                    return Bind_Unary_Expression((Unary_Expression_Syntax_Node)expression_syntax);

                case Syntax_Kind_of_Token.Binary_Expression:
                    return Bind_Binary_Expression((Binary_Expression_Syntax_Node)expression_syntax);

                case Syntax_Kind_of_Token.Parenthesized_Expression:
                    return Bind_Expression(((Parenthesized_Expression_Syntax_Node)expression_syntax).Expression);

                default:
                    throw new Exception($"Unexpected Syntax {expression_syntax.Kind_Of_Token}");
            }
        }

        private Bound_Expression_Node Bind_Literal_Expression(Literal_Expression_Syntax_Node expression_syntax)
        {
            var value = expression_syntax.Value ?? 0;
            return new Bound_Literal_Expression_Node(value);
        }

        private Bound_Expression_Node Bind_Unary_Expression(Unary_Expression_Syntax_Node expression_syntax)
        {
            var bound_operand = Bind_Expression(expression_syntax.Operand_token);
            var bound_operator = Bound_Unary_Operator.Bind(expression_syntax.Operator_Token.Kind_Of_Token, bound_operand.Type);

            if (bound_operator == null)
            {
                _diagnostics.Report_Undefined_Unary_Operator(expression_syntax.Operator_Token.Span, expression_syntax.Operator_Token.Text, bound_operand.Type);
                return bound_operand;
            }

            return new Bound_Unary_Expression_Node(bound_operator, bound_operand);
        }

        private Bound_Expression_Node Bind_Binary_Expression(Binary_Expression_Syntax_Node expression_syntax)
        {
            var bound_left = Bind_Expression(expression_syntax.Left_Token);
            var bound_right = Bind_Expression(expression_syntax.Right_Token);
            var bound_operator = Bound_Binary_Operator.Bind(expression_syntax.Operator_Token.Kind_Of_Token, bound_left.Type, bound_right.Type);

            if (bound_operator == null)
            {
                _diagnostics.Report_Undefined_Binary_Operator(expression_syntax.Operator_Token.Span, expression_syntax.Operator_Token.Text, bound_left.Type, bound_right.Type);
                return bound_left;
            }

            return new Bound_Binary_Expression_Node(bound_left, bound_operator, bound_right);
        }
        // private Bound_Unary_Operator_Kind? Bind_Unary_Operator_Kind(Syntax_Kind_of_Token kind_Of_Token, Type operand_type)
        // {
        //     if (operand_type == typeof(int))
        //     {
        //         switch (kind_Of_Token)
        //         {
        //             case Syntax_Kind_of_Token.Plus_Token:
        //                 return Bound_Unary_Operator_Kind.Identity;

        //             case Syntax_Kind_of_Token.Minus_Token:
        //                 return Bound_Unary_Operator_Kind.Negation;
        //         }
        //     }
        //     if (operand_type == typeof(bool))
        //     {
        //         switch (kind_Of_Token)
        //         {
        //             case Syntax_Kind_of_Token.Not_Token:
        //                 return Bound_Unary_Operator_Kind.Logical_Negation;
        //         }
        //     }
        //     return null;
        // }

        // private Bound_Binary_Operator_Kind? Bind_Binary_Operator_Kind(Syntax_Kind_of_Token kind_Of_Token, Type left_type, Type right_type)
        // {
        //     if (left_type == typeof(int) && right_type == typeof(int))
        //     {
        //         switch (kind_Of_Token)
        //         {
        //             case Syntax_Kind_of_Token.Plus_Token:
        //                 return Bound_Binary_Operator_Kind.Addition;

        //             case Syntax_Kind_of_Token.Minus_Token:
        //                 return Bound_Binary_Operator_Kind.Subtraction;

        //             case Syntax_Kind_of_Token.Star_Token:
        //                 return Bound_Binary_Operator_Kind.Multiplication;

        //             case Syntax_Kind_of_Token.Slash_Token:
        //                 return Bound_Binary_Operator_Kind.Division;
        //         }
        //     }
        //     if (left_type == typeof(bool) && right_type == typeof(bool))
        //     {
        //         switch (kind_Of_Token)
        //         {
        //             case Syntax_Kind_of_Token.Ampersand_Ampersand_Token:
        //                 return Bound_Binary_Operator_Kind.Logical_And;

        //             case Syntax_Kind_of_Token.Pipe_Pipe_Token:
        //                 return Bound_Binary_Operator_Kind.Logical_Or;
        //         }
        //     }
        //     return null;
        // }
    }
}