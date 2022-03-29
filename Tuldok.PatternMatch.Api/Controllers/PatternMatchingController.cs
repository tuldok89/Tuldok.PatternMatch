using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tuldok.PatternMatch.Api.Models;
using Tuldok.PatternMatch.Service.Interfaces;

namespace Tuldok.PatternMatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatternMatchingController : ControllerBase
    {
        private readonly IDataProviderService _dataProviderService;
        private readonly IPatternSearchService _patternSearchService;

        public PatternMatchingController(IDataProviderService dataProviderService, IPatternSearchService patternSearchService)
        {
            _dataProviderService = dataProviderService;
            _patternSearchService = patternSearchService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string text;
            IEnumerable<string> subTexts;

            try
            {
                text = await _dataProviderService.GetTextToSearch();
                subTexts = await _dataProviderService.GetSubTexts();
            }
            catch (TimeoutException)
            {
                return StatusCode(408);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            var results = new List<SubTextResult>();

            foreach (var subText in subTexts)
            {
                var result = _patternSearchService.Find(text, subText);
                
                if (result.Count == 0)
                {
                    results.Add(new SubTextResult
                    {
                        Result = "<No Output>",
                        SubText = subText
                    });
                }
                else
                {
                    results.Add(new SubTextResult
                    {
                        Result = String.Join(", ", result),
                        SubText = subText
                    });
                }
            }

            var resultDto = new ResultDto
            {
                Candidate = "Denver Ivan B. Gazo",
                Results = results,
                Text = text
            };

            try
            {
                await SubmitResult(resultDto);
            }
            catch (TimeoutException)
            {
                return StatusCode(408);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok(resultDto);
        }

        private async Task SubmitResult(ResultDto values)
        {
            var client = new HttpClient();
            var retries = 16;
            while (retries-- > 0)
            {
                try
                {
                    var result = await client.PostAsJsonAsync("https://join.reckon.com/test2/submitResults", values);
                    if (!result.IsSuccessStatusCode)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    throw;
                }
            }

            if (retries == 0)
            {
                throw new TimeoutException();
            }
        }
    }
}
