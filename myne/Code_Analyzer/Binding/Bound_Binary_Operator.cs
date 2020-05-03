using System;
using myne.Code_Analyzer.Syntax;

namespace myne.Code_Analyzer.Binding
{
    internal sealed class Bound_Binary_Operator
    {
        private Bound_Binary_Operator(Syntax_Kind_of_Token syntax_kind, Bound_Binary_Operator_Kind kind, Type type)
            : this(syntax_kind, kind, type, type, type)
        {

        }
        private Bound_Binary_Operator(Syntax_Kind_of_Token syntax_kind, Bound_Binary_Operator_Kind kind, Type operand_type, Type result_type)
            : this(syntax_kind, kind, operand_type, operand_type, result_type)
        {

        }
        private Bound_Binary_Operator(Syntax_Kind_of_Token syntax_kind, Bound_Binary_Operator_Kind kind, Type left_type, Type right_type, Type result_type)
        {
            Syntax_Kind = syntax_kind;
            Kind = kind;
            Left_Type = left_type;
            Right_Type = right_type;
            Type = result_type;
        }

        public Syntax_Kind_of_Token Syntax_Kind { get; }
        public Bound_Binary_Operator_Kind Kind { get; }
        public Type Left_Type { get; }
        public Type Right_Type { get; }
        public Type Type { get; }

        private static Bound_Binary_Operator[] _operators =
        {
                new Bound_Binary_Operator(Syntax_Kind_of_Token.Plus_Token,Bound_Binary_Operator_Kind.Addition,typeof(int)),
                new Bound_Binary_Operator(Syntax_Kind_of_Token.Minus_Token,Bound_Binary_Operator_Kind.Subtraction,typeof(int)),
                new Bound_Binary_Operator(Syntax_Kind_of_Token.Star_Token,Bound_Binary_Operator_Kind.Multiplication,typeof(int)),
                new Bound_Binary_Operator(Syntax_Kind_of_Token.Slash_Token,Bound_Binary_Operator_Kind.Division,typeof(int)),

                new Bound_Binary_Operator(Syntax_Kind_of_Token.Equals_Equals_Token,Bound_Binary_Operator_Kind.Equals,typeof(int),typeof(bool)),
                new Bound_Binary_Operator(Syntax_Kind_of_Token.Not_Equals_Token,Bound_Binary_Operator_Kind.Not_Equals,typeof(int),typeof(bool)),


                new Bound_Binary_Operator(Syntax_Kind_of_Token.Ampersand_Ampersand_Token,Bound_Binary_Operator_Kind.Logical_And,typeof(bool)),
                new Bound_Binary_Operator(Syntax_Kind_of_Token.Pipe_Pipe_Token,Bound_Binary_Operator_Kind.Logical_Or,typeof(bool)),

                new Bound_Binary_Operator(Syntax_Kind_of_Token.Equals_Equals_Token,Bound_Binary_Operator_Kind.Equals,typeof(bool)),
                new Bound_Binary_Operator(Syntax_Kind_of_Token.Not_Equals_Token,Bound_Binary_Operator_Kind.Not_Equals,typeof(bool))
        };
        public static Bound_Binary_Operator Bind(Syntax_Kind_of_Token syntax_kind, Type left_type, Type right_type)
        {
            foreach (var op in _operators)
            {
                if (op.Syntax_Kind == syntax_kind && op.Left_Type == left_type && op.Right_Type == right_type)
                    return op;
            }
            return null;
        }
    }
}