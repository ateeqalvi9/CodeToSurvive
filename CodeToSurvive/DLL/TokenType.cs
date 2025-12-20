namespace CodeToSurvive.Language
{
    public enum TokenType
    {
        Identifier,
        Number,
        String,

        Keyword,
        Operator,
        Punctuation,
        Comment,
        Whitespace,

        Unknown,
        EOF
    }
}