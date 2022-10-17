using Borders.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Borders.Repositories
{
    public interface IOrgansRepository
    {
        Task<List<Organ>> GetActiveOrgans();
        Task<bool> UpdateLastLicitation(string document, int lastLicitation);
    }
}
