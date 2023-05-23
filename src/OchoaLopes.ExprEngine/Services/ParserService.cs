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


        public IExpression Parse(IList<Token>? tokens)
        {
            _tokens = tokens;
            _currentIndex = 0;

            return ParseExpression();
        }

        private IExpression ParseExpression()
        {
            return ParseOrExpression();
        }

        private IExpression ParseOrExpression()
        {
            var left = ParseAndExpression();

            while (MatchToken(TokenTypeEnum.Or))
            {
                var op = ConsumeToken();
                var right = ParseAndExpression();
                left = new Or(left, right);
            }

            return left;
        }

        private IExpression ParseAndExpression()
        {
            var left = ParseEqualityExpression();

            while (MatchToken(TokenTypeEnum.And))
            {
                var op = ConsumeToken();
                var right = ParseEqualityExpression();
                left = new And(left, right);
            }

            return left;
        }

        private IExpression ParseEqualityExpression()
        {
            var left = ParseRelationalExpression();

            while (MatchToken(TokenTypeEnum.Equal, TokenTypeEnum.NotEqual))
            {
                var op = ConsumeToken();
                var right = ParseRelationalExpression();

                if (op.Type == TokenTypeEnum.Equal)
                    left = new Equal(left, right);
                else
                    left = new NotEqual(left, right);
            }

            return left;
        }

        private IExpression ParseRelationalExpression()
        {
            var left = ParseAdditiveExpression();

            while (MatchToken(TokenTypeEnum.GreaterThan, TokenTypeEnum.GreaterThanOrEqual, TokenTypeEnum.LessThan, TokenTypeEnum.LessThanOrEqual))
            {
                var op = ConsumeToken();
                var right = ParseAdditiveExpression();

                switch (op.Type)
                {
                    case TokenTypeEnum.GreaterThan:
                        left = new GreaterThan(left, right);
                        break;
                    case TokenTypeEnum.GreaterThanOrEqual:
                        left = new GreaterThanOrEqual(left, right);
                        break;
                    case TokenTypeEnum.LessThan:
                        left = new LessThan(left, right);
                        break;
                    case TokenTypeEnum.LessThanOrEqual:
                        left = new LessThanOrEqual(left, right);
                        break;
                }
            }

            return left;
        }

        private IExpression ParseAdditiveExpression()
        {
            var left = ParseMultiplicativeExpression();

            while (MatchToken(TokenTypeEnum.Add, TokenTypeEnum.BinaryMinus))
            {
                var op = ConsumeToken();
                var right = ParseMultiplicativeExpression();

                if (op.Type == TokenTypeEnum.Add)
                {
                    left = new Add(left, right);
                }
                else if (op.Type == TokenTypeEnum.BinaryMinus)
                {
                    left = new Subtract(left, right);
                }
            }

            return left;
        }

        private IExpression ParseMultiplicativeExpression()
        {
            var left = ParsePrimaryExpression();

            while (MatchToken(TokenTypeEnum.Multiply, TokenTypeEnum.Divide, TokenTypeEnum.Modulo))
            {
                var op = ConsumeToken();
                var right = ParsePrimaryExpression();

                switch (op.Type)
                {
                    case TokenTypeEnum.Multiply:
                        left = new Multiply(left, right);
                        break;
                    case TokenTypeEnum.Divide:
                        left = new Divide(left, right);
                        break;
                    case TokenTypeEnum.Modulo:
                        left = new Modulo(left, right);
                        break;
                }
            }

            return left;
        }

        private IExpression ParsePrimaryExpression()
        {
            if (MatchToken(TokenTypeEnum.LeftParenthesis))
            {
                ConsumeToken(); // Consume the '(' token
                var expression = ParseExpression();

                if (!MatchToken(TokenTypeEnum.RightParenthesis))
                {
                    throw new InvalidOperationException("Expected ')' after expression.");
                }

                ConsumeToken(); // Consume the ')' token

                return expression;
            }
            else if (MatchToken(
                TokenTypeEnum.LiteralInteger,
                TokenTypeEnum.LiteralDouble,
                TokenTypeEnum.LiteralDecimal,
                TokenTypeEnum.LiteralFloat,
                TokenTypeEnum.LiteralString,
                TokenTypeEnum.LiteralChar,
                TokenTypeEnum.LiteralBoolean,
                TokenTypeEnum.LiteralNull,
                TokenTypeEnum.Variable))
            {
                var token = ConsumeToken();

                if (token.Value == null)
                {
                    throw new InvalidOperationException("Token value is invalid");
                }

                switch (token.Type)
                {
                    case TokenTypeEnum.LiteralInteger:
                        return new Literal(int.Parse(token.Value));
                    case TokenTypeEnum.LiteralDouble:
                        return new Literal(double.Parse(token.Value));
                    case TokenTypeEnum.LiteralDecimal:
                        return new Literal(decimal.Parse(token.Value));
                    case TokenTypeEnum.LiteralFloat:
                        return new Literal(float.Parse(token.Value));
                    case TokenTypeEnum.LiteralString:
                        return new Literal(token.Value.ToString());
                    case TokenTypeEnum.LiteralChar:
                        return new Literal(char.Parse(token.Value));
                    case TokenTypeEnum.LiteralBoolean:
                        return new Literal(bool.Parse(token.Value));
                    case TokenTypeEnum.LiteralNull:
                        return new Literal(null);
                    case TokenTypeEnum.Variable:
                        return new Variable(token.Value);
                    default:
                        throw new InvalidOperationException("Unexpected token.");
                }
            }
            else if (MatchToken(TokenTypeEnum.UnaryMinus))
            {
                ConsumeToken(); // Consume the '-' token
                var expression = ParsePrimaryExpression();
                return new UnaryMinus(expression);
            }
            else
            {
                throw new InvalidOperationException("Unexpected token.");
            }
        }



        private bool MatchToken(params TokenTypeEnum[] types)
        {
            if (_tokens == null)
            {
                throw new InvalidOperationException("No tokens to consume.");
            }

            if (_currentIndex >= _tokens.Count)
                return false;

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
                throw new InvalidOperationException("No more tokens to consume.");

            return _tokens[_currentIndex++];
        }
    }
}