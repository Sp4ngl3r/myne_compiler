using System.Collections.Generic;

namespace myne.Code_Analyzer
{
    internal sealed class Lexer
    {
        private readonly string _text;
        private int _position;
        private List<string> _diagnostics = new List<string>();

        public Lexer(string text)
        {
            _text = text;
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private char Current_Character
        {
            get
            {
                if (_position >= _text.Length)
                    return '\0';

                return _text[_position];
            }
        }

        private void Next_Character()
        {
            _position++;
        }
        public Syntax_Tokens_Set Next_Token_in_Set()
        {
            // numbers
            // +  -  *  /  (  )
            // whitespace

            if (_position >= _text.Length)
            {
                return new Syntax_Tokens_Set(Syntax_Kind_of_Token.End_of_File_Token, _position, "\0", null);
            }

            if (char.IsDigit(Current_Character))
            {
                var start = _position;

                while (char.IsDigit(Current_Character))
                    Next_Character();

                var length_of_text = _position - start;
                var text = _text.Substring(start, length_of_text);

                if (!int.TryParse(text, out var value))
                    _diagnostics.Add($"The number {_text} is illegal Integer-32");

                return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Number_Tokens, start, text, value);
            }

            if (char.IsWhiteSpace(Current_Character))
            {
                var start = _position;

                while (char.IsWhiteSpace(Current_Character))
                    Next_Character();

                var length_of_text = _position - start;
                var text = _text.Substring(start, length_of_text);

                return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Whitespace_Tokens, start, text, null);
            }

            if (Current_Character == '+')
                return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Plus_Token, _position++, "+", null);

            else if (Current_Character == '-')
                return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Minus_Token, _position++, "-", null);

            else if (Current_Character == '*')
                return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Star_Token, _position++, "*", null);

            else if (Current_Character == '/')
                return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Slash_Token, _position++, "/", null);

            else if (Current_Character == '(')
                return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Open_Parenthesis_Token, _position++, "(", null);

            else if (Current_Character == ')')
                return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Closed_Parenthesis_Token, _position++, ")", null);

            _diagnostics.Add($"ERROR: Bad Character Input: '{Current_Character}'");

            return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Invalid_Token, _position++, _text.Substring(_position - 1, 1), null);
        }
    }
}