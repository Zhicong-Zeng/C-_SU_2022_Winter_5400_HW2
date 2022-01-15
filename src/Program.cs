using System;
using System.Collections.Generic;
using RecDescent.Parsers;
using RecDescent.Tree;

namespace RecDescent
{
    class Program
    {
        static void Main(string[] _)
        {
            var variables = new Dictionary<string, int>()
            {
                ["x"] = 7,
            };

            const string input = "3 + x";

            var parser = new RecDescentParser();
            var tree = parser.Parse(input);
            Console.WriteLine(tree.RenderTree());

            var eval = new TreeEvaluator();
            var result = eval.Evaluate(tree, variables);
            Console.WriteLine(result);
        }
    }
}
