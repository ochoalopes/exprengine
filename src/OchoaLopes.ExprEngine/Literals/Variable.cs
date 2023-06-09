﻿using OchoaLopes.ExprEngine.Interfaces;

namespace OchoaLopes.ExprEngine.Literals
{
    internal class Variable : IExpression
    {
        public string Name { get; }

        public Variable(string name)
        {
            Name = name;
        }

        public object Evaluate(IDictionary<string, object> variables)
        {
            return variables[Name];
        }
    }
}