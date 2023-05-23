using OchoaLopes.ExprEngine.Interfaces;

namespace OchoaLopes.ExprEngine.Operations
{
    internal abstract class UnaryOperation : IExpression
    {
        protected IExpression Operand { get; }

        protected UnaryOperation(IExpression operand)
        {
            Operand = operand;
        }

        public abstract object Evaluate(IDictionary<string, object> variables);
    }
}