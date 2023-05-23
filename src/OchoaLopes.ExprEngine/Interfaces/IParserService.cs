using OchoaLopes.ExprEngine.ValueObjects;

namespace OchoaLopes.ExprEngine.Interfaces
{
    internal interface IParserService
	{
        public IExpression Parse(IList<Token>? tokens);
    }
}