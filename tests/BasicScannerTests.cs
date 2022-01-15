using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecDescent.Exceptions;
using RecDescent.Scanners;

namespace RecDescent.Tests
{
    [TestClass]
    public class BasicScannerTests
    {
        [DataTestMethod]
        [DataRow("3", "<Number, 3>", "Number")]
        [DataRow("3 + x", "<Number, 3> <AddOperator, +> <Identifier, x>", "3 + x")]
        public void ExpectedSuccessTests(string expr, string expected, string label)
        {
            // Arrange
            var scanner = new Scanner();

            // Act
            var stream = scanner.GenerateTokens(expr);
            var tokens = string.Join(" ", stream.Select(t => t.ToString()));

            // Assert
            Assert.AreEqual(expected, tokens, label);
        }

        [DataTestMethod]
        [DataRow("3.14159", "doesn't handle real numbers yet")]
        public void ExpectedFailureTests(string expr, string label)
        {
            // Assert
            var exception = Assert.ThrowsException<UnknownTokenException>(() =>
            {
                // Arrange
                var scanner = new Scanner();

                // Act
                scanner.GenerateTokens(expr);
            }, label);
        }
    }
}