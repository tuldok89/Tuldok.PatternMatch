using System.Collections.Generic;

namespace Tuldok.PatternMatch.Service.Interfaces
{
    public interface IPatternSearchService
    {
        List<int> Find(string text, string pattern);
    }
}