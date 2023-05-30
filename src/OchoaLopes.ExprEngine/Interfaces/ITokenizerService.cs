using System.Globalization;

namespace OchoaLopes.ExprEngine.Interfaces
{
    public interface ITokenizerService
	{
		public IList<string> TokenizeExpression(string expression, CultureInfo? cultureInfo = null);
	}
}