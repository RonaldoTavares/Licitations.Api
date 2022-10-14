using Borders.Shared;
using System.Threading.Tasks;

namespace Borders.UseCases
{
    public interface IGetLicitationsDERRJ
    {
        Task<UseCaseResponse<bool>> Execute();
    }
}
