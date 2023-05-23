namespace OchoaLopes.ExprEngine.Exceptions
{
    internal class ExpressionEvaluationException : Exception
    {
        public ExpressionEvaluationException() { }

        public ExpressionEvaluationException(string message)
            : base(message) { }

        public ExpressionEvaluationException(string message, Exception inner)
            : base(message, inner) { }
    }
}