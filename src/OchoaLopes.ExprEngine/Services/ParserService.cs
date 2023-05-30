using System.Globalization;
using OchoaLopes.ExprEngine.Builders;
using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.ValueObjects;

namespace OchoaLopes.ExprEngine.Services
{
    internal class ParserService : IParserService
    {
        private IList<Token>? _tokens;
        private int _currentIndex;
        private CultureInfo _cultureInfo;

        public ParserService(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo;
        }

        public IExpression Parse(IList<Token>? tokens, CultureInfo? cultureInfo = null)
        {
            _tokens = tokens;
            _currentIndex = 0;

            return ParseExpression(cultureInfo ?? _cultureInfo);
        }

        #region Private Methods
        private IExpression ParseExpression(CultureInfo cultureInfo)
        {
            return ParseOrExpression(cultureInfo);
        }

        private IExpression ParseOrExpression(CultureInfo cultureInfo)
        {
            var left = ParseAndExpression(cultureInfo);

            while (MatchToken(TokenTypeEnum.Or))
            {
                var op = ConsumeToken();
                var right = ParseAndExpression(cultureInfo);
                left = ExpressionBuilder.BuildExpression(op.Type, left, right);
            }

            return left;
        }

        private IExpression ParseAndExpression(CultureInfo cultureInfo)
        {
            var left = ParseEqualityExpression(cultureInfo);

            while (MatchToken(TokenTypeEnum.And))
            {
                var op = ConsumeToken();
                var right = ParseEqualityExpression(cultureInfo);
                left = ExpressionBuilder.BuildExpression(op.Type, left, right);
            }

            return left;
        }

        private IExpression ParseEqualityExpression(CultureInfo cultureInfo)
        {
            var left = ParseRelationalExpression(cultureInfo);

            while (MatchToken(TokenTypeEnum.Equal, TokenTypeEnum.NotEqual))
            {
                var op = ConsumeToken();
                var right = ParseRelationalExpression(cultureInfo);
                left = ExpressionBuilder.BuildExpression(op.Type, left, right);
            }

            return left;
        }

        private IExpression ParseRelationalExpression(CultureInfo cultureInfo)
        {
            var left = ParseAdditiveExpression(cultureInfo);

            while (MatchToken(TokenTypeEnum.GreaterThan, TokenTypeEnum.GreaterThanOrEqual, TokenTypeEnum.LessThan, TokenTypeEnum.LessThanOrEqual))
            {
                var op = ConsumeToken();
                var right = ParseAdditiveExpression(cultureInfo);
                left = ExpressionBuilder.BuildExpression(op.Type, left, right);
            }

            return left;
        }

        private IExpression ParseAdditiveExpression(CultureInfo cultureInfo)
        {
            var left = ParseMultiplicativeExpression(cultureInfo);

            while (MatchToken(TokenTypeEnum.Add, TokenTypeEnum.BinaryMinus))
            {
                var op = ConsumeToken();
                var right = ParseMultiplicativeExpression(cultureInfo);
                left = ExpressionBuilder.BuildExpression(op.Type, left, right);
            }

            return left;
        }

        private IExpression ParseMultiplicativeExpression(CultureInfo cultureInfo)
        {
            var left = ParseStringEqualityExpression(cultureInfo);

            while (MatchToken(TokenTypeEnum.Multiply, TokenTypeEnum.Divide, TokenTypeEnum.Modulo))
            {
                var op = ConsumeToken();
                var right = ParseStringEqualityExpression(cultureInfo);
                left = ExpressionBuilder.BuildExpression(op.Type, left, right);
            }

            return left;
        }

        private IExpression ParseStringEqualityExpression(CultureInfo cultureInfo)
        {
            var left = ParsePrimaryExpression(cultureInfo);

            while (MatchToken(TokenTypeEnum.Like))
            {
                var op = ConsumeToken();
                var right = ParsePrimaryExpression(cultureInfo);
                left = ExpressionBuilder.BuildExpression(op.Type, left, right);
            }

            return left;
        }

        private IExpression ParsePrimaryExpression(CultureInfo cultureInfo)
        {

            if (MatchToken(TokenTypeEnum.LeftParenthesis))
            {
                ConsumeToken(); // Consume the '(' token

                var expression = ParseExpression(cultureInfo);

                if (!MatchToken(TokenTypeEnum.RightParenthesis))
                {
                    throw new InvalidOperationException("Expected ')' after expression.");
                }

                ConsumeToken(); // Consume the ')' token

                return expression;
            }

            if (MatchToken(TokenTypeEnum.LiteralInteger, TokenTypeEnum.LiteralDouble, TokenTypeEnum.LiteralDecimal,
                TokenTypeEnum.LiteralFloat, TokenTypeEnum.LiteralString, TokenTypeEnum.LiteralStringStartsWith,
                TokenTypeEnum.LiteralStringEndsWith, TokenTypeEnum.LiteralStringContains, TokenTypeEnum.LiteralChar,
                TokenTypeEnum.LiteralBoolean, TokenTypeEnum.LiteralNull, TokenTypeEnum.LiteralDateTime, TokenTypeEnum.Variable))
            {
                var token = ConsumeToken();

                return ExpressionBuilder.BuildLiteral(token.Type, token.Value ?? string.Empty, cultureInfo);
            }

            if (MatchToken(TokenTypeEnum.UnaryMinus))
            {
                ConsumeToken(); // Consume the '-' token
                var expression = ParsePrimaryExpression(cultureInfo);
                return new UnaryMinus(expression);
            }

            throw new InvalidOperationException("Unexpected token.");
        }

        private bool MatchToken(params TokenTypeEnum[] types)
        {
            if (_tokens == null)
            {
                throw new InvalidOperationException("No tokens to consume.");
            }

            if (_currentIndex >= _tokens.Count)
            {
                return false;
            }

            var currentToken = _tokens[_currentIndex];

            return Array.Exists(types, t => t == currentToken.Type);
        }

        private Token ConsumeToken()
        {
            if (_tokens == null)
            {
                throw new InvalidOperationException("No tokens to be consume.");
            }

            if (_currentIndex >= _tokens.Count)
            {
                throw new InvalidOperationException("No more tokens to consume.");
            }
                
            return _tokens[_currentIndex++];
        }
        #endregion
    }
}