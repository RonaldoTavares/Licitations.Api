using Borders.Entities;
using System.Threading.Tasks;

namespace Borders.Repositories
{
    public interface ILicitationsRepository
    {
        Task<bool> CreateLicitation(Licitation request);
    }
}
