using OchoaLopes.ExprEngine.Exceptions;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Literals;
using OchoaLopes.ExprEngine.Operations;

namespace OchoaLopes.ExprEngine.Services
{
    internal class EvaluatorService : IEvaluatorService
	{
        public object EvaluateExpression(IExpression expression, IDictionary<string, object> variables)
        {
            return EvaluateNode(expression, variables);
        }

        #region Private Methods
        private object EvaluateNode(IExpression node, IDictionary<string, object> variables)
        {
            if (node is BinaryOperation binaryOperation)
            {
                return binaryOperation.Evaluate(variables);
            }

            if (node is Literal literalExpression)
            {
                return literalExpression.Value;
            }

            if (node is Variable variableExpression)
            {
                if (variables.ContainsKey(variableExpression.Name))
                {
                    return variables[variableExpression.Name];
                }

                throw new KeyNotFoundException($"Variable '{variableExpression.Name}' not found.");
            }

            throw new ExpressionEvaluationException("Invalid expression node type.");
        }
        #endregion
    }
}