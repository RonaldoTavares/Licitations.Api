using Borders.Entities;
using Borders.Enums;
using Borders.Helpers;
using Borders.Repositories;
using Dapper;
using Repositories.SqlStatements;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class LicitationsLinkRepository : ILicitationsLinkRepository
    {
        private readonly IRepositoryHelper _helper;

        public LicitationsLinkRepository(IRepositoryHelper helper)
        {
            _helper = helper;
        }

        public async Task<List<LicitationLink>> GetLicitationsLinksByStatus(LicitationLinkStatus status)
        {
            using var connection = _helper.GetConnection();
            var query = string.Format(LicitationsLinksStatements.GET_BY_STATUS, (int)status);

            var response = await connection.QueryAsync<LicitationLink>(query);

            return response.ToList();
        }
    }
}
