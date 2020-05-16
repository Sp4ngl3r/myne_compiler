using System;
using System.Collections.Generic;
using myne.Code_Analyzer.Binding;

namespace myne.Code_Analyzer
{
    internal sealed class Evaluator
    {
        private readonly Bound_Expression_Node _root_node;
        private readonly Dictionary<string, object> _variables;

        public Evaluator(Bound_Expression_Node root_node, Dictionary<string, object> variables)
        {
            _root_node = root_node;
            _variables = variables;
        }

        public object Evaluate()
        {
            return Evaluate_Expression(_root_node);
        }

        private object Evaluate_Expression(Bound_Expression_Node node)
        {
            if (node is Bound_Literal_Expression_Node n)
                return n.Value;

            if (node is Bound_Variable_Expression_Node v)
                return _variables[v.Name];

            if (node is Bound_Assignment_Expression_Node a)
            {
                var value = Evaluate_Expression(a.Expression);
                _variables[a.Name] = value;
                return value;
            }
            if (node is Bound_Unary_Expression_Node u)
            {
                var operand = Evaluate_Expression(u.Operand);

                switch (u.Op.Kind)
                {
                    case Bound_Unary_Operator_Kind.Identity:
                        return (int)operand;
                    case Bound_Unary_Operator_Kind.Negation:
                        return -(int)operand;
                    case Bound_Unary_Operator_Kind.Logical_Negation:
                        return !(bool)operand;
                    default:
                        throw new Exception($"Unexpected unary operator {u.Op}");
                }
            }

            if (node is Bound_Binary_Expression_Node b)
            {
                var left = Evaluate_Expression(b.Left);
                var right = Evaluate_Expression(b.Right);

                switch (b.Op.Kind)
                {
                    case Bound_Binary_Operator_Kind.Addition:
                        return (int)left + (int)right;
                    case Bound_Binary_Operator_Kind.Subtraction:
                        return (int)left - (int)right;
                    case Bound_Binary_Operator_Kind.Multiplication:
                        return (int)left * (int)right;
                    case Bound_Binary_Operator_Kind.Division:
                        return (int)left / (int)right;
                    case Bound_Binary_Operator_Kind.Logical_And:
                        return (bool)left && (bool)right;
                    case Bound_Binary_Operator_Kind.Logical_Or:
                        return (bool)left || (bool)right;
                    case Bound_Binary_Operator_Kind.Equals:
                        return Equals(left, right);
                    case Bound_Binary_Operator_Kind.Not_Equals:
                        return !Equals(left, right);
                    default:
                        throw new Exception($"Unexpected binary operator {b.Op}");
                }
            }
            throw new Exception($"Invalid Node {node.Kind}");
        }
    }
}