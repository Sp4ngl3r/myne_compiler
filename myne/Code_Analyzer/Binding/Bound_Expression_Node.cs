using System;

namespace myne.Code_Analyzer.Binding
{
    internal abstract class Bound_Expression_Node : Bound_Node
    {
        public abstract Type Type { get; }
    }
}