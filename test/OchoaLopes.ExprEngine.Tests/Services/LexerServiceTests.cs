using System.Globalization;
using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Services;
using OchoaLopes.ExprEngine.ValueObjects;

namespace OchoaLopes.ExprEngine.Tests.Services
{
    [TestFixture]
    internal class LexerServiceTests
	{
        private ILexerService _lexer;

        [SetUp]
        public void SetUp()
        {
            _lexer = new LexerService(new TokenizerService(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        }

        [Test]
        public void Tokenize_UserName_And_Age_Test()
        {
            // Arrange
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

            // Act
            var actualTokens = _lexer.LexExpression(":userName == 'Test' && :userAge >= 18i");

            // Assert
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
            // Arrange
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

            // Act
            var actualTokens = _lexer.LexExpression(expression);

            // Assert
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
            // Arrange
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

            // Act
            var actualTokens = _lexer.LexExpression(expression);

            // Assert
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
            // Arrange
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

            // Act
            var actualTokens = _lexer.LexExpression(expression);

            // Assert
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
            // Arrange
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

            // Act
            var actualTokens = _lexer.LexExpression(expression);

            // Assert
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
            // Arrange
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

            // Act
            var actualTokens = _lexer.LexExpression(expression);

            // Assert
            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_With_LiteralDates_Test()
        {
            // Arrange
            string expression = "(:birthDate == '2000-01-01't && :anniversary == '2010-12-31't)";

            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "birthDate"),
                new Token(TokenTypeEnum.Equal, "=="),
                new Token(TokenTypeEnum.LiteralDateTime, "2000-01-01"),
                new Token(TokenTypeEnum.And, "&&"),
                new Token(TokenTypeEnum.Variable, "anniversary"),
                new Token(TokenTypeEnum.Equal, "=="),
                new Token(TokenTypeEnum.LiteralDateTime, "2010-12-31"),
                new Token(TokenTypeEnum.RightParenthesis, ")")
            };

            // Act
            var actualTokens = _lexer.LexExpression(expression);

            // Assert
            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_With_Mixed_LiteralDates_And_Other_Types_Test()
        {
            // Arrange
            string expression = ":age >= 18i && :birthDate == '2000-01-01't || :anniversary < '2010-12-31't";

            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.Variable, "age"),
                new Token(TokenTypeEnum.GreaterThanOrEqual, ">="),
                new Token(TokenTypeEnum.LiteralInteger, "18"),
                new Token(TokenTypeEnum.And, "&&"),
                new Token(TokenTypeEnum.Variable, "birthDate"),
                new Token(TokenTypeEnum.Equal, "=="),
                new Token(TokenTypeEnum.LiteralDateTime, "2000-01-01"),
                new Token(TokenTypeEnum.Or, "||"),
                new Token(TokenTypeEnum.Variable, "anniversary"),
                new Token(TokenTypeEnum.LessThan, "<"),
                new Token(TokenTypeEnum.LiteralDateTime, "2010-12-31")
            };

            // Act
            var actualTokens = _lexer.LexExpression(expression);

            // Assert
            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_With_DatesWithoutFormat_ShouldBe_LiteralString()
        {
            // Arrange
            string expression = ":age >= 18i && :birthDate == '2000-01-01' || :anniversary < '2010-12-31'";

            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.Variable, "age"),
                new Token(TokenTypeEnum.GreaterThanOrEqual, ">="),
                new Token(TokenTypeEnum.LiteralInteger, "18"),
                new Token(TokenTypeEnum.And, "&&"),
                new Token(TokenTypeEnum.Variable, "birthDate"),
                new Token(TokenTypeEnum.Equal, "=="),
                new Token(TokenTypeEnum.LiteralString, "2000-01-01"),
                new Token(TokenTypeEnum.Or, "||"),
                new Token(TokenTypeEnum.Variable, "anniversary"),
                new Token(TokenTypeEnum.LessThan, "<"),
                new Token(TokenTypeEnum.LiteralString, "2010-12-31")
            };

            // Act
            var actualTokens = _lexer.LexExpression(expression);

            // Assert
            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_WithInvalidDateExpression_ReturnsParsedExpressionStringLiteralForInvalidDate()
        {
            // Arrange
            var expression = "(:birthDate == '2000-13-01't)";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _lexer.LexExpression(expression));
        }

        [Test]
        public void Tokenize_IsNull_Expression()
        {
            // Arrange
            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.Variable, "x"),
                new Token(TokenTypeEnum.Equal, "is"),
                new Token(TokenTypeEnum.LiteralNull, "null")
            };

            // Act
            var actualTokens = _lexer.LexExpression(":x is null");

            // Assert
            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_IsNotNull_Expression()
        {
            // Arrange
            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.Variable, "x"),
                new Token(TokenTypeEnum.NotEqual, "is not"),
                new Token(TokenTypeEnum.LiteralNull, "null")
            };

            // Act
            var actualTokens = _lexer.LexExpression(":x is not null");

            // Assert
            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_AndOrExpressions_ShouldReturnTokenizedString()
        {
            // Arrange
            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "x"),
                new Token(TokenTypeEnum.GreaterThan, ">"),
                new Token(TokenTypeEnum.LiteralInteger, "10"),
                new Token(TokenTypeEnum.RightParenthesis, ")"),
                new Token(TokenTypeEnum.And, "and"),
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "x"),
                new Token(TokenTypeEnum.LessThan, "<"),
                new Token(TokenTypeEnum.LiteralInteger, "20"),
                new Token(TokenTypeEnum.RightParenthesis, ")"),
                new Token(TokenTypeEnum.Or, "or"),
                new Token(TokenTypeEnum.LeftParenthesis, "("),
                new Token(TokenTypeEnum.Variable, "x"),
                new Token(TokenTypeEnum.Equal, "=="),
                new Token(TokenTypeEnum.LiteralInteger, "5"),
                new Token(TokenTypeEnum.RightParenthesis, ")"),
            };

            // Act
            var actualTokens = _lexer.LexExpression("(:x > 10i) and (:x < 20i) or (:x == 5i)");

            // Assert
            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_Like_Expression()
        {
            // Arrange
            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.Variable, "x"),
                new Token(TokenTypeEnum.Like, "like"),
                new Token(TokenTypeEnum.LiteralStringStartsWith, "testing")
            };

            // Act
            var actualTokens = _lexer.LexExpression(":x like %'testing'");

            // Assert
            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }

        [Test]
        public void Tokenize_NotLike_Expression()
        {
            // Arrange
            var expectedTokens = new List<Token>
            {
                new Token(TokenTypeEnum.Variable, "x"),
                new Token(TokenTypeEnum.NotLike, "not like"),
                new Token(TokenTypeEnum.LiteralStringContains, "testing")
            };

            // Act
            var actualTokens = _lexer.LexExpression(":x not like %'testing'%");

            // Assert
            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count));

            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Assert.That(actualTokens[i].Type, Is.EqualTo(expectedTokens[i].Type));
                Assert.That(actualTokens[i].Value, Is.EqualTo(expectedTokens[i].Value));
            }
        }
    }
}