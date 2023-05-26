using OchoaLopes.ExprEngine.Helpers;
using OchoaLopes.ExprEngine.Interfaces;

namespace OchoaLopes.ExprEngine.Validators
{
    public static class ExpressionValidator
	{
		public static void ValidateAdd(object? left, object? right)
		{
            if (left is bool || right is bool)
            {
                throw new InvalidOperationException("Both operands of an add cannot be a boolean type.");
            }

            if (left is string && right is not string)
            {
                throw new InvalidOperationException("Both operands of a add must be a string.");
            }

            if (left is DateTime && right is not int)
            {
                throw new InvalidOperationException("Add operation with dates can be only with integers.");
            }
        }

        public static void ValidateAnd(object? left, object? right)
        {
            if (left is not bool || right is not bool)
            {
                throw new InvalidOperationException("Both operands of an And operation must be booleans.");
            }
        }

        public static void ValidateGreaterThan(object? left, object? right)
        {
            if (left is bool || right is bool)
            {
                throw new InvalidOperationException("Both operands of a comparison cannot be a boolean type.");
            }
        }

        public static void ValidateGreaterThanOrEqual(object? left, object? right)
        {
            if (left is bool || right is bool)
            {
                throw new InvalidOperationException("Both operands of a comparison cannot be a boolean type.");
            }
        }

        public static void ValidateLessThan(object? left, object? right)
        {
            if (left is bool || right is bool)
            {
                throw new InvalidOperationException("Both operands of a comparison cannot be a boolean type.");
            }
        }

        public static void ValidateLessThanOrEqual(object? left, object? right)
        {
            if (left is bool || right is bool)
            {
                throw new InvalidOperationException("Both operands of a comparison cannot be a boolean type.");
            }
        }

        public static void ValidateModulo(object? left, object? right)
        {
            if (left is string || right is string)
            {
                throw new InvalidOperationException("Both values cannot be a string.");
            }

            if (left is bool || right is bool)
            {
                throw new InvalidOperationException("Both operands of a comparison cannot be a boolean type.");
            }

            if (right is double rightDouble && Math.Abs(rightDouble) < double.Epsilon)
            {
                throw new DivideByZeroException("Division by zero is not allowed.");
            }

            if (left is DateTime || right is DateTime)
            {
                throw new InvalidOperationException("Both values cannot be a date.");
            }
        }

        public static void ValidateMultiply(object? left, object? right)
        {
            if (left is string || right is string)
            {
                throw new InvalidOperationException("Both values cannot be a string.");
            }

            if (left is bool || right is bool)
            {
                throw new InvalidOperationException("Both operands of a comparison cannot be a boolean type.");
            }

            if (left is DateTime || right is DateTime)
            {
                throw new InvalidOperationException("Both values cannot be a date.");
            }
        }

        public static void ValidateNot(object? value)
        {
            if (value is not bool)
            {
                throw new InvalidOperationException("The operand of a Not operation must be a boolean.");
            }
        }

        public static void ValidateOr(object? left, object? right)
        {
            if (left is not bool || right is not bool)
            {
                throw new InvalidOperationException("Both operands of an Or operation must be booleans.");
            }
        }

        public static void ValidateSubtract(object? left, object? right)
        {
            if (left is string || right is string)
            {
                throw new InvalidOperationException("Both values cannot be a string.");
            }

            if (left is bool || right is bool)
            {
                throw new InvalidOperationException("Both operands of a comparison cannot be a boolean type.");
            }

            if (left is DateTime && right is not int)
            {
                throw new InvalidOperationException("Subtract operation with dates can be only with integers.");
            }
        }
    }
}