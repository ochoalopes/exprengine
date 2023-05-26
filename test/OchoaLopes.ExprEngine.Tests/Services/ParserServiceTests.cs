using System.Globalization;
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
            _lexerService = new LexerService(new TokenizerService(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            _parserService = new ParserService(CultureInfo.InvariantCulture);
        }

        [Test]
        public void Parse_WithValidExpression_ReturnsParsedExpression()
        {
            // Arrange
            var expression = _lexerService.LexExpression("2i + 3i");
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
            var expression = _lexerService.LexExpression("2i +");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _parserService.Parse(expression));
        }

        [Test]
        public void Parse_WithNestedParentheses_ReturnsCorrectExpression()
        {
            // Arrange
            var expression = _lexerService.LexExpression("((2i + 3i) * 4i)");
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
            var expression = _lexerService.LexExpression("-3i + 4i");
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
            var expression = _lexerService.LexExpression(":x + 3d");
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
            var expression = _lexerService.LexExpression("2i +* 3i");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _parserService.Parse(expression));
        }

        [Test]
        public void Parse_WithMissingParentheses_ThrowsInvalidOperationException()
        {
            // Arrange
            var expression = _lexerService.LexExpression("(2D + 3d * 4f");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _parserService.Parse(expression));
        }

        [Test]
        public void Parse_BrazilianCultureInfoFormat_ReturnsCorrectExpression()
        {
            // Arrange
            var expression = _lexerService.LexExpression("(2,4D + 3,5d * 4,6f)", new CultureInfo("pt-BR"));
            var expected = new Add(new Literal(2.4), new Multiply(new Literal(3.5), new Literal(4.6)));

            // Act
            var result = _parserService.Parse(expression, new CultureInfo("pt-BR"));

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expected.GetType()));
        }

        [Test]
        public void Parse_WithValidDateExpression_ReturnsParsedExpression()
        {
            // Arrange
            var expression = _lexerService.LexExpression("(:birthDate == '2000-01-01't)");
            var expected = new Equal(new Variable("birthDate"), new Literal(new DateTime(2000, 1, 1)));

            // Act
            var result = _parserService.Parse(expression);

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expected.GetType()));
            Assert.That((result as BinaryOperation)?.Left.ToString(), Is.EqualTo(expected.Left.ToString()));
            Assert.That((result as BinaryOperation)?.Right.ToString(), Is.EqualTo(expected.Right.ToString()));
        }

        [Test]
        public void Parse_BrazilianDateFormat_ReturnsCorrectExpression()
        {
            // Arrange
            var brazilianCulture = new CultureInfo("pt-BR");
            var expression = _lexerService.LexExpression(":birthDate == '01/01/2001't", brazilianCulture);
            var expected = new Equal(new Variable("birthDate"), new Literal(new DateTime(2001, 01, 01)));

            // Act
            var result = _parserService.Parse(expression, brazilianCulture);

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expected.GetType()));
        }

        [Test]
        public void Parse_FrenchDateFormat_ReturnsCorrectExpression()
        {
            // Arrange
            var frenchCulture = new CultureInfo("fr-FR");
            var expression = _lexerService.LexExpression(":birthDate == '01/01/2001't", frenchCulture);
            var expected = new Equal(new Variable("birthDate"), new Literal(new DateTime(2001, 01, 01)));

            // Act
            var result = _parserService.Parse(expression, frenchCulture);

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expected.GetType()));
        }

        [Test]
        public void Parse_AmericanDateFormat_ReturnsCorrectExpression()
        {
            // Arrange
            var americanCulture = new CultureInfo("en-US");
            var expression = _lexerService.LexExpression(":birthDate == '01/01/2001't", americanCulture);
            var expected = new Equal(new Variable("birthDate"), new Literal(new DateTime(2001, 01, 01)));

            // Act
            var result = _parserService.Parse(expression, americanCulture);

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expected.GetType()));
        }

        [Test]
        public void Parse_JapaneseDateFormat_ReturnsCorrectExpression()
        {
            // Arrange
            var japaneseCulture = new CultureInfo("ja-JP");
            var expression = _lexerService.LexExpression(":birthDate == '2001/01/01't", japaneseCulture);
            var expected = new Equal(new Variable("birthDate"), new Literal(new DateTime(2001, 01, 01)));

            // Act
            var result = _parserService.Parse(expression, japaneseCulture);

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expected.GetType()));
        }
    }
}