using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RecDescent.Tokens
{
    public class TokenStream : IEnumerable<Token>
    {
        private readonly Token EndOfInputToken = new(TokenType.EndOfInput, null);
        private int position = 0;

        private readonly Token[] tokens;

        public TokenStream(IEnumerable<Token> tokens)
        {
            this.tokens = tokens.ToArray();
        }

        public Token GetToken()
        {
            if (position >= tokens.Length)
            {
                return EndOfInputToken;
            }
            return tokens[position];
        }

        public void Consume()
        {
            position++;
        }

        public IEnumerator<Token> GetEnumerator()
        {
            foreach (var token in tokens)
            {
                yield return token;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}