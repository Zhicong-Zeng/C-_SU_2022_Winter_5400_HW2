using System;
using RecDescent.Tokens;

namespace RecDescent.Exceptions
{
    public class ParseException : Exception
    {
        public ParseException(TokenType expected, TokenType actual) : base($"Expected token type {expected}, got {actual}") { }

        public ParseException(string msg) : base(msg) { }
    }
}