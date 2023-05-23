using OchoaLopes.ExprEngine.Interfaces;

namespace OchoaLopes.ExprEngine.Operations
{
    internal abstract class BinaryOperation : IExpression
    {
        public IExpression Left { get; }
        public IExpression Right { get; }

        protected BinaryOperation(IExpression left, IExpression right)
        {
            Left = left;
            Right = right;
        }

        public abstract object Evaluate(IDictionary<string, object> variables);
    }
}