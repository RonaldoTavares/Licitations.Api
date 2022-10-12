using Borders.Shared;
using System.Threading.Tasks;

namespace Borders.UseCases
{
    public interface IGetjudgmentsUseCase
    {
        Task<UseCaseResponse<bool>> Execute();
    }
}
