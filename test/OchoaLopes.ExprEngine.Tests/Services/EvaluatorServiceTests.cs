using OchoaLopes.ExprEngine.Exceptions;
using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Literals;
using OchoaLopes.ExprEngine.Services;

namespace OchoaLopes.ExprEngine.Tests.Services
{
    [TestFixture]
    internal class EvaluatorServiceTests
    {
        [Test]
        public void EvaluateExpression_WithValidExpression_ReturnsExpectedResult()
        {
            // Arrange
            var evaluatorService = new EvaluatorService();
            var expression = new Add(new LiteralInteger(2), new LiteralInteger(3));
            var expected = 5;

            // Act
            var result = evaluatorService.EvaluateExpression(expression, new Dictionary<string, object>());

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void EvaluateExpression_WithUndefinedVariable_ThrowsKeyNotFoundException()
        {
            // Arrange
            var evaluatorService = new EvaluatorService();
            var expression = new Variable("undefinedVariable");

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => evaluatorService.EvaluateExpression(expression, new Dictionary<string, object>()));
        }

        [Test]
        public void EvaluateExpression_WithInvalidExpressionNodeType_ThrowsExpressionEvaluationException()
        {
            // Arrange
            var evaluatorService = new EvaluatorService();
            var invalidExpression = new InvalidExpression();

            // Act & Assert
            Assert.Throws<ExpressionEvaluationException>(() => evaluatorService.EvaluateExpression(invalidExpression, new Dictionary<string, object>()));
        }
    }

    // Example custom expression class for testing
    public class InvalidExpression : IExpression
    {
        public object Evaluate(IDictionary<string, object> variables)
        {
            return null;
        }
    }
}