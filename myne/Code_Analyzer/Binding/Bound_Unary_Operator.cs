using System;
using myne.Code_Analyzer.Syntax;

namespace myne.Code_Analyzer.Binding
{
    internal sealed class Bound_Unary_Operator
    {
        private Bound_Unary_Operator(Syntax_Kind_of_Token syntax_kind, Bound_Unary_Operator_Kind kind, Type operand_type)
            : this(syntax_kind, kind, operand_type, operand_type)
        {

        }
        private Bound_Unary_Operator(Syntax_Kind_of_Token syntax_kind, Bound_Unary_Operator_Kind kind, Type operand_type, Type result_type)
        {
            Syntax_Kind = syntax_kind;
            Kind = kind;
            Operand_Type = operand_type;
            Type = result_type;
        }

        public Syntax_Kind_of_Token Syntax_Kind { get; }
        public Bound_Unary_Operator_Kind Kind { get; }
        public Type Operand_Type { get; }
        public Type Type { get; }

        private static Bound_Unary_Operator[] _operators =
        {
                new Bound_Unary_Operator(Syntax_Kind_of_Token.Not_Token,Bound_Unary_Operator_Kind.Logical_Negation,typeof(bool)),

                new Bound_Unary_Operator(Syntax_Kind_of_Token.Plus_Token,Bound_Unary_Operator_Kind.Identity,typeof(int)),
                new Bound_Unary_Operator(Syntax_Kind_of_Token.Minus_Token,Bound_Unary_Operator_Kind.Negation,typeof(int))
        };

        public static Bound_Unary_Operator Bind(Syntax_Kind_of_Token syntax_kind, Type operand_type)
        {
            foreach (var op in _operators)
            {
                if (op.Syntax_Kind == syntax_kind && op.Operand_Type == operand_type)
                    return op;
            }
            return null;
        }
    }
}