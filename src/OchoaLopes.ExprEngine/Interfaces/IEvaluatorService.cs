namespace OchoaLopes.ExprEngine.Interfaces
{
    internal interface IEvaluatorService
	{
        public object EvaluateExpression(IExpression expression, IDictionary<string, object> variables);
    }
}