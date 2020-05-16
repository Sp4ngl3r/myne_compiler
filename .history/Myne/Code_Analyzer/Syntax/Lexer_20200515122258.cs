using System.Collections.Generic;

namespace myne.Code_Analyzer.Syntax
{
    internal sealed class Lexer
    {
        private readonly string _text;
        private int _position;
        private Diagnostic_Collection _diagnostics = new Diagnostic_Collection();

        public Lexer(string text)
        {
            _text = text;
        }

        public Diagnostic_Collection Diagnostics => _diagnostics;

        private char Current_Character => Peek(0);
        private char Look_Ahead => Peek(1);

        private char Peek(int offset)
        {
            var index = _position + offset;

            if (index >= _text.Length)
                return '\0';

            return _text[index];
        }

        private void Next_Character()
        {
            _position++;
        }
        public Syntax_Tokens_Set Lex()
        {

            if (_position >= _text.Length)
            {
                return new Syntax_Tokens_Set(Syntax_Kind_of_Token.End_of_File_Token, _position, "\0", null);
            }

            var start = _position;

            if (char.IsDigit(Current_Character))
            {
                while (char.IsDigit(Current_Character))
                    Next_Character();

                var length_of_text = _position - start;
                var text = _text.Substring(start, length_of_text);

                if (!int.TryParse(text, out var value))
                    _diagnostics.Report_Invalid_Number(new Text_Span(start, length_of_text), _text, typeof(int));

                return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Number_Token, start, text, value);
            }

            if (char.IsLetter(Current_Character))
            {
                while (char.IsLetter(Current_Character))
                    Next_Character();

                var length_of_text = _position - start;
                var text = _text.Substring(start, length_of_text);
                var kind = Syntax_Operator_Precedence.Get_Keyword_Kind(text);

                return new Syntax_Tokens_Set(kind, start, text, null);
            }

            if (char.IsWhiteSpace(Current_Character))
            {
                while (char.IsWhiteSpace(Current_Character))
                    Next_Character();

                var length_of_text = _position - start;
                var text = _text.Substring(start, length_of_text);

                return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Whitespace_Token, start, text, null);
            }

            switch (Current_Character)
            {
                case '+':
                    return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Plus_Token, _position++, "+", null);
                case '-':
                    return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Minus_Token, _position++, "-", null);
                case '*':
                    return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Star_Token, _position++, "*", null);
                case '/':
                    return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Slash_Token, _position++, "/", null);
                case '(':
                    return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Open_Parenthesis_Token, _position++, "(", null);
                case ')':
                    return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Closed_Parenthesis_Token, _position++, ")", null);
                case '&':
                    if (Look_Ahead == '&')
                    {
                        _position += 2;
                        return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Ampersand_Ampersand_Token, start, "&&", null);
                    }
                    break;
                case '|':
                    if (Look_Ahead == '|')
                    {
                        _position += 2;
                        return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Pipe_Pipe_Token, start, "||", null);
                    }
                    break;
                case '=':
                    if (Look_Ahead == '=')
                    {
                        _position += 2;
                        return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Equals_Equals_Token, start, "==", null);
                    }
                    break;
                case '!':
                    if (Look_Ahead == '=')
                    {
                        _position += 2;
                        return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Not_Equals_Token, start, "!=", null);
                    }
                    else
                    {
                        _position += 1;
                        return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Not_Token, start, "!", null);
                    }
            }

            _diagnostics.Report_Invalid_Character(_position, Current_Character);

            return new Syntax_Tokens_Set(Syntax_Kind_of_Token.Invalid_Token, _position++, _text.Substring(_position - 1, 1), null);
        }
    }
}