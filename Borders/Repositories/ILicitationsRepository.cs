using Borders.Entities;
using Borders.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Borders.Repositories
{
    public interface ILicitationsRepository
    {
        Task<bool> CreateLicitation(Licitation request);
        Task<List<Licitation>> GetLicitationsByStatus(LicitationStatus request, string document);
        Task<bool> UpdateLicitationStatus(Guid id, LicitationStatus status);
    };
}
