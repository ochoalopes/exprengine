using System.Text;

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

                        if (i + 1 < expression.Length && (expression[i + 1] == 't' || expression[i + 1] == '%'))
                        {
                            currentToken.Append(expression[i + 1]);
                            i++;
                        }

                        result.Add(currentToken.ToString());
                        currentToken.Clear();
                        continue;
                    }
                }

                if (!inString && i + 6 <= expression.Length && expression.Substring(i, 6).ToLower() == "is not")
                {
                    if (currentToken.Length > 0)
                    {
                        result.Add(currentToken.ToString());
                        currentToken.Clear();
                    }

                    result.Add("is not");
                    i += 5;
                    continue;
                }

                if (!inString && i + 8 <= expression.Length && expression.Substring(i, 8).ToLower() == "not like")
                {
                    if (currentToken.Length > 0)
                    {
                        result.Add(currentToken.ToString());
                        currentToken.Clear();
                    }

                    result.Add("not like");
                    i += 7;
                    continue;
                }

                if (!inString && expression[i] == ' ')
                {
                    if (currentToken.Length > 0)
                    {
                        result.Add(currentToken.ToString());
                        currentToken.Clear();
                    }
                    continue;
                }

                currentToken.Append(expression[i]);
            }

            if (currentToken.Length > 0)
            {
                result.Add(currentToken.ToString());
            }

            return result;
        }
    }
}