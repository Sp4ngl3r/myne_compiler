namespace myne.Code_Analyzer.Syntax
{
    internal static class Syntax_Operator_Precedence
    {
        public static int Get_Unary_Operator_Precedence(this Syntax_Kind_of_Token kind_of_token_object)
        {
            switch (kind_of_token_object)
            {
                case Syntax_Kind_of_Token.Plus_Token:
                case Syntax_Kind_of_Token.Minus_Token:
                    return 3;

                default:
                    return 0;
            }
        }
        public static int Get_Binary_Operator_Precedence(this Syntax_Kind_of_Token kind_of_token_object)
        {
            switch (kind_of_token_object)
            {
                case Syntax_Kind_of_Token.Star_Token:
                case Syntax_Kind_of_Token.Slash_Token:
                    return 2;
                case Syntax_Kind_of_Token.Plus_Token:
                case Syntax_Kind_of_Token.Minus_Token:
                    return 1;

                default:
                    return 0;
            }
        }
    }
}