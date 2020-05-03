using System;

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
                case Syntax_Kind_of_Token.Not_Token:
                    return 6;

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
                    return 5;

                case Syntax_Kind_of_Token.Plus_Token:
                case Syntax_Kind_of_Token.Minus_Token:
                    return 4;

                case Syntax_Kind_of_Token.Equals_Equals_Token:
                case Syntax_Kind_of_Token.Not_Equals_Token:
                    return 3;

                case Syntax_Kind_of_Token.Ampersand_Ampersand_Token:
                    return 2;

                case Syntax_Kind_of_Token.Pipe_Pipe_Token:
                    return 1;

                default:
                    return 0;
            }
        }

        public static Syntax_Kind_of_Token Get_Keyword_Kind(string text)
        {
            switch (text)
            {
                case "true":
                    return Syntax_Kind_of_Token.True_Keyword;

                case "false":
                    return Syntax_Kind_of_Token.False_Keyword;

                default:
                    return Syntax_Kind_of_Token.Identifier_Token;
            }

        }
    }
}