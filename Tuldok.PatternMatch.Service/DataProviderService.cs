using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tuldok.PatternMatch.Service.Models;
using System.Net.Http.Json;
using Tuldok.PatternMatch.Service.Interfaces;

namespace Tuldok.PatternMatch.Service
{
    public class DataProviderService : IDataProviderService
    {
        public async Task<string> GetTextToSearch()
        {
            var client = new HttpClient();
            var retries = 16;

            while (retries-- > 0)
            {
                try
                {
                    var message = await client.GetAsync("https://join.reckon.com/test2/textToSearch");

                    if (!message.IsSuccessStatusCode)
                    {
                        continue;
                    }

                    var textDto = await message.Content.ReadFromJsonAsync<TextDto>();

                    if (textDto != null)
                    {
                        return textDto.Text;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            throw new TimeoutException();
        }

        public async Task<IEnumerable<string>> GetSubTexts()
        {
            var client = new HttpClient();
            var retries = 16;

            while (retries-- > 0)
            {
                try
                {
                    var message = await client.GetAsync("https://join.reckon.com/test2/subTexts");
                    if (!message.IsSuccessStatusCode)
                    {
                        continue;
                    }
                    var subtext = await message.Content.ReadFromJsonAsync<SubtextDto>();

                    if (subtext != null)
                    {
                        return subtext.SubTexts;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            throw new TimeoutException();
        }
    }
}
