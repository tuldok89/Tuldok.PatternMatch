using System;
using System.Collections.Generic;
using Tuldok.PatternMatch.Service.Interfaces;

namespace Tuldok.PatternMatch.Service
{
    public class PatternSearchService : IPatternSearchService
    {
        public List<int> Find(string text, string pattern)
        {
            var indexes = new List<int>();

            if (string.IsNullOrEmpty(pattern))
            {
                indexes.Add(0);
                return indexes;
            }

            if (text == null || pattern.Length > text.Length)
            {
                return indexes;
            }

            var patternSpan = pattern.AsSpan();
            var textSpan = text.AsSpan();

            var x = patternSpan.Length;
            var y = textSpan.Length;

            for (var i = 0; i <= y - x; i++)
            {
                int j;

                for (j = 0; j < x; j++)
                {
                    //if (textSpan[i + j] != patternSpan[j])
                    if (!textSpan[i + j].ToString().Equals(patternSpan[j].ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                }

                if (j == x)
                {
                    indexes.Add(i+1);
                }
            }

            return indexes;
        }
    }
}
