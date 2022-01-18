using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecDescent.Parsers;
using RecDescent.Tree;

namespace RecDescent.Tests
{
    [TestClass]
    public class EvaluationTests
    {
        private static readonly Dictionary<string, int> variables = new()
        {
            ["x"] = 7,
        };

        [DataTestMethod]
        [DataRow("3", 3, "single number")]
        [DataRow("-3", -3, "single negative number")]
        [DataRow("x", 7, "single identifier")]
        [DataRow("3 + x", 10, "simple addition")]
        [DataRow("3 - x", -4, "simple subtraction")]
        [DataRow("4 * 2", 8, "simple multiply")]
        [DataRow("8 / 4", 2, "simple divition")]
        [DataRow("3 + ( x * 2 )", 17, "with parens")]
        [DataRow("3 + 2 * 7", 17, "handle correct (not left-to-right) order of operations")]
        [DataRow("2 * 7 + 3", 17, "handle correct (left-to-right) order of operations")]
        public void ExpectedSuccessTests(string expr, int expected, string label)
        {
            // Arrange
            var parser = new RecDescentParser();
            var eval = new TreeEvaluator();

            // Act
            var tree = parser.Parse(expr);
            var result = eval.Evaluate(tree, variables);

            // Assert
            Assert.AreEqual(expected, result, label);
        }
    }
}
