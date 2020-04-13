namespace myne.Code_Analyzer
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
        Open_Parenthesis_Token,
        Closed_Parenthesis_Token,
        Invalid_Token,
        End_of_File_Token,


        //These are expression variables.
        Literal_Expression,
        Unary_Expression,
        Binary_Expression,
        Parenthesized_Expression,

    }
}