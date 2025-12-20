using System.Collections.Generic;

namespace CodeToSurvive.Language
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        private readonly List<SyntaxError> _errors = new();

        public Parser(IEnumerable<Token> tokens)
        {
            _tokens = new List<Token>(tokens);
        }

        public List<SyntaxError> Parse()
        {
            CheckBraces();
            CheckUnknownTokens();
            return _errors;
        }

        private void CheckBraces()
        {
            Stack<Token> stack = new();

            foreach (var t in _tokens)
            {
                if (t.Value is "(" or "{" or "[")
                    stack.Push(t);

                if (t.Value is ")" or "}" or "]")
                {
                    if (stack.Count == 0)
                    {
                        AddError(t, $"Unmatched closing '{t.Value}'");
                        continue;
                    }

                    Token open = stack.Pop();
                    if (!Matches(open.Value, t.Value))
                    {
                        AddError(t, $"Mismatched '{open.Value}' and '{t.Value}'");
                    }
                }
            }

            while (stack.Count > 0)
            {
                Token t = stack.Pop();
                AddError(t, $"Unmatched opening '{t.Value}'");
            }
        }

        private void CheckUnknownTokens()
        {
            foreach (var t in _tokens)
            {
                if (t.Type == TokenType.Unknown)
                {
                    AddError(t, $"Invalid token '{t.Value}'");
                }
            }
        }

        private void AddError(Token t, string msg)
        {
            _errors.Add(new SyntaxError
            {
                Position = t.Position,
                Length = t.Value.Length,
                Line = t.Line,
                Message = msg
            });
        }

        private bool Matches(string open, string close)
        {
            return (open == "(" && close == ")") ||
                   (open == "{" && close == "}") ||
                   (open == "[" && close == "]");
        }
    }
}
