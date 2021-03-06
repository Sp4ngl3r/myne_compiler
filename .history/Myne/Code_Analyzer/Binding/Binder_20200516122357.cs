using System;
using System.Collections.Generic;
using myne.Code_Analyzer.Syntax;

namespace myne.Code_Analyzer.Binding
{
    internal sealed class Binder
    {
        private readonly Diagnostic_Collection _diagnostics = new Diagnostic_Collection();
        private readonly Dictionary<string, object> _variables;

        public Binder(Dictionary<string, object> variables)
        {
            _variables = variables;
        }

        public Diagnostic_Collection Diagnostics => _diagnostics;

        public Bound_Expression_Node Bind_Expression(Expression_Syntax_Node expression_syntax)
        {
            switch (expression_syntax.Kind_Of_Token)
            {
                case Syntax_Kind_of_Token.Parenthesized_Expression:
                    return Bind_Parenthesized_Expression((Parenthesized_Expression_Syntax_Node)expression_syntax);

                case Syntax_Kind_of_Token.Literal_Expression:
                    return Bind_Literal_Expression((Literal_Expression_Syntax_Node)expression_syntax);

                case Syntax_Kind_of_Token.Name_Expression:
                    return Bind_Name_Expression((Name_Expression_Syntax_Node)expression_syntax);

                case Syntax_Kind_of_Token.Assignment_Expression:
                    return Bind_Assignment_Expression((Assignment_Expression_Syntax_Node)expression_syntax);

                case Syntax_Kind_of_Token.Unary_Expression:
                    return Bind_Unary_Expression((Unary_Expression_Syntax_Node)expression_syntax);

                case Syntax_Kind_of_Token.Binary_Expression:
                    return Bind_Binary_Expression((Binary_Expression_Syntax_Node)expression_syntax);

                default:
                    throw new Exception($"Unexpected Syntax {expression_syntax.Kind_Of_Token}");
            }
        }

        private Bound_Expression_Node Bind_Parenthesized_Expression(Parenthesized_Expression_Syntax_Node expression_syntax)
        {
            return Bind_Expression(expression_syntax.Expression);
        }

        private Bound_Expression_Node Bind_Literal_Expression(Literal_Expression_Syntax_Node expression_syntax)
        {
            var value = expression_syntax.Value ?? 0;
            return new Bound_Literal_Expression_Node(value);
        }

        private Bound_Expression_Node Bind_Name_Expression(Name_Expression_Syntax_Node expression_syntax)
        {
            var name = expression_syntax.Identifier_Token.Text;
            if (!_variables.TryGetValue(name, out var value))
            {
                _diagnostics.Report_Undefined_Name(expression_syntax.Identifier_Token.Span, name);
                return new Bound_Literal_Expression_Node(0);
            }

            var type = value?.GetType() ?? typeof(object);
            return new Bound_Variable_Expression_Node(name, type);
        }

        private Bound_Expression_Node Bind_Assignment_Expression(Assignment_Expression_Syntax_Node expression_syntax)
        {

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
    }
}