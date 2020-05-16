using System.Collections.Generic;
using System.Linq;

namespace myne.Code_Analyzer
{
    public sealed class Evaluation_Result
    {
        public Evaluation_Result(IEnumerable<string> diagnostics, object value)
        {
            Diagnostics = diagnostics.ToArray();
            Value = value;
        }

        public IReadOnlyList<string> Diagnostics { get; }
        public object Value { get; }
    }
}