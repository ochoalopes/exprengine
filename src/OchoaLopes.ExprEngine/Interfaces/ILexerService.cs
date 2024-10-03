using OchoaLopes.ExprEngine.ValueObjects;
using System.Globalization;

namespace OchoaLopes.ExprEngine.Interfaces
{
    internal interface ILexerService
	{
        public List<Token> LexExpression(string expression, CultureInfo? cultureInfo = null);
    }
}