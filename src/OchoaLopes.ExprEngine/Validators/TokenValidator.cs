using System.Globalization;
using System.Text.RegularExpressions;
using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.ValueObjects;

namespace OchoaLopes.ExprEngine.Validators
{
    internal static class TokenValidator
    {
        public static bool IsDateType(string token)
        {
            return token.StartsWith("'") && token.EndsWith("'t");
        }

        public static bool IsStringOperation(string token)
        {
            return token.Trim().StartsWith("%") || token.Trim().EndsWith("%"); 
        }

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
            return token.Trim() == "==" || token.Trim() == "is";
        }

        public static bool IsLike(string token)
        {
            return token.Trim() == "like";
        }

        public static bool IsNotLike(string token)
        {
            return token.Trim() == "not like";
        }

        public static bool IsNotEqual(string token)
        {
            return token.Trim() == "!=" || token.Trim() == "is not";
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
            return token.Trim() == "&&" || token.Trim() == "and";
        }

        public static bool IsOr(string token)
        {
            return token.Trim() == "||" || token.Trim() == "or";
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

        public static bool IsLiteralInteger(string token)
        {
            return Regex.IsMatch(token.Trim(), @"^-?\d+i$");
        }

        public static bool IsLiteralDouble(string token, CultureInfo cultureInfo)
        {
            var decimalSeparator = Regex.Escape(cultureInfo.NumberFormat.NumberDecimalSeparator);
            return Regex.IsMatch(token.Trim(), @"^-?\d+(" + decimalSeparator + @"\d+)?D$");
        }

        public static bool IsLiteralDecimal(string token, CultureInfo cultureInfo)
        {
            var decimalSeparator = Regex.Escape(cultureInfo.NumberFormat.NumberDecimalSeparator);
            return Regex.IsMatch(token.Trim(), @"^-?\d+(" + decimalSeparator + @"\d+)?d$");
        }

        public static bool IsLiteralFloat(string token, CultureInfo cultureInfo)
        {
            var decimalSeparator = Regex.Escape(cultureInfo.NumberFormat.NumberDecimalSeparator);
            return Regex.IsMatch(token.Trim(), @"^-?\d+(" + decimalSeparator + @"\d+)?f$");
        }

        public static bool IsLiteralString(string token)
        {
            return token.StartsWith("'") && token.EndsWith("'");
        }

        public static bool IsLiteralChar(string token)
        {
            return token.Length == 3 && token.StartsWith("'") && token.EndsWith("'");
        }

        public static bool IsLiteralBoolean(string token)
        {
            return token.Trim().ToLower() == "true" || token.Trim().ToLower() == "false";
        }

        public static bool IsLiteralNull(string token)
        {
            return token.Trim().ToLower() == "null";
        }

        public static bool IsLiteralDate(string token, CultureInfo cultureInfo)
        {
            if (IsDateType(token))
            {
                var potentialDateString = token.TrimEnd('t').Trim('\'');

                string[] formats = { "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy", "yyyy/MM/dd" };

                if (DateTime.TryParseExact(potentialDateString, formats, cultureInfo, DateTimeStyles.None, out _))
                {
                    return true;
                }
            }

            return false;
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

        internal static bool IsLiteralStringStartsWith(string token)
        {
            return token.Trim().StartsWith("%") && token.Trim().EndsWith("'");
        }

        internal static bool IsLiteralStringEndsWith(string token)
        {
            return token.Trim().StartsWith("'") && token.Trim().EndsWith("%");
        }

        internal static bool IsLiteralStringContains(string token)
        {
            return token.Trim().StartsWith("%") && token.Trim().EndsWith("%");
        }
    }
}