using Borders.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Borders.UseCases
{
    public interface IGetHomologationUseCase
    {
        Task<UseCaseResponse<List<string>>> Execute(string date);
    }
}
