using System;
using System.Collections.Generic;
using System.Linq;
using myne.Code_Analyzer;
using myne.Code_Analyzer.Binding;
using myne.Code_Analyzer.Syntax;

namespace myne
{
    internal static class Program
    {
        private static void Main()
        {
            var show_tree = false;
            while (true)
            {
                Console.WriteLine();
                Console.Write(" L(~ __ ~)__/  ");
                var input_line = Console.ReadLine();
                Console.WriteLine();

                if (string.IsNullOrWhiteSpace(input_line))
                    return;

                if (input_line == "#showtree")
                {
                    show_tree = !show_tree;
                    Console.WriteLine(show_tree ? "Displaying Parse trees." : "Not able to show Parse tree");
                    continue;
                }

                else if (input_line == "#cls")
                {
                    Console.Clear();
                    continue;
                }

                var syntax_tree_object = Syntax_Tree.Parse(input_line);
                var compilation = new Compilation(syntax_tree_object);
                var result = compilation.Evaluate();

                var diagnostics = result.Diagnostics;

                if (show_tree)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Sample_Print(syntax_tree_object.Root_Node);
                    Console.ResetColor();
                }

                if (!diagnostics.Any())
                {
                    Console.WriteLine(result.Value);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach (var diagnostic in diagnostics)
                        Console.WriteLine(diagnostic);

                    Console.ResetColor();
                }

                // var lexer_object = new Lexer(input_line);
                // while (true)
                // {
                //     var current_token = lexer_object.Next_Token_in_Set();
                //     if (current_token.Kind_Of_Token == Syntax_Kind_of_Token.End_of_File_Token)
                //         break;

                //     Console.Write($"{current_token.Kind_Of_Token} : '{current_token.Text}' ----> ");
                //     if (current_token.Value != null)
                //         Console.Write($"{current_token.Value}");

                //     Console.WriteLine();
                // }
            }
        }


        static void Sample_Print(Syntax_Node node, string indentation = "", bool is_last = true)
        {
            var marker = is_last ? "└──" : "├──";

            Console.Write(indentation);
            Console.Write(marker);
            Console.Write(node.Kind_Of_Token);

            if (node is Syntax_Tokens_Set t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indentation += is_last ? "   " : "|  ";

            var last_child = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                Sample_Print(child, indentation, child == last_child);
        }
    }
}
