using System;
using System.Collections.Generic;
using System.Text;

namespace CodeToSurvive.Language
{
    public class Tokenizer
    {
        private readonly string _text;
        private int _pos;
        private int _line;

        private readonly HashSet<string> _keywords = new()
        {
            "start", "end",
            "if", "else",
            "repeat", "until",
            "func", "return",
            "var", "set", "to",
            "while", "break", "continue"
        };

        public Tokenizer(string text)
        {
            _text = text ?? "";
            _pos = 0;
            _line = 1;
        }

        private char Current => _pos < _text.Length ? _text[_pos] : '\0';

        private void Advance()
        {
            if (Current == '\n') _line++;
            _pos++;
        }

        public IEnumerable<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (_pos < _text.Length)
            {
                char c = Current;

                // Whitespace
                if (char.IsWhiteSpace(c))
                {
                    Advance();
                    continue;
                }

                // Numbers
                if (char.IsDigit(c))
                {
                    int start = _pos;
                    while (char.IsDigit(Current)) Advance();
                    tokens.Add(new Token(TokenType.Number, _text[start.._pos], start, _line));
                    continue;
                }

                // Identifier / Keyword
                if (char.IsLetter(c))
                {
                    int start = _pos;
                    while (char.IsLetterOrDigit(Current) || Current == '_') Advance();
                    string word = _text[start.._pos];

                    tokens.Add(_keywords.Contains(word)
                        ? new Token(TokenType.Keyword, word, start, _line)
                        : new Token(TokenType.Identifier, word, start, _line));

                    continue;
                }

                // String literal
                if (c == '"')
                {
                    int start = _pos;
                    Advance(); // skip "

                    StringBuilder sb = new();

                    while (Current != '"' && Current != '\0')
                    {
                        sb.Append(Current);
                        Advance();
                    }

                    if (Current == '"') Advance();  // closing "

                    tokens.Add(new Token(TokenType.String, sb.ToString(), start, _line));
                    continue;
                }

                // Comment //
                if (c == '/' && Peek() == '/')
                {
                    int start = _pos;
                    while (Current != '\n' && Current != '\0') Advance();
                    tokens.Add(new Token(TokenType.Comment, "//", start, _line));
                    continue;
                }

                // Operators
                if ("+-*/=<>(){}".Contains(c))
                {
                    int start = _pos;
                    tokens.Add(new Token(TokenType.Operator, c.ToString(), start, _line));
                    Advance();
                    continue;
                }

                // Unknown character
                tokens.Add(new Token(TokenType.Unknown, c.ToString(), _pos, _line));
                Advance();
            }

            tokens.Add(new Token(TokenType.EOF, "", _pos, _line));
            return tokens;
        }

        private char Peek()
        {
            return (_pos + 1 < _text.Length) ? _text[_pos + 1] : '\0';
        }
    }
}
