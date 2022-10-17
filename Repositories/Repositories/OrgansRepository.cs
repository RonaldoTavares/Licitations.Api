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
    public class OrgansRepository : IOrgansRepository
    {
        private readonly IRepositoryHelper _helper;

        public OrgansRepository(IRepositoryHelper helper)
        {
            _helper = helper;
        }

        public async Task<List<Organ>> GetActiveOrgans()
        {
            using var connection = _helper.GetConnection();
            var query = OrgansStatements.GET_ACTIVE_ORGANS;

            var response = await connection.QueryAsync<Organ>(query);

            return response.ToList();
        }

        public async Task<bool> UpdateLastLicitation(string document, int lastLicitation)
        {
            using var connection = _helper.GetConnection();
            var parameters = new DynamicParameters();
            var query = OrgansStatements.UPDATE_LAST_LICITATION;
            parameters.Add("@LastLicitation", lastLicitation);
            parameters.Add("@OrganDocument", document);

            var response = await (connection.QueryAsync<bool>(query, parameters));

            return true;
        }
    }
}
