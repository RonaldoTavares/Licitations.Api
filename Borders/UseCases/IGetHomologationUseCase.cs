using Borders.Shared;
using System.Threading.Tasks;

namespace Borders.UseCases
{
    public interface IGetHomologationUseCase
    {
        Task<UseCaseResponse<bool>> Execute();
    }
}
