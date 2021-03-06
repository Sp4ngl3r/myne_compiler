using System.Collections.Generic;

namespace myne.Code_Analyzer.Syntax
{
    internal sealed class Parser
    {
        private readonly Syntax_Tokens_Set[] _tokens_set;
        private int _position;
        private Diagnostic_Collection _diagnostics = new Diagnostic_Collection();

        public Parser(string text)
        {
            var tokens_set = new List<Syntax_Tokens_Set>();
            var lexer_obj = new Lexer(text);
            Syntax_Tokens_Set token_obj;

            do
            {
                token_obj = lexer_obj.Lex();

                if (token_obj.Kind_Of_Token != Syntax_Kind_of_Token.Whitespace_Token &&
                    token_obj.Kind_Of_Token != Syntax_Kind_of_Token.Invalid_Token)
                {
                    tokens_set.Add(token_obj);
                }
            } while (token_obj.Kind_Of_Token != Syntax_Kind_of_Token.End_of_File_Token);

            _tokens_set = tokens_set.ToArray();
            _diagnostics.AddRange(lexer_obj.Diagnostics);

        }

        public Diagnostic_Collection Diagnostics => _diagnostics;

        private Syntax_Tokens_Set Peeking_Tokens(int offset)
        {
            var index_of_token = _position + offset;

            if (index_of_token >= _tokens_set.Length)
                return _tokens_set[_tokens_set.Length - 1];

            return _tokens_set[index_of_token];
        }

        private Syntax_Tokens_Set Current_Node => Peeking_Tokens(0);

        private Syntax_Tokens_Set Next_Token_in_Set()
        {
            var current_node_obj = Current_Node;
            _position++;
            return current_node_obj;
        }

        private Syntax_Tokens_Set Match_Token(Syntax_Kind_of_Token kind_of_token_object)
        {
            if (Current_Node.Kind_Of_Token == kind_of_token_object)
                return Next_Token_in_Set();

            _diagnostics.Report_Unexpected_Token(Current_Node.Span, Current_Node.Kind_Of_Token, kind_of_token_object);

            return new Syntax_Tokens_Set(kind_of_token_object, Current_Node.Position, null, null);
        }

        public Syntax_Tree Parse()
        {
            var expression = Parse_Expression();
            var end_of_file_object = Match_Token(Syntax_Kind_of_Token.End_of_File_Token);
            return new Syntax_Tree(_diagnostics, expression, end_of_file_object);
        }

        private Expression_Syntax_Node Parse_Expression()
        {
            return Parse_Assignment_Expression();
        }

        private Expression_Syntax_Node Parse_Assignment_Expression()
        {
            if (Peeking_Tokens(0).Kind_Of_Token == Syntax_Kind_of_Token.Identifier_Token &&
               Peeking_Tokens(1).Kind_Of_Token == Syntax_Kind_of_Token.Equals_Token)
            {
                var identifier_token = Next_Token_in_Set();
                var operator_token = Next_Token_in_Set();
                var right = Parse_Assignment_Expression();
                return new Assignment_Expression_Syntax_Node(identifier_token, operator_token, right);
            }
            return Parse_Binary_Expression();
        }

        private Expression_Syntax_Node Parse_Binary_Expression(int parent_precedence = 0)
        {
            Expression_Syntax_Node left;
            var unary_operator_precedence = Current_Node.Kind_Of_Token.Get_Unary_Operator_Precedence();

            if (unary_operator_precedence != 0 && unary_operator_precedence >= parent_precedence)
            {
                var operator_token = Next_Token_in_Set();
                var operand = Parse_Binary_Expression(unary_operator_precedence);
                left = new Unary_Expression_Syntax_Node(operator_token, operand);
            }
            else
            {
                left = Parse_Primary_Expression();
            }

            while (true)
            {
                var precedence = Current_Node.Kind_Of_Token.Get_Binary_Operator_Precedence();
                if (precedence == 0 || precedence <= parent_precedence)
                    break;

                var operator_token = Next_Token_in_Set();
                var right = Parse_Binary_Expression(precedence);
                left = new Binary_Expression_Syntax_Node(left, operator_token, right);
            }
            return left;
        }

        private Expression_Syntax_Node Parse_Primary_Expression()
        {
            switch (Current_Node.Kind_Of_Token)
            {
                case Syntax_Kind_of_Token.Open_Parenthesis_Token:
                    {
                        var left = Next_Token_in_Set();
                        var expression = Parse_Expression();
                        var right = Match_Token(Syntax_Kind_of_Token.Closed_Parenthesis_Token);
                        return new Parenthesized_Expression_Syntax_Node(left, expression, right);
                    }

                case Syntax_Kind_of_Token.False_Keyword:
                case Syntax_Kind_of_Token.True_Keyword:
                    {
                        var keyword_token = Next_Token_in_Set();
                        var value = keyword_token.Kind_Of_Token == Syntax_Kind_of_Token.True_Keyword;
                        return new Literal_Expression_Syntax_Node(keyword_token, value);
                    }

                case

            default:
                    {
                        var number_token = Match_Token(Syntax_Kind_of_Token.Number_Token);
                        return new Literal_Expression_Syntax_Node(number_token);
                    }

            }
        }
    }
}