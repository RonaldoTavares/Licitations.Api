using Borders.Entities;
using Borders.Enums;
using Borders.Helpers;
using Borders.Repositories;
using Dapper;
using Repositories.SqlStatements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Repositories.Repositories
{
    public class LicitationsRepository : ILicitationsRepository
    {
        private readonly IRepositoryHelper _helper;

        public LicitationsRepository(IRepositoryHelper helper)
        {
            _helper = helper;
        }

        public async Task<bool> CreateLicitation(Licitation request)
        {
            var sql = LicitationStatements.CREATE_LICITATION;
            
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = _helper.GetConnection())
                {
                    await connection.QueryAsync<Licitation>(sql, request);
                }

                scope.Complete();
            }

            return true;
        }

        public async Task<List<Licitation>> GetLicitationsByStatus(LicitationStatus request)
        {
            using var connection = _helper.GetConnection();
            var parameters = new DynamicParameters();
            var query = LicitationStatements.GET_LICITATION_BY_STATUS;
            parameters.Add("@Status", request);

            var response = await (connection.QueryAsync<Licitation>(query, parameters));

            return response.ToList();
        }

        public async Task<bool> UpdateLicitationStatus(Guid id, LicitationStatus status)
        {
            using var connection = _helper.GetConnection();
            var parameters = new DynamicParameters();
            var query = LicitationStatements.UPDATE_LICITATION_STATUS_BY_ID;
            parameters.Add("@Status", status);
            parameters.Add("@PkLicitation", id);

            var response = await (connection.QueryAsync<bool>(query, parameters));

            return true;
        }
    }
}
