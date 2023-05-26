using System.Globalization;
using System.Text;
using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Validators;

namespace OchoaLopes.ExprEngine.Helpers
{
    internal static class TokenizerHelper
    {
        public static List<string> SplitString(string expression)
        {
            var result = new List<string>();
            var currentToken = new StringBuilder();
            bool inString = false;

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '\'')
                {
                    inString = !inString;

                    if (!inString)
                    {
                        currentToken.Append(expression[i]);

                        if (i + 1 < expression.Length && (expression[i + 1] == 't'))
                        {
                            currentToken.Append(expression[i + 1]);
                            i++;
                        }

                        result.Add(currentToken.ToString());
                        currentToken.Clear();
                        continue;
                    }
                }

                if (!inString && expression[i] == ' ')
                {
                    if (currentToken.Length > 0)
                    {
                        result.Add(currentToken.ToString());
                        currentToken.Clear();
                    }
                }
                else
                {
                    currentToken.Append(expression[i]);
                }
            }

            if (currentToken.Length > 0)
            {
                result.Add(currentToken.ToString());
            }

            return result;
        }
    }
}