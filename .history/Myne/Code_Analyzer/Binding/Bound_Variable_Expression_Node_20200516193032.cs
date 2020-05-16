using System;

namespace myne.Code_Analyzer.Binding
{
    internal sealed class Bound_Variable_Expression_Node : Bound_Expression_Node
    {
        public Bound_Variable_Expression_Node(Variable_Symbol variable)
        {
            Variable = variable;
        }
        public override Type Type => Variable.Type;
        public override Bound_Node_Kind Kind => Bound_Node_Kind.Variable_Expression;
        public Variable_Symbol Variable { get; }
    }
}