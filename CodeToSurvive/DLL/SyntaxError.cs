namespace CodeToSurvive.Language
{
    public class SyntaxError
    {
        public int Position { get; set; }
        public int Line { get; set; }
        public int Length { get; set; }
        public string Message { get; set; }
    }
}