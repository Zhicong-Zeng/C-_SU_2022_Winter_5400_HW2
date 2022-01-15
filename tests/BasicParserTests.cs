using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecDescent.Exceptions;
using RecDescent.Parsers;

namespace RecDescent.Tests
{
    [TestClass]
    public class BasicParserTests
    {
        [DataTestMethod]
        [DataRow("3", "(Goal (Expr (Term (Factor (3)))))", "single number")]
        [DataRow("-3", "(Goal (Expr (Term (Factor (-3)))))", "single negative number")]
        [DataRow("x", "(Goal (Expr (Term (Factor (x)))))", "single identifier")]
        [DataRow("3 + x", "(Goal (Expr (Term (Factor (3))) (ExprPrime (+ (Term (Factor (x)))))))", "simple addition")]
        public void ExpectedSuccessTests(string expr, string expected, string label)
        {
            // Arrange
            var parser = new RecDescentParser();

            // Act
            var tree = parser.Parse(expr);
            var rendering = tree.RenderTree();

            // Assert
            Assert.AreEqual(expected, rendering, label);
        }


        [DataTestMethod]
        [DataRow("+ 3 2", "prefix notation shouldn't work")]
        [DataRow("( 3 + x", "missing close paren")]
        [DataRow("( ( 3 + x )", "mismatched parens")]
        [DataRow("3 +", "Premature end of input")]
        public void ExpectedFailureTests(string expr, string label)
        {
            // Assert
            var exception = Assert.ThrowsException<ParseException>(() =>
            {
                // Arrange
                var parser = new RecDescentParser();

                // Act
                parser.Parse(expr);
            }, label);
        }
    }
}
