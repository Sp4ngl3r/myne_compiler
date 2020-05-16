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

                    foreach (var diagnostic in diagnostics)
                    {
                        //Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(diagnostic);
                        Console.ResetColor();

                        var prefix = input_line.Substring(0, diagnostic.Span.Start);
                        var error = input_line.Substring(diagnostic.Span.Start, diagnostic.Span.Length);
                        var suffix = input_line.Substring(diagnostic.Span.End);

                        Console.Write("   ");
                        Console.Write(prefix);

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(error);
                        Console.ResetColor();

                        Console.Write(suffix);
                        Console.WriteLine();
                    }
                }
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
