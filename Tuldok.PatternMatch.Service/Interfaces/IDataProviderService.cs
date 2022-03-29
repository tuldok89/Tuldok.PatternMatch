using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tuldok.PatternMatch.Service.Interfaces
{
    public interface IDataProviderService
    {
        Task<IEnumerable<string>> GetSubTexts();
        Task<string> GetTextToSearch();
    }
}