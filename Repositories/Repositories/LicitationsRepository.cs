using Borders.Entities;
using Borders.Helpers;
using Borders.Repositories;
using Dapper;
using Repositories.SqlStatements;
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
    }
}
