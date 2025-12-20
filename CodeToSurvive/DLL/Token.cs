namespace CodeToSurvive.Language
{
    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }
        public int Position { get; set; }
        public int Line { get; set; }

        public Token(TokenType type, string value, int pos, int line)
        {
            Type = type;
            Value = value;
            Position = pos;
            Line = line;
        }
    }
}