using Borders.Entities;
using Borders.Helpers;
using Borders.Repositories;
using Dapper;
using Repositories.SqlStatements;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class SearchConstantsRepository : ISearchConstantsRepository
    {
        private readonly IRepositoryHelper _helper;

        public SearchConstantsRepository(IRepositoryHelper helper)
        {
            _helper = helper;
        }

        public async Task<List<SearchConstant>> GetConstantsByDocumentOrgan(string request)
        {
            using var connection = _helper.GetConnection();
            var parameters = new DynamicParameters();
            var query = SearchConstantsStatements.GET_CONSTANTS_BY_DOCUMENT_ORGAN;
            parameters.Add("@DocumentOrgan", request);

            var response = await connection.QueryAsync<SearchConstant>(query, parameters);

            return response.ToList();
        }
    }
}
