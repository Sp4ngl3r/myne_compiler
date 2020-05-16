namespace myne.Code_Analyzer
{
    public class Diagnostic
    {
        public Diagnostic(Text_Span span, string message)
        {
            Span = span;
            Message = message;
        }

        public Text_Span Span { get; }
        public string Message { get; }

        public override string ToString() => Message;
    }
}