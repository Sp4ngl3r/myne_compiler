using System;

namespace myne.Code_Analyzer.Binding
{
    internal sealed class Bound_Binary_Expression_Node : Bound_Expression_Node
    {

        public Bound_Binary_Expression_Node(Bound_Expression_Node left, Bound_Binary_Operator op, Bound_Expression_Node right)
        {
            Left = left;
            Op = op;
            Right = right;
        }

        public override Type Type => Op.Type;
        public override Bound_Node_Kind Kind => Bound_Node_Kind.Binary_Expression_Node;
        public Bound_Expression_Node Left { get; }
        public Bound_Binary_Operator Op { get; }
        public Bound_Expression_Node Right { get; }

    }
}