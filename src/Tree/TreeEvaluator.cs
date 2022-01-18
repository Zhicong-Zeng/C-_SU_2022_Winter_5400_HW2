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
            //Like an XML need to parser every node
            if (node.TokenType == Goal)
            {
                EvaluateImpl(node.Children[0], variables);
                return;
            }
            if (node.TokenType == Expr)
            {
                EvaluateImpl(node.Children[0], variables);
                EvaluateImpl(node.Children[1], variables);
                return;
            }
            if (node.TokenType == Term)
            {
                EvaluateImpl(node.Children[0], variables);
                EvaluateImpl(node.Children[1], variables);
                return;
            }
            if (node.TokenType == Factor)
            {
                if(node.Children.Count == 0)    return;

                if (node.Children[0].TokenType == Identifier)
                {
                    stack.Push(variables[node.Children[0].Lexeme]);
                    return;
                }
                if (node.Children[0].TokenType == Number)
                {
                    stack.Push(int.Parse(node.Children[0].Lexeme));
                    return;
                }
            }
            if (node.TokenType == TermPrime)
            {
                if (node.Children.Count == 0) return;

                if (node.Children[0].TokenType == MulOperator)
                {
                    node = node.Children[0];
                    EvaluateImpl(node.Children[0], variables);
                    stack.Push(stack.Pop() * stack.Pop());
                    return;
                }
                if (node.Children[0].TokenType == DivOperator)
                {
                    node = node.Children[0];
                    EvaluateImpl(node.Children[0], variables);
                    int temp = stack.Pop();
                    stack.Push(stack.Pop() / temp);
                    return;
                }
            }
            if (node.TokenType == ExprPrime)
            {
                if (node.Children.Count == 0) return;

                if (node.Children[0].TokenType == AddOperator)
                {
                    node = node.Children[0];
                    EvaluateImpl(node.Children[0], variables);
                    EvaluateImpl(node.Children[1], variables);
                    stack.Push(stack.Pop() + stack.Pop());
                    return;
                }
                if (node.Children[0].TokenType == SubOperator)
                {
                    node = node.Children[0];
                    EvaluateImpl(node.Children[0], variables);
                    EvaluateImpl(node.Children[1], variables);
                    stack.Push(-(stack.Pop() - stack.Pop()));
                    return;
                }
                if (node.Children[0].TokenType == LeftParen)
                {
                    node = node.Children[0];
                    EvaluateImpl(node.Children[0], variables);
                    EvaluateImpl(node.Children[1], variables);
                    return;
                }
            }
            return;
        }
    }
}