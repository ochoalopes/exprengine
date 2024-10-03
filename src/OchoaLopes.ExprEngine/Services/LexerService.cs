using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Helpers;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Validators;
using OchoaLopes.ExprEngine.ValueObjects;
using System.Globalization;

namespace OchoaLopes.ExprEngine.Services
{
    internal class LexerService : ILexerService
    {
        private readonly ITokenizerService _tokenizerService;
        private CultureInfo _cultureInfo;

        public LexerService(ITokenizerService tokenizerService, CultureInfo cultureInfo)
        {
            _tokenizerService = tokenizerService;
            _cultureInfo = cultureInfo;
        }

        public List<Token> LexExpression(string expression, CultureInfo? cultureInfo = null)
        {
            var localCultureInfo = cultureInfo ?? _cultureInfo;

            var tokens = new List<Token>();

            var tokenizedExpression = _tokenizerService.TokenizeExpression(expression, localCultureInfo);

            foreach(var token in tokenizedExpression)
            {
                var type = LexerHelper.GetTokenType(token, localCultureInfo);

                if (type == TokenTypeEnum.Subtract)
                {
                    type = TokenValidator.IsUnaryMinus(tokens, tokens.Count) ? TokenTypeEnum.UnaryMinus : TokenTypeEnum.BinaryMinus;
                }

                tokens.Add(new Token(type, LexerHelper.CleanUpTokenValue(type, token)));
            }

            return tokens;
        }
    }
}