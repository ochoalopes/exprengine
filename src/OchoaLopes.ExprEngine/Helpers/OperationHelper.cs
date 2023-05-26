using System.Runtime.Intrinsics.X86;
using OchoaLopes.ExprEngine.Delegates;

namespace OchoaLopes.ExprEngine.Helpers
{
    internal static class OperationHelper
    {
        public static object OperateNumbers(object left, object right, NumericOperation operation)
        {
            if (left is int leftInt)
            {
                if (right is int rightInt)
                    return operation(leftInt, rightInt);
                else if (right is double rightDouble)
                    return operation(leftInt, rightDouble);
                else if (right is decimal rightDecimal)
                    return operation(leftInt, (double)rightDecimal);
                else if (right is float rightFloat)
                    return operation(leftInt, (float)rightFloat);
            }
            else if (left is double leftDouble)
            {
                if (right is int rightInt)
                    return operation(leftDouble, rightInt);
                else if (right is double rightDouble)
                    return operation(leftDouble, rightDouble);
                else if (right is decimal rightDecimal)
                    return operation(leftDouble, (double)rightDecimal);
                else if (right is float rightFloat)
                    return operation(leftDouble, (float)rightFloat);
            }
            else if (left is decimal leftDecimal)
            {
                if (right is int rightInt)
                    return operation((double)leftDecimal, rightInt);
                else if (right is double rightDouble)
                    return operation((double)leftDecimal, rightDouble);
                else if (right is decimal rightDecimal)
                    return operation((double)leftDecimal, (double)rightDecimal);
                else if (right is float rightFloat)
                    return operation(leftDecimal, (float)rightFloat);
            }
            else if (left is float leftFloat)
            {
                if (right is int rightInt)
                    return operation((double)leftFloat, rightInt);
                else if (right is double rightDouble)
                    return operation((double)leftFloat, rightDouble);
                else if (right is decimal rightDecimal)
                    return operation((double)leftFloat, (double)rightDecimal);
                else if (right is float rightFloat)
                    return operation(leftFloat, rightFloat);
            }

            throw new InvalidOperationException("Both operands of a numeric operation must be of type int, double or decimal.");
        }

        public static object OperateAddString(object left, object right)
        {
            if (left is string leftString && right is string rightString)
            {
                return leftString + rightString;
            }

            throw new InvalidOperationException("Both operands of a string operation must be of type string.");
        }

        public static object OperateAddDate(object left, object right)
        {
            if (left is DateTime leftDate && right is int rightInt)
            {
                return leftDate.AddDays(rightInt);
            }

            throw new InvalidOperationException("Add operation with dates can be only with integers.");
        }

        public static object OperateSubtractDate(object left, object right)
        {
            if (left is DateTime leftDate && right is int rightInt)
            {
                return leftDate.AddDays(-rightInt);
            }

            throw new InvalidOperationException("Subtract operation with dates can be only with integers.");
        }


        public static int Compare(object left, object right)
        {
            if (left is IComparable leftComparable && right is IComparable rightComparable)
            {
                if (left is int || left is double || left is float)
                {
                    left = Convert.ToDouble(left);
                    right = Convert.ToDouble(right);
                }
                else if (left is decimal)
                {
                    left = Convert.ToDecimal(left);
                    right = Convert.ToDecimal(right);
                }

                return ((IComparable)left).CompareTo(right);
            }

            throw new InvalidOperationException("Both operands of a comparison operation must be IComparable.");
        }
    }
}