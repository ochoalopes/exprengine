using OchoaLopes.ExprEngine.ValueObjects;

namespace OchoaLopes.ExprEngine.Interfaces
{
    internal interface ILexerService
	{
        public List<Token> Tokenize(string expression);
    }
}