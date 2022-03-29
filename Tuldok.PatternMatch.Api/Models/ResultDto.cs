namespace Tuldok.PatternMatch.Api.Models
{
    internal class ResultDto
    {
        public string Candidate { get; set; } = default!;
        public string Text { get; set; } = default!;

        public IEnumerable<SubTextResult> Results { get; set; } = default!;
    }
}
