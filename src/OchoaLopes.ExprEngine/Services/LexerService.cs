using System.Text.RegularExpressions;
using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Helpers;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Validators;
using OchoaLopes.ExprEngine.ValueObjects;

namespace OchoaLopes.ExprEngine.Services
{
    internal class LexerService : ILexerService
    {
        private static readonly Regex TokenRegex = new Regex(@"(:\w+|==|!=|>=|<=|<|>|&&|\|\||!|\+|-|\*|/|%|'[^']*'|true|false|\d+\.?\d*[idfD]?|\d*\.\d+[idfD]?|null|is\s+not\s+null|is\s+null|\(|\))", RegexOptions.IgnoreCase);

        public List<Token> Tokenize(string expression)
        {
            var matches = TokenRegex.Matches(expression);
            var tokens = new List<Token>();

            foreach (Match match in matches)
            {
                var value = match.Value;
                var type = TokenizerHelper.GetTokenType(value);

                if (type == TokenTypeEnum.Subtract)
                {
                    type = TokenizerValidator.IsUnaryMinus(tokens, tokens.Count) ? TokenTypeEnum.UnaryMinus : TokenTypeEnum.BinaryMinus;
                }

                tokens.Add(new Token(type, TokenizerHelper.CleanUpTokenValue(type, value)));
            }

            return tokens;
        }
    }
}