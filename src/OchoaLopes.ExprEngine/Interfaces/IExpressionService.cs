using System.Globalization;

namespace OchoaLopes.ExprEngine.Interfaces
{
    public interface IExpressionService
	{
        bool ValidateExpression(string expression, CultureInfo? cultureInfo = null);

        bool ValidateExpression(string expression, IDictionary<string, object> variables, CultureInfo? cultureInfo = null);

        bool ValidateExpression(string expression, IList<object> values, CultureInfo? cultureInfo = null);

        bool EvaluateExpression(string expression, CultureInfo? cultureInfo = null);

        bool EvaluateExpression(string expression, IDictionary<string, object> variables, CultureInfo? cultureInfo = null);

        bool EvaluateExpression(string expression, IList<object> values, CultureInfo? cultureInfo = null);

        object ComputeExpression(string expression, CultureInfo? cultureInfo = null);

        object ComputeExpression(string expression, IDictionary<string, object> variables, CultureInfo? cultureInfo = null);

        object ComputeExpression(string expression, IList<object> values, CultureInfo? cultureInfo = null);
    }
}