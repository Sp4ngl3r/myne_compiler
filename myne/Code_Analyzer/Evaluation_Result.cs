using System.Collections.Generic;
using System.Linq;

namespace myne.Code_Analyzer
{
    public sealed class Evaluation_Result
    {
        public Evaluation_Result(IEnumerable<Diagnostic> diagnostics, object value)
        {
            Diagnostics = diagnostics.ToArray();
            Value = value;
        }

        public IReadOnlyList<Diagnostic> Diagnostics { get; }
        public object Value { get; }
    }
}