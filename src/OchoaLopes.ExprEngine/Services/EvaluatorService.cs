using OchoaLopes.ExprEngine.Exceptions;
using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;

namespace OchoaLopes.ExprEngine.Services
{
    internal class EvaluatorService : IEvaluatorService
	{
        public object EvaluateExpression(IExpression expression, IDictionary<string, object> variables)
        {
            return EvaluateNode(expression, variables);
        }

        private object EvaluateNode(IExpression node, IDictionary<string, object> variables)
        {
            if (node is BinaryOperation binaryOperation)
            {
                return binaryOperation.Evaluate(variables);
            }
            else if (node is Literal literalExpression)
            {
                return literalExpression.Value;
            }
            else if (node is Variable variableExpression)
            {
                if (variables.ContainsKey(variableExpression.Name))
                {
                    return variables[variableExpression.Name];
                }
                else
                {
                    throw new KeyNotFoundException($"Variable '{variableExpression.Name}' not found.");
                }
            }

            throw new ExpressionEvaluationException("Invalid expression node type.");
        }
    }
}