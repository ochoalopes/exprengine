namespace OchoaLopes.ExprEngine.Interfaces
{
    internal interface IExpression
    {
        object Evaluate(IDictionary<string, object> variables);
    }
}