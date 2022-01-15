using RecDescent.Exceptions;
using RecDescent.Scanners;
using RecDescent.Tokens;
using RecDescent.Tree;
using static RecDescent.Tokens.TokenType;

namespace RecDescent.Parsers
{
    public class RecDescentParser
    {
        public TokenStream TokenStream { get; private set; }

        // The main entry point of our parser.  Creates a
        // a TokenStream from the input and then invokes
        // the Goal rule on it.
        public TokenTreeNode Parse(string input)
        {
            TokenStream = new Scanner().GenerateTokens(input);
            return Goal();
        }

        // Since we're using a predictive parser model, we won't
        // return a bool to indicate success or failure; instead,
        // on failure we'll throw an exception and otherwise
        // assume success.
        private void Match(TokenType target)
        {
            if (TokenStream.GetToken().TokenType == target)
            {
                TokenStream.Consume();
            }
            else
            {
                throw new ParseException(target, TokenStream.GetToken().TokenType);
            }
        }

        // TODO:
        // Need to create one method per rule:
        //   * Goal
        //   * Expr
        //   * ExprPrime
        //   * Term
        //   * TermPrime
        //   * Factor
        // Each method should return a TokenTreeNode that
        // represents the rule and any production trees
        // the rule generated.

        // Goal is pretty simple: just create a tree
        // node whose only child is whatever Expr generates.
        private TokenTreeNode Goal()
        {
            return new TokenTreeNode(TokenType.Goal, Expr());
        }

        // Expr is just a little more complicated: create
        // a treen node whose children are the results of
        // *both* the Term and ExprPrime rules, per the
        // grammar.
        private TokenTreeNode Expr()
        {
            var res = new TokenTreeNode(TokenType.Expr);

            var termTree = Term();
            var epTree = ExprPrime();
            return res.AddChildren(termTree, epTree);
        }

        // ExprPrime has alternatives, so we need to
        // check which one to take.  We do this by
        // looking at the current token's type.  If
        // it's a "+" or "-", handle it.  The ε alternative
        // means "don't throw an error if you don't have
        // a match".
        private TokenTreeNode ExprPrime()
        {
            var res = new TokenTreeNode(TokenType.ExprPrime);

            var t = TokenStream.GetToken();

            if (t.TokenType == AddOperator)
            {
                Match(AddOperator);
                var termTree = Term();
                var epTree = ExprPrime();
                res.AddChildren(new TokenTreeNode(t, termTree, epTree));
            }
            else if (t.TokenType == SubOperator)
            {
                Match(SubOperator);
                var termTree = Term();
                var epTree = ExprPrime();
                res.AddChildren(new TokenTreeNode(t, termTree, epTree));
            }

            return res;
        }

        // TODO:
        // Term is very much like Expr
        private TokenTreeNode Term()
        {
        }

        // TODO:
        // TermPrime is very much like ExprPrime
        private TokenTreeNode TermPrime()
        {
        }

        // TODO:
        // Factor should be coded much like ExprPrime.  However,
        // Factor is interesting in that it does *not* have
        // an ε alternative.  This means if we can't match
        // something in Factor, we should throw a
        // ParseException!
        private TokenTreeNode Factor()
        {
        }
    }
}