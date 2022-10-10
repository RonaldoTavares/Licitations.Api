using Borders.Shared;
using Borders.UseCases;
using Licitacao.api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Licitacao.api.Controllers
{
    [Route("api")]
    [ApiController]
    public class LicitationsController : Controller
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly IGetLicitationsUseCase _getLicitationsUseCase;

        public LicitationsController(IActionResultConverter actionResultConverter, IGetLicitationsUseCase getLicitationsUseCase)
        {
            _actionResultConverter = actionResultConverter;
            _getLicitationsUseCase = getLicitationsUseCase;
        }

        [HttpGet("Licitations")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(500, Type = typeof(bool))]
        public async Task<IActionResult> GetLicitations()
        {

            var response = await _getLicitationsUseCase.Execute();
            return _actionResultConverter.Convert(response);
        }
    }
}
