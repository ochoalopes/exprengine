using OchoaLopes.ExprEngine.Enums;

namespace OchoaLopes.ExprEngine.ValueObjects
{
    internal class Token
	{
		public Token(TokenTypeEnum type, string value)
		{
			Type = type;
			Value = value;
		}

		public TokenTypeEnum? Type { get; set; }
		public string? Value { get; set; }
	}
}