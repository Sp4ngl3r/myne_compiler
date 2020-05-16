using System;

namespace myne.Code_Analyzer.Binding
{
    internal sealed class Bound_Variable_Expression_Node: Bound_Expression_Node
    {
        public Bound_Variable_Expression_Node(string name,Type type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public override Type Type { get; }
        public override Bound_Node_Kind Kind => Bound_Node_Kind.Variable_Expression;
    }
}