using System.Collections.Generic;
using static RecDescent.Tokens.TokenType;

namespace RecDescent.Tree
{
    public class TreeEvaluator
    {
        private readonly Stack<int> stack = new();

        public int Evaluate(TokenTreeNode tree, Dictionary<string, int> variables)
        {
            EvaluateImpl(tree, variables);
            return stack.Pop();
        }

        // TODO:
        // Implement this method to walk the parse tree and calculate the result
        // of the expression you parsed.
        // 
        // Hint: make sure you (recursively) call EvaluateImpl for every child of
        // a node, then perform any action necessary for the current node (like
        // pushing a value on the stack if you see a Number, or popping two
        // values off the stack, adding them, and pushing the result on the stack
        // for an AddOperator).
        private void EvaluateImpl(TokenTreeNode node, Dictionary<string, int> variables)
        {
        }
    }
}