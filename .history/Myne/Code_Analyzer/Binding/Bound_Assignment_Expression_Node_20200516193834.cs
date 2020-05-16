using System;

namespace myne.Code_Analyzer.Binding
{
    internal sealed class Bound_Assignment_Expression_Node : Bound_Expression_Node
    {
        public Bound_Assignment_Expression_Node(Variable_Symbol variable, Bound_Expression_Node expression)
        {
            Variable = variable;
            Expression = expression;
        }

        public override Type Type => Expression.Type;
        public override Bound_Node_Kind Kind => Bound_Node_Kind.Assignment_Expression;
        public Variable_Symbol Variable { get; }
        public Bound_Expression_Node Expression { get; }
    }
}