using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Services;
using OchoaLopes.ExprEngine.ValueObjects;
using System.Globalization;

namespace OchoaLopes.ExprEngine
{
    public class ExpressionService : IExpressionService
	{
        private readonly ILexerService _lexerService;
        private readonly IParserService _parserService;
        private readonly IEvaluatorService _evaluatorService;

        public ExpressionService()
        {
            _lexerService = new LexerService(new TokenizerService(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
            _parserService = new ParserService(CultureInfo.CurrentCulture);
            _evaluatorService = new EvaluatorService();
        }

        public ExpressionService(CultureInfo cultureInfo)
        {
            _lexerService = new LexerService(new TokenizerService(cultureInfo), cultureInfo);
            _parserService = new ParserService(cultureInfo);
            _evaluatorService = new EvaluatorService();
        }

        public bool ValidateExpression(string expression, CultureInfo? cultureInfo = null)
        {
            try
            {
                ParseExpression(expression, cultureInfo);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateExpression(string expression, IDictionary<string, object> variables, CultureInfo? cultureInfo = null)
        {
            try
            {
                var (tokens, parsedExpression) = ParseExpression(expression, cultureInfo);

                foreach (var token in tokens)
                {
                    if (token.Value == null)
                    {
                        return false;
                    }

                    if (token.Type == TokenTypeEnum.Variable && !variables.ContainsKey(token.Value))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateExpression(string expression, IList<object> values, CultureInfo? cultureInfo = null)
        {
            try
            {
                var (tokens, parsedExpression) = ParseExpression(expression, cultureInfo);

                var variablesCount = tokens.Count(v => v.Type == TokenTypeEnum.Variable);

                if (variablesCount != values.Count)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EvaluateExpression(string expression, CultureInfo? cultureInfo = null)
        {
            try
            {
                return Convert.ToBoolean(ComputeExpression(expression, new Dictionary<string, object>(), cultureInfo));
            }
            catch
            {
                return false;
            }
        }

        public bool EvaluateExpression(string expression, IDictionary<string, object> variables, CultureInfo? cultureInfo = null)
        {
            try
            {
                return Convert.ToBoolean(ComputeExpression(expression, variables, cultureInfo));
            }
            catch
            {
                return false;
            }
        }

        public bool EvaluateExpression(string expression, IList<object> values, CultureInfo? cultureInfo = null)
        {
            try
            {
                return Convert.ToBoolean(ComputeExpression(expression, values, cultureInfo));
            }
            catch
            {
                return false;
            }
        }

        public object ComputeExpression(string expression, CultureInfo? cultureInfo = null)
        {
            var (tokens, parsedExpression) = ParseExpression(expression, cultureInfo);

            if (parsedExpression == null)
            {
                throw new InvalidOperationException("Expression is not valid.");
            }

            return _evaluatorService.EvaluateExpression(parsedExpression, new Dictionary<string, object>());
        }

        public object ComputeExpression(string expression, IDictionary<string, object> variables, CultureInfo? cultureInfo = null)
        {
            var (tokens, parsedExpression) = ParseExpression(expression, cultureInfo);

            if (parsedExpression == null)
            {
                throw new InvalidOperationException("Expression is not valid.");
            }

            return _evaluatorService.EvaluateExpression(parsedExpression, variables ?? new Dictionary<string, object>());
        }

        public object ComputeExpression(string expression, IList<object> values, CultureInfo? cultureInfo = null)
        {
            var (tokens, parsedExpression) = ParseExpression(expression, cultureInfo);

            if (parsedExpression == null)
            {
                throw new InvalidOperationException("Expression is not valid.");
            }

            var variables = new Dictionary<string, object>();
            var i = 0;
            values = values ?? new List<object>();

            foreach (var token in tokens)
            {
                if (token.Value == null)
                {
                    return false;
                }

                if (token.Type == TokenTypeEnum.Variable && !variables.ContainsKey(token.Value))
                {
                    variables.Add(token.Value, values[i]);
                    i++;
                }
            }

            return _evaluatorService.EvaluateExpression(parsedExpression, variables);
        }

        #region Private Methods
        private (List<Token>, IExpression?) ParseExpression(string expression, CultureInfo? cultureInfo = null)
        {
            var tokens = _lexerService.LexExpression(expression, cultureInfo);
            var parsedExpression = _parserService.Parse(tokens, cultureInfo);

            return (tokens, parsedExpression);
        }
        #endregion
    }
}