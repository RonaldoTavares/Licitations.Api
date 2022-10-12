using Borders.Shared;
using System.Threading.Tasks;

namespace Borders.UseCases
{
    public interface IGetClosedLicitations
    {
        Task<UseCaseResponse<bool>> Execute();
    }
}
