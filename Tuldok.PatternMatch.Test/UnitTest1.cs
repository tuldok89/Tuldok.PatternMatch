using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tuldok.PatternMatch.Service;

namespace Tuldok.PatternMatch.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly PatternSearchService _patternSearch;
        private readonly DataProviderService _dataProvider;

        public UnitTest1()
        {
            _patternSearch = new PatternSearchService();
            _dataProvider = new DataProviderService();
        }
        
        [TestMethod]
        public void TestMethod1()
        {
            //var result = _patternSearch.Find("AABAACAADAABAAABAA", "AABA");
            var result = _patternSearch.Find("AABAABACDABAABAABAAABA", "AABA");

            Assert.IsTrue(result.SequenceEqual(new List<int> { 1, 4, 12, 15, 19 }));
        }

        [TestMethod]
        public async Task TestGetText()
        {
            var result = await _dataProvider.GetTextToSearch();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestGetSubtexts()
        {
            var result = await _dataProvider.GetSubTexts();

            Assert.IsNotNull(result);
        }

    }
}