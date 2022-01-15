namespace RecDescent.Tokens
{
    public class Token
    {
        public TokenType TokenType { get; init; }

        public string Lexeme { get; init; }

        public Token(TokenType type, string lexeme)
        {
            TokenType = type;
            Lexeme = lexeme;
        }

        public override string ToString()
        {
            return $"<{TokenType}, {Lexeme}>";
        }
    }
}