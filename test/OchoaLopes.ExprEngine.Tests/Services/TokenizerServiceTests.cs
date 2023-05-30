using System.Globalization;
using OchoaLopes.ExprEngine.Services;

namespace OchoaLopes.ExprEngine.Tests.Services
{
    public class TokenizerServiceTests
	{
        private TokenizerService _tokenizerService;

        [SetUp]
        public void Setup()
        {
            _tokenizerService = new TokenizerService(CultureInfo.InvariantCulture);
        }

        [Test]
        public void Test_TokenizeExpression_IsNullExpression()
        {
            // Arrange && Act
            var tokens = _tokenizerService.TokenizeExpression(":x is null");

            // Assert
            CollectionAssert.AreEqual(new[] { ":x", "is", "null" }, tokens);
        }

        [Test]
        public void Test_TokenizeComplexExpression_IsNullExpression()
        {
            // Arrange && Act
            var tokens = _tokenizerService.TokenizeExpression("(:x is null && (:y >= 10i)) is null");

            // Assert
            CollectionAssert.AreEqual(new[] { "(",":x", "is", "null", "&&", "(", ":y", ">=", "10i", ")", ")", "is", "null" }, tokens);
        }

        [Test]
        public void Test_TokenizeExpression_IsNotNullExpression()
        {
            // Arrange && Act
            var tokens = _tokenizerService.TokenizeExpression(":x is not null");

            // Assert
            CollectionAssert.AreEqual(new[] { ":x", "is not", "null" }, tokens);
        }

        [Test]
        public void Test_TokenizeComplexExpression_IsNotNullExpression()
        {
            // Arrange && Act
            var tokens = _tokenizerService.TokenizeExpression("(:x is :y && (:y >= 10i)) is not null");

            // Assert
            CollectionAssert.AreEqual(new[] { "(", ":x", "is", ":y", "&&", "(", ":y", ">=", "10i", ")", ")", "is not", "null" }, tokens);
        }

        [Test]
        public void Test_TokenizeExpression_AndExpression()
        {
            // Arrange && Act
            var tokens = _tokenizerService.TokenizeExpression(":x > 10i and :x < 20i");

            // Assert
            CollectionAssert.AreEqual(new[] { ":x", ">", "10i", "and", ":x", "<", "20i" }, tokens);
        }

        [Test]
        public void Test_TokenizeExpression_OrExpression()
        {
            // Arrange && Act
            var tokens = _tokenizerService.TokenizeExpression(":x > 10i or :x < 20i");

            // Assert
            CollectionAssert.AreEqual(new[] { ":x", ">", "10i", "or", ":x", "<", "20i" }, tokens);
        }

        [Test]
        public void Test_TokenizeExpression_StartsWithExpression()
        {
            // Arrange && Act
            var tokens = _tokenizerService.TokenizeExpression(":x == %'starts with'");

            // Assert
            CollectionAssert.AreEqual(new[] { ":x", "==", "%'starts with'" }, tokens);
        }

        [Test]
        public void Test_TokenizeExpression_EndsWithExpression()
        {
            // Arrange && Act
            var tokens = _tokenizerService.TokenizeExpression(":x == 'ends with'%");

            // Assert
            CollectionAssert.AreEqual(new[] { ":x", "==", "'ends with'%" }, tokens);
        }

        [Test]
        public void Test_TokenizeExpression_ContainsExpression()
        {
            // Arrange && Act
            var tokens = _tokenizerService.TokenizeExpression(":x == %'contains with'%");

            // Assert
            CollectionAssert.AreEqual(new[] { ":x", "==", "%'contains with'%" }, tokens);
        }

        [Test]
        public void Test_TokenizeExpression_LikeExpression()
        {
            // Arrange && Act
            var tokens = _tokenizerService.TokenizeExpression(":x like %'contains with'%");

            // Assert
            CollectionAssert.AreEqual(new[] { ":x", "like", "%'contains with'%" }, tokens);
        }

        [Test]
        public void Test_TokenizeExpression_NotLikeExpression()
        {
            // Arrange && Act
            var tokens = _tokenizerService.TokenizeExpression(":x not like %'contains with'%");

            // Assert
            CollectionAssert.AreEqual(new[] { ":x", "not like", "%'contains with'%" }, tokens);
        }
    }
}

