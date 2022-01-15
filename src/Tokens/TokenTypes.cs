namespace RecDescent.Tokens
{
    public enum TokenType
    {
        Unknown,
        Number,
        Identifier,
        AddOperator,
        SubOperator,
        MulOperator,
        DivOperator,
        ModOperator,
        LeftParen,
        RightParen,
        EndOfInput,

        // These are only for TokenTreeNodes
        Goal,
        Expr,
        ExprPrime,
        Term,
        TermPrime,
        Factor
    }
}
