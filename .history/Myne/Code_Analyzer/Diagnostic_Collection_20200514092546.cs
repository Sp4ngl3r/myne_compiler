using System;
using System.Collections;
using System.Collections.Generic;

namespace myne.Code_Analyzer
{
    public sealed class Diagnostic_Collection : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();

        public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private void Report(Text_Span span, string message)
        {
            var diagnostic = new Diagnostic(span, message);
            _diagnostics.Add(diagnostic);
        }

        internal void Report_Invalid_Number(Text_Span span, string text, Type type)
        {
            var message = $"The number {text} isn't valid {type}";
            Report(span, message);
        }
    }
}