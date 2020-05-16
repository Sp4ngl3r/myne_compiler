using System;
using System.Linq;
using myne.Code_Analyzer.Binding;
using myne.Code_Analyzer.Syntax;

namespace myne.Code_Analyzer
{
    public class Compilation
    {
        public Compilation(Syntax_Tree syntax)
        {
            Syntax = syntax;
        }

        public Syntax_Tree Syntax { get; }

        public Evaluation_Result Evaluate()
        {
            var binder = new Binder();
            var bound_expression = binder.Bind_Expression(Syntax.Root_Node);

            var diagnostics = Syntax.Diagnostics.Concat(binder.Diagnostics).ToArray();

            if (diagnostics.Any())
                return new Evaluation_Result(diagnostics, null);

            var evaluator = new Evaluator(bound_expression);
            var value = evaluator.Evaluate();

            return new Evaluation_Result(Array.Empty<string>(), value);
        }
    }
}