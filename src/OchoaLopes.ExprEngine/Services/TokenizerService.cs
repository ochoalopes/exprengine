using OchoaLopes.ExprEngine.Builders;
using OchoaLopes.ExprEngine.Helpers;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Validators;
using System.Globalization;
using System.Text.RegularExpressions;

namespace OchoaLopes.ExprEngine.Services
{
    public class TokenizerService : ITokenizerService
	{
        private CultureInfo _cultureInfo;
        private Regex _regex;

        public TokenizerService(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo;
            _regex = RegexBuilder.BuildRegex(cultureInfo);
        }

        public IList<string> TokenizeExpression(string expression, CultureInfo? cultureInfo = null)
        {
            var localRegex = cultureInfo != null ? RegexBuilder.BuildRegex(cultureInfo) : _regex;

            var tokens = new List<string>();

            var splitExpression = TokenizerHelper.SplitString(expression);

            if (splitExpression == null)
            {
                return tokens;
            }

            foreach(var token in splitExpression.Where(s => !string.IsNullOrEmpty(s)))
            {
                if (TokenValidator.IsDateType(token))
                {
                    tokens.Add(token.Trim());
                    continue;
                }

                if (TokenValidator.IsStringOperation(token))
                {
                    tokens.Add(token.Trim());
                    continue;
                }

                var matches = localRegex.Matches(token);
                if (matches != null)
                {
                    tokens.AddRange(matches.Select(m => m.Value).ToList());
                }
            }

            return tokens;
        }
    }
}