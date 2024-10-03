using OchoaLopes.ExprEngine.ValueObjects;
using System.Globalization;

namespace OchoaLopes.ExprEngine.Interfaces
{
    internal interface IParserService
	{
        public IExpression Parse(IList<Token>? tokens, CultureInfo? cultureInfo = null);
    }
}