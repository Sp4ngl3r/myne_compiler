using System.Collections.Generic;
using System.Linq;

namespace myne.Code_Analyzer
{
    public sealed class Syntax_Tree
    {
        public Syntax_Tree(IEnumerable<string> diagnostics, Expression_Syntax_Node root_node, Syntax_Tokens_Set end_of_file_object)
        {
            Diagnostics = diagnostics.ToArray();
            Root_Node = root_node;
            End_Of_File_Object = end_of_file_object;
        }

        public IReadOnlyCollection<string> Diagnostics { get; }
        public Expression_Syntax_Node Root_Node { get; }
        public Syntax_Tokens_Set End_Of_File_Object { get; }

        public static Syntax_Tree Parse(string text)
        {
            var parser_object = new Parser(text);
            return parser_object.Parse();
        }
    }
}