namespace myne.Code_Analyzer.Syntax
{
    public enum Syntax_Kind_of_Token
    {

        //These are normal or basic tokens.
        Number_Token,
        Whitespace_Token,
        Plus_Token,
        Minus_Token,
        Star_Token,
        Slash_Token,
        Not_Token,
        Ampersand_Ampersand_Token,
        Pipe_Pipe_Token,
        Equals_Equals_Token,
        Not_Equals_Token,
        Open_Parenthesis_Token,
        Closed_Parenthesis_Token,
        Invalid_Token,
        End_of_File_Token,
        Identifier_Token,


        //These are Keywords
        True_Keyword,
        False_Keyword,


        //These are expression variables.
        Literal_Expression,
        Unary_Expression,
        Binary_Expression,
        Parenthesized_Expression,

    }
}