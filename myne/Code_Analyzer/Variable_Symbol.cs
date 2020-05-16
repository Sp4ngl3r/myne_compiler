using System;

namespace myne.Code_Analyzer
{
    public sealed class Variable_Symbol
    {
        internal Variable_Symbol(string name,Type type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; }
        public Type Type { get; }
    }
}