using System.Collections.Generic;
using RecDescent.Tokens;

namespace RecDescent.Tree
{
    public class TokenTreeNode : Token
    {
        public List<TokenTreeNode> Children { get; init; } = new();

        public TokenTreeNode(TokenType type) : base(type, type.ToString()) { }

        public TokenTreeNode(TokenType type, params TokenTreeNode[] children) : base(type, type.ToString())
        {
            Children.AddRange(children);
        }

        public TokenTreeNode(Token t) : base(t.TokenType, t.Lexeme) { }

        public TokenTreeNode(Token t, params TokenTreeNode[] children) : this(t)
        {
            Children.AddRange(children);
        }

        public TokenTreeNode AddChildren(params TokenTreeNode[] children)
        {
            Children.AddRange(children);
            return this;
        }

        public string RenderTree()
        {
            if (Children.Count > 0)
            {
                var s = "(" + Lexeme;

                foreach (var child in Children)
                {
                    var childText = child.RenderTree();
                    s += childText == string.Empty ? "" : " " + childText;
                }

                return s + ")";
            }
            else
            {
                if (TokenType is TokenType.Number or TokenType.Identifier)
                {
                    return "(" + Lexeme + ")";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}