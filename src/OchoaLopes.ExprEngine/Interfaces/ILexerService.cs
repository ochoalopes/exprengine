using System.Globalization;
using OchoaLopes.ExprEngine.ValueObjects;

namespace OchoaLopes.ExprEngine.Interfaces
{
    internal interface ILexerService
	{
        public List<Token> LexExpression(string expression, CultureInfo? cultureInfo = null);
    }
}