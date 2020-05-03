using System;

namespace myne.Code_Analyzer.Binding
{
    internal sealed class Bound_Literal_Expression_Node : Bound_Expression_Node
    {
        public Bound_Literal_Expression_Node(object value)
        {
            Value = value;
        }
        public override Type Type => Value.GetType();
        public override Bound_Node_Kind Kind => Bound_Node_Kind.Literal_Expression_Node;
        public object Value { get; }        
    }
}