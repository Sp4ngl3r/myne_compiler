using System.Collections.Generic;

namespace myne.Code_Analyzer
{
    internal sealed class Parser
    {
        private readonly Syntax_Tokens_Set[] _tokens_set;
        private int _position;
        private List<string> _diagnostics = new List<string>();

        public Parser(string text)
        {
            var tokens_set = new List<Syntax_Tokens_Set>();
            var lexer_obj = new Lexer(text);
            Syntax_Tokens_Set token_obj;

            do
            {
                token_obj = lexer_obj.Next_Token_in_Set();

                if (token_obj.Kind_Of_Token != Syntax_Kind_of_Token.Whitespace_Tokens &&
                   token_obj.Kind_Of_Token != Syntax_Kind_of_Token.Invalid_Token)
                {
                    tokens_set.Add(token_obj);
                }
            } while (token_obj.Kind_Of_Token != Syntax_Kind_of_Token.End_of_File_Token);

            _tokens_set = tokens_set.ToArray();
            _diagnostics.AddRange(lexer_obj.Diagnostics);

        }

        public IEnumerable<string> Diagnostics => _diagnostics;

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

        private Syntax_Tokens_Set Match(Syntax_Kind_of_Token kind_of_token_object)
        {
            if (Current_Node.Kind_Of_Token == kind_of_token_object)
                return Next_Token_in_Set();

            _diagnostics.Add($"ERROR: Unexpected Token <{Current_Node.Kind_Of_Token}>, expected <{kind_of_token_object}>");

            return new Syntax_Tokens_Set(kind_of_token_object, Current_Node.Position, null, null);
        }

        public Syntax_Tree Parse()
        {
            var expression = Parse_for_Addition_and_Subtraction();
            var end_of_file_object = Match(Syntax_Kind_of_Token.End_of_File_Token);
            return new Syntax_Tree(_diagnostics, expression, end_of_file_object);
        }

        private Expression_Syntax_Node Parse_Expression()
        {
            return Parse_for_Addition_and_Subtraction();
        }
        private Expression_Syntax_Node Parse_for_Addition_and_Subtraction()
        {
            var left_expression = Parse_for_Multiplication_and_Division();

            while (Current_Node.Kind_Of_Token == Syntax_Kind_of_Token.Plus_Token ||
                   Current_Node.Kind_Of_Token == Syntax_Kind_of_Token.Minus_Token)
            {
                var operator_token = Next_Token_in_Set();
                var right_expression = Parse_for_Multiplication_and_Division();
                left_expression = new Binary_Expression_Syntax_Node(left_expression, operator_token, right_expression);
            }

            return left_expression;
        }

        private Expression_Syntax_Node Parse_for_Multiplication_and_Division()
        {
            var left_expression = Parse_Primary_Expression();

            while (Current_Node.Kind_Of_Token == Syntax_Kind_of_Token.Star_Token ||
                   Current_Node.Kind_Of_Token == Syntax_Kind_of_Token.Slash_Token)
            {
                var operator_token = Next_Token_in_Set();
                var right_expression = Parse_Primary_Expression();
                left_expression = new Binary_Expression_Syntax_Node(left_expression, operator_token, right_expression);
            }

            return left_expression;
        }


        private Expression_Syntax_Node Parse_Primary_Expression()
        {
            if (Current_Node.Kind_Of_Token == Syntax_Kind_of_Token.Open_Parenthesis_Token)
            {
                var left = Next_Token_in_Set();
                var expression = Parse_Expression();
                var right = Match(Syntax_Kind_of_Token.Closed_Parenthesis_Token);
                return new Parenthesized_Expression_Syntax_Node(left, expression, right);
            }

            var number_token = Match(Syntax_Kind_of_Token.Number_Tokens);
            return new Number_Expression_Syntax_Node(number_token);
        }
    }
}