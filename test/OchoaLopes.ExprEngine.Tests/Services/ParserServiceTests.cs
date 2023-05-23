using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;
using OchoaLopes.ExprEngine.Services;

namespace OchoaLopes.ExprEngine.Tests.Services
{
    [TestFixture]
    internal class ParserServiceTests
    {
        private IParserService _parserService;
        private ILexerService _lexerService;

        [SetUp]
        public void Setup()
        {
            _lexerService = new LexerService();
            _parserService = new ParserService();
        }

        [Test]
        public void Parse_WithValidExpression_ReturnsParsedExpression()
        {
            // Arrange
            var expression = _lexerService.Tokenize("2i + 3i");
            var expected = new Add(new Literal(2), new Literal(3));

            // Act
            var result = _parserService.Parse(expression);

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expected.GetType()));
            Assert.That((result as BinaryOperation)?.Left.ToString(), Is.EqualTo(expected.Left.ToString()));
            Assert.That((result as BinaryOperation)?.Right.ToString(), Is.EqualTo(expected.Right.ToString()));
        }

        [Test]
        public void Parse_WithInvalidExpression_ThrowsInvalidOperationException()
        {
            // Arrange
            var expression = _lexerService.Tokenize("2i +");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _parserService.Parse(expression));
        }

        [Test]
        public void Parse_WithNestedParentheses_ReturnsCorrectExpression()
        {
            // Arrange
            var expression = _lexerService.Tokenize("((2i + 3i) * 4i)");
            var expected = new Multiply(new Add(new Literal(2), new Literal(3)), new Literal(4));

            // Act
            var result = _parserService.Parse(expression);

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expected.GetType()));
        }

        [Test]
        public void Parse_WithUnaryOperator_ReturnsCorrectExpression()
        {
            // Arrange
            var expression = _lexerService.Tokenize("-3i + 4i");
            var expected = new Add(new UnaryMinus(new Literal(3)), new Literal(4));

            // Act
            var result = _parserService.Parse(expression);

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expected.GetType()));
        }

        [Test]
        public void Parse_WithVariable_ReturnsCorrectExpression()
        {
            // Arrange
            var expression = _lexerService.Tokenize(":x + 3d");
            var expected = new Add(new Variable("x"), new Literal(3));

            // Act
            var result = _parserService.Parse(expression);

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expected.GetType()));
        }

        [Test]
        public void Parse_WithMisplacedOperators_ThrowsInvalidOperationException()
        {
            // Arrange
            var expression = _lexerService.Tokenize("2i +* 3i");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _parserService.Parse(expression));
        }

        [Test]
        public void Parse_WithMissingParentheses_ThrowsInvalidOperationException()
        {
            // Arrange
            var expression = _lexerService.Tokenize("(2D + 3d * 4f");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _parserService.Parse(expression));
        }
    }
}