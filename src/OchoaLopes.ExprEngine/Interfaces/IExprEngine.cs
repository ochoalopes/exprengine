namespace OchoaLopes.ExprEngine.Interfaces
{
    public interface IExprEngine
	{
        bool ValidateExpression(string expression, IDictionary<string, object>? variables = null);

        bool ValidateExpression(string expression, IList<object>? values = null);

        bool EvaluateExpression(string expression, IDictionary<string, object>? variables = null);

        bool EvaluateExpression(string expression, IList<object>? values = null);

        object ComputeExpression(string expression, IDictionary<string, object>? variables = null);

        object ComputeExpression(string expression, IList<object>? values = null);
    }
}