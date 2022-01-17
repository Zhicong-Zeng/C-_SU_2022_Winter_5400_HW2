using System.Collections.Generic;
using System.Text.RegularExpressions;
using RecDescent.Exceptions;
using RecDescent.Tokens;
using static RecDescent.Tokens.TokenType;

namespace RecDescent.Scanners
{
    public class Scanner
    {
        private readonly Dictionary<string, TokenType> Patterns = new()
        {
            [@"\("] = LeftParen,
            [@"\)"] = RightParen,
            [@"\+"] = AddOperator,
            [@"-"] = SubOperator,
            [@"\*"] = MulOperator,
            [@"/"] = DivOperator,
            [@"%"] = ModOperator,
            [@"-?\d+"] = Number,
            [@"[A-Za-z]+[A-Za-z0-9_]*"] = Identifier,
        };

        // Splits the input string on whitespace characters,
        // then converts each piece into a Token object.
        // Finally, creates a new TokenStream on the List<Token>
        // for the parser to consume.
        public TokenStream GenerateTokens(string input)
        {
            var tokens = new List<Token>();

            var pieces = input.Split(new char[] { ' ', '\t', '\n' });

            foreach (var piece in pieces)
            {
                // TODO: implement this and populate the `tokens` list.
                // Should be a lot like the previous homework.
                // Token(TokenType type, string lexeme)
                if (Regex.IsMatch(piece,@"\("))
                {
                    tokens.Add(new Token(LeftParen, piece));
                }
                else if (Regex.IsMatch(piece, @"\)"))
                {
                    tokens.Add(new Token(RightParen, piece));
                }
                else if(Regex.IsMatch(piece, @"\+"))
                {
                    tokens.Add(new Token(AddOperator, piece));
                }
                //Just Integer
                //else if (Regex.IsMatch(piece, @"-?\d+"))
                //else if (Regex.IsMatch(piece, @"^[-+]?\d*$"))
                else if (Regex.IsMatch(piece, @"^(\+|-)?\d+$"))
                {
                    tokens.Add(new Token(Number, piece));
                }
                else if (Regex.IsMatch(piece, @"-"))
                {
                    tokens.Add(new Token(SubOperator, piece));
                }
                else if (Regex.IsMatch(piece, @"\*"))
                {
                    tokens.Add(new Token(MulOperator, piece));
                }
                else if (Regex.IsMatch(piece, @"/"))
                {
                    tokens.Add(new Token(DivOperator, piece));
                }
                else if (Regex.IsMatch(piece, @"%"))
                {
                    tokens.Add(new Token(ModOperator, piece));
                }
                else if (Regex.IsMatch(piece, @"[A-Za-z]+[A-Za-z0-9_]*"))
                {
                    tokens.Add(new Token(Identifier, piece));
                }
                else
                {
                    throw new UnknownTokenException(piece);
                }
            }
            return new TokenStream(tokens);
        }
    }
}