using Borders.Entities;
using Borders.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Borders.Repositories
{
    public interface ILicitationsLinkRepository
    {
        Task<List<LicitationLink>> GetLicitationsLinksByStatus(LicitationLinkStatus status);
    };
}
