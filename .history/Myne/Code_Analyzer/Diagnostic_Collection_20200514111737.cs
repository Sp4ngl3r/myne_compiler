using System;
using System.Collections;
using System.Collections.Generic;
using myne.Code_Analyzer.Syntax;

namespace myne.Code_Analyzer
{
    public sealed class Diagnostic_Collection : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();

        public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void AddRange(Diagnostic_Collection diagnostics)
        {
            _diagnostics.AddRange(diagnostics._diagnostics);
        }

        private void Report(Text_Span span, string message)
        {
            var diagnostic = new Diagnostic(span, message);
            _diagnostics.Add(diagnostic);
        }
        public void Report_Invalid_Number(Text_Span span, string text, Type type)
        {
            var message = $"The number {text} isn't valid {type} ......";
            Report(span, message);
        }

        public void Report_Invalid_Character(int position, char character)
        {
            var span = new Text_Span(position, 1);
            var message = $"Invalid Character Input: '{character}'......";
            Report(span, message);
        }
        public void Report_Unexpected_Token(Text_Span span, Syntax_Kind_of_Token actual_kind, Syntax_Kind_of_Token expected_kind)
        {
            var message = $"Unexpected token <{actual_kind}>, expected <{expected_kind}>......";
            Report(span, message);
        }
        public void Report_Undefined_Unary_Operator(Text_Span span, string operator_text, Type operand_type)
        {
            var message = $"Unary operator '{operator_text}' is not defined for type {operand_type}......";
            Report(span, message);
        }

        public void Report_Undefined_Binary_Operator(Text_Span span, string operator_text, Type left_type, Type right_type)
        {
            var message = $"Binary operator '{operator_text}' is not defined for types {left_type} and {right_type}....!";
            Report(span, message);
        }
    }
}