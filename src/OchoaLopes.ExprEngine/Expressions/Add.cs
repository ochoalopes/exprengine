﻿using OchoaLopes.ExprEngine.Helpers;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class Add : BinaryOperation
    {
        public Add(IExpression left, IExpression right) : base(left, right) { }

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var leftResult = Left.Evaluate(variables);
            var rightResult = Right.Evaluate(variables);

            if (leftResult is bool leftBool || rightResult is bool rightBool)
            {
                throw new InvalidOperationException("Both operands of a comparison cannot be a boolean type.");
            }

            return OperationHelper.Operate(leftResult, rightResult, (a, b) => a + b);
        }
    }
}