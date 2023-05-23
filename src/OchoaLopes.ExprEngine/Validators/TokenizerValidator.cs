using System.Text.RegularExpressions;
using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.ValueObjects;

namespace OchoaLopes.ExprEngine.Validators
{
    internal static class TokenizerValidator
    {
        public static bool IsVariable(string token)
        {
            return token.Trim().StartsWith(":");
        }

        public static bool IsLeftParenthesis(string token)
        {
            return token.Trim() == "(";
        }

        public static bool IsRightParenthesis(string token)
        {
            return token.Trim() == ")";
        }

        public static bool IsEqual(string token)
        {
            return token.Trim() == "==";
        }

        public static bool IsNotEqual(string token)
        {
            return token.Trim() == "!=";
        }

        public static bool IsGreaterThan(string token)
        {
            return token.Trim() == ">";
        }

        public static bool IsGreaterThanOrEqual(string token)
        {
            return token.Trim() == ">=";
        }

        public static bool IsLessThan(string token)
        {
            return token.Trim() == "<";
        }

        public static bool IsLessThanOrEqual(string token)
        {
            return token.Trim() == "<=";
        }

        public static bool IsAnd(string token)
        {
            return token.Trim() == "&&";
        }

        public static bool IsOr(string token)
        {
            return token.Trim() == "||";
        }

        public static bool IsNot(string token)
        {
            return token.Trim() == "!";
        }

        public static bool IsMutiply(string token)
        {
            return token.Trim() == "*";
        }

        public static bool IsDivide(string token)
        {
            return token.Trim() == "/";
        }

        public static bool IsModulo(string token)
        {
            return token.Trim() == "%";
        }

        public static bool IsAdd(string token)
        {
            return token.Trim() == "+";
        }

        public static bool IsSubtract(string token)
        {
            return token.Trim() == "-";
        }

        public static bool IsLiteralInteger(string value)
        {
            return Regex.IsMatch(value.Trim(), @"^-?\d+i$");
        }

        public static bool IsLiteralDouble(string value)
        {
            return Regex.IsMatch(value.Trim(), @"^-?\d+(\.\d+)?D$");
        }

        public static bool IsLiteralDecimal(string value)
        {
            return Regex.IsMatch(value.Trim(), @"^-?\d+(\.\d+)?d$");
        }

        public static bool IsLiteralFloat(string value)
        {
            return Regex.IsMatch(value.Trim(), @"^-?\d+(\.\d+)?f$");
        }

        public static bool IsLiteralString(string value)
        {
            return value.StartsWith("'") && value.EndsWith("'");
        }

        public static bool IsLiteralChar(string value)
        {
            return value.Length == 3 && value.StartsWith("'") && value.EndsWith("'");
        }

        public static bool IsLiteralBoolean(string value)
        {
            return value.Trim().ToLower() == "true" || value.Trim().ToLower() == "false";
        }

        public static bool IsLiteralNull(string value)
        {
            return value.Trim().ToLower() == "null";
        }

        public static bool IsOperator(TokenTypeEnum type)
        {
            var operatorTypes = new TokenTypeEnum[]
            {
                TokenTypeEnum.Add,
                TokenTypeEnum.And,
                TokenTypeEnum.Divide,
                TokenTypeEnum.Equal,
                TokenTypeEnum.GreaterThan,
                TokenTypeEnum.GreaterThanOrEqual,
                TokenTypeEnum.LessThan,
                TokenTypeEnum.LessThanOrEqual,
                TokenTypeEnum.Modulo,
                TokenTypeEnum.Multiply,
                TokenTypeEnum.Not,
                TokenTypeEnum.NotEqual,
                TokenTypeEnum.Or,
                TokenTypeEnum.Subtract,
                TokenTypeEnum.UnaryMinus,
                TokenTypeEnum.BinaryMinus
            };

            return operatorTypes.Contains(type);
        }

        public static bool IsUnaryMinus(List<Token> tokens, int index)
        {
            if (tokens.Count == 0 || index == 0)
            {
                return true;
            }

            var previousToken = tokens[index - 1];

            if (previousToken.Type == null || previousToken.Type == TokenTypeEnum.LeftParenthesis || IsOperator(previousToken.Type.Value))
            {
                return true;
            }

            return false;
        }

        public static bool IsOperand(TokenTypeEnum type)
        {
            var operands = new TokenTypeEnum[]
            {
                TokenTypeEnum.Variable,
                TokenTypeEnum.LiteralInteger,
                TokenTypeEnum.LiteralDecimal,
                TokenTypeEnum.LiteralDouble,
                TokenTypeEnum.LiteralFloat,
                TokenTypeEnum.LiteralString,
                TokenTypeEnum.LiteralChar,
                TokenTypeEnum.LiteralBoolean,
                TokenTypeEnum.Literal
            };

            return operands.Contains(type);
        }

        public static bool IsBinaryMinus(List<Token> tokens, int index)
        {
            if (index == 0)
            {
                return false;
            }

            var previousToken = tokens[index - 1];

            if (previousToken != null && previousToken.Type.HasValue)
            {
                return IsOperand(previousToken.Type.Value) ||
                       previousToken.Type == TokenTypeEnum.RightParenthesis;
            }

            return false;
        }
    }
}