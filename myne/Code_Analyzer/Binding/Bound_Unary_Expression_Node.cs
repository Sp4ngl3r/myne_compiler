using System;

namespace myne.Code_Analyzer.Binding
{
    internal sealed class Bound_Unary_Expression_Node : Bound_Expression_Node
    {

        public Bound_Unary_Expression_Node(Bound_Unary_Operator op, Bound_Expression_Node operand)
        {
            Op = op;
            Operand = operand;
        }
        public override Type Type => Op.Type;
        public override Bound_Node_Kind Kind => Bound_Node_Kind.Unary_Expression_Node;
        public Bound_Unary_Operator Op { get; }
        public Bound_Expression_Node Operand { get; }


    }
}