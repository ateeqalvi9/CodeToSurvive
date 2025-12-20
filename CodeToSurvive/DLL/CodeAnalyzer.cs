using System.Collections.Generic;

namespace CodeToSurvive.Language
{
    public static class CodeAnalyzer
    {
        public static List<SyntaxError> Analyze(string code)
        {
            var tokenizer = new Tokenizer(code);
            var tokens = tokenizer.Tokenize();

            var parser = new Parser(tokens);
            return parser.Parse();
        }
    }
}