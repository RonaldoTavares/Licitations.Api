using Borders.Entities;
using Borders.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Borders.UseCases
{
    public interface IGetLicitationsUseCase
    {
        Task<UseCaseResponse<List<Licitation>>> Execute();
    }
}
