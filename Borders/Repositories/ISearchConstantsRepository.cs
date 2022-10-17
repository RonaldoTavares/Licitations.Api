using Borders.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Borders.Repositories
{
    public interface ISearchConstantsRepository
    {
        Task<List<SearchConstant>> GetConstantsByDocumentOrgan(string request);
    }
}
