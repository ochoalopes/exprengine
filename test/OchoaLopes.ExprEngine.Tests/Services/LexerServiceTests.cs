using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Services;
using OchoaLopes.ExprEngine.ValueObjects;

namespace OchoaLopes.ExprEngine.Tests.Services
{
    [TestFixture]
    internal class LexerServiceTests
	{
        private ILexerService lexer;

        [SetUp]
        public void SetUp()
        {
            lexer = new LexerService();
        }

        [Test]
        public void Tokenize_UserName_And_Age_Test()
        {
            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.Variable, "userName"),
                new Token(TokenTypeEnum.Equal, "=="),
                new Token(TokenTypeEnum.LiteralString, "Test"),
                new Token(TokenTypeEnum.And, "&&"),
                new Token(TokenTypeEnum.Variable, "userAge"),
                new Token(TokenTypeEnum.GreaterThanOrEqual, ">="),
                new Token(TokenTypeEnum.LiteralInteger, "18"),
            };

            var actualTokens = lexer.Tokenize(":userName == 'Test' && :userAge >= 18i");

            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_With_More_Conditions_Test()
        {
            string expression = ":userName == 'Test' && :userAge >= 18i || :userType != 'Admin' && ( :isActive || :isBlocked )";

            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.Variable, "userName"),
                new Token(TokenTypeEnum.Equal, "=="),
                new Token(TokenTypeEnum.LiteralString, "Test"),
                new Token(TokenTypeEnum.And, "&&"),
                new Token(TokenTypeEnum.Variable, "userAge"),
                new Token(TokenTypeEnum.GreaterThanOrEqual, ">="),
                new Token(TokenTypeEnum.LiteralInteger, "18"),
                new Token(TokenTypeEnum.Or, "||"),
                new Token(TokenTypeEnum.Variable, "userType"),
                new Token(TokenTypeEnum.NotEqual, "!="),
                new Token(TokenTypeEnum.LiteralString, "Admin"),
                new Token(TokenTypeEnum.And, "&&"),
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "isActive"),
                new Token(TokenTypeEnum.Or, "||"),
                new Token(TokenTypeEnum.Variable, "isBlocked"),
                new Token(TokenTypeEnum.RightParenthesis, ")")
            };

            var actualTokens = lexer.Tokenize(expression);

            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_With_LiteralIntegers_Test()
        {
            string expression = "(:value + 15i) > (:product / 2D) || (:value % 2i == 0i)";

            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "value"),
                new Token(TokenTypeEnum.Add, "+"),
                new Token(TokenTypeEnum.LiteralInteger, "15"),
                new Token(TokenTypeEnum.RightParenthesis, ")"),
                new Token(TokenTypeEnum.GreaterThan, ">"),
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "product"),
                new Token(TokenTypeEnum.Divide, "/"),
                new Token(TokenTypeEnum.LiteralDouble, "2"),
                new Token(TokenTypeEnum.RightParenthesis, ")"),
                new Token(TokenTypeEnum.Or, "||"),
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "value"),
                new Token(TokenTypeEnum.Modulo, "%"),
                new Token(TokenTypeEnum.LiteralInteger, "2"),
                new Token(TokenTypeEnum.Equal, "=="),
                new Token(TokenTypeEnum.LiteralInteger, "0"),
                new Token(TokenTypeEnum.RightParenthesis, ")"),
            };

            var actualTokens = lexer.Tokenize(expression);

            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_With_LiteralDecimals_Test()
        {
            string expression = "(:value + 15.2D) > (:product / 2.7D) || (:value % 2i == 0i)";

            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "value"),
                new Token(TokenTypeEnum.Add, "+"),
                new Token(TokenTypeEnum.LiteralDouble, "15.2"),
                new Token(TokenTypeEnum.RightParenthesis, ")"),
                new Token(TokenTypeEnum.GreaterThan, ">"),
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "product"),
                new Token(TokenTypeEnum.Divide, "/"),
                new Token(TokenTypeEnum.LiteralDouble, "2.7"),
                new Token(TokenTypeEnum.RightParenthesis, ")"),
                new Token(TokenTypeEnum.Or, "||"),
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "value"),
                new Token(TokenTypeEnum.Modulo, "%"),
                new Token(TokenTypeEnum.LiteralInteger, "2"),
                new Token(TokenTypeEnum.Equal, "=="),
                new Token(TokenTypeEnum.LiteralInteger, "0"),
                new Token(TokenTypeEnum.RightParenthesis, ")"),
            };

            var actualTokens = lexer.Tokenize(expression);

            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_With_LiteralStrings_Test()
        {
            string expression = ":userName == 'John Doe' || :country == 'United States'";

            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.Variable, "userName"),
                new Token(TokenTypeEnum.Equal, "=="),
                new Token(TokenTypeEnum.LiteralString, "John Doe"),
                new Token(TokenTypeEnum.Or, "||"),
                new Token(TokenTypeEnum.Variable, "country"),
                new Token(TokenTypeEnum.Equal, "=="),
                new Token(TokenTypeEnum.LiteralString, "United States")
            };

            var actualTokens = lexer.Tokenize(expression);

            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_With_LiteralDoubles_Test()
        {
            string expression = "(:value + 15.123d) > (:product / 2.123d) || (:value % 2.1d == 0i)";

            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "value"),
                new Token(TokenTypeEnum.Add, "+"),
                new Token(TokenTypeEnum.LiteralDecimal, "15.123"),
                new Token(TokenTypeEnum.RightParenthesis, ")"),
                new Token(TokenTypeEnum.GreaterThan, ">"),
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "product"),
                new Token(TokenTypeEnum.Divide, "/"),
                new Token(TokenTypeEnum.LiteralDecimal, "2.123"),
                new Token(TokenTypeEnum.RightParenthesis, ")"),
                new Token(TokenTypeEnum.Or, "||"),
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "value"),
                new Token(TokenTypeEnum.Modulo, "%"),
                new Token(TokenTypeEnum.LiteralDecimal, "2.1"),
                new Token(TokenTypeEnum.Equal, "=="),
                new Token(TokenTypeEnum.LiteralInteger, "0"),
                new Token(TokenTypeEnum.RightParenthesis, ")"),
            };

            var actualTokens = lexer.Tokenize(expression);

            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

    }
}