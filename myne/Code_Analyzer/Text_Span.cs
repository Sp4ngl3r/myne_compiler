namespace myne.Code_Analyzer
{
    public struct Text_Span
    {
        public Text_Span(int start, int length)
        {
            Start = start;
            Length = length;
        }

        public int Start { get; }
        public int Length { get; }
        public int End => Start + Length;
    }
}