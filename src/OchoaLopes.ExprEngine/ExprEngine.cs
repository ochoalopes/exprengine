using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Services;

namespace OchoaLopes.ExprEngine
{
    public class ExprEngine : IExprEngine
	{
        private readonly ILexerService _lexerService;
        private readonly IParserService _parserService;
        private readonly IEvaluatorService _evaluatorService;

        public ExprEngine()
        {
            _lexerService = new LexerService();
            _parserService = new ParserService();
            _evaluatorService = new EvaluatorService();
        }

        public bool ValidateExpression(string expression, IDictionary<string, object>? variables = null)
        {
            try
            {
                var tokens = _lexerService.Tokenize(expression);
                var parsedExpression = _parserService.Parse(tokens);

                if (variables == null)
                {
                    return true;
                }

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

        public bool ValidateExpression(string expression, IList<object>? values = null)
        {
            try
            {
                var tokens = _lexerService.Tokenize(expression);
                var parsedExpression = _parserService.Parse(tokens);

                if (values == null)
                {
                    return true;
                }

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

        public bool EvaluateExpression(string expression, IDictionary<string, object>? variables = null)
        {
            try
            {
                return Convert.ToBoolean(ComputeExpression(expression, variables));
            }
            catch
            {
                return false;
            }
        }

        public bool EvaluateExpression(string expression, IList<object>? values = null)
        {
            try
            {
                return Convert.ToBoolean(ComputeExpression(expression, values));
            }
            catch
            {
                return false;
            }
        }

        public object ComputeExpression(string expression, IDictionary<string, object>? variables = null)
        {
            if (!ValidateExpression(expression, variables))
            {
                throw new InvalidOperationException("Expression is not valid");
            }

            var tokens = _lexerService.Tokenize(expression);
            var parsedExpression = _parserService.Parse(tokens);

            return _evaluatorService.EvaluateExpression(parsedExpression, variables ?? new Dictionary<string, object>());
        }

        public object ComputeExpression(string expression, IList<object>? values = null)
        {
            if (!ValidateExpression(expression, values))
            {
                throw new InvalidOperationException("Expression is not valid");
            }

            var tokens = _lexerService.Tokenize(expression);
            var parsedExpression = _parserService.Parse(tokens);

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
    }
}