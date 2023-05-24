namespace OchoaLopes.ExprEngine.Interfaces
{
    public interface IExpressionService
	{
        bool ValidateExpression(string expression);

        bool ValidateExpression(string expression, IDictionary<string, object> variables);

        bool ValidateExpression(string expression, IList<object> values);

        bool EvaluateExpression(string expression);

        bool EvaluateExpression(string expression, IDictionary<string, object> variables);

        bool EvaluateExpression(string expression, IList<object> values);

        object ComputeExpression(string expression);

        object ComputeExpression(string expression, IDictionary<string, object> variables);

        object ComputeExpression(string expression, IList<object> values);
    }
}