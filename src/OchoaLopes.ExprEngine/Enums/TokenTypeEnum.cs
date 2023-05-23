namespace OchoaLopes.ExprEngine.Enums
{
    internal enum TokenTypeEnum
    {
        Variable,
        LeftParenthesis,
        RightParenthesis,

        // Literal types
        LiteralInteger,
        LiteralDecimal,
        LiteralDouble,
        LiteralFloat,
        LiteralString,
        LiteralChar,
        LiteralBoolean,
        LiteralNull,
        Literal,

        // Unary operators
        UnaryMinus,
        BinaryMinus,
        Not,

        // Binary operators
        Add,
        Subtract,
        Multiply,
        Divide,
        Modulo,
        And,
        Or,
        Equal,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual
    }

}