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
        private readonly IGetjudgmentsUseCase _getjudgmentsUseCase;
        private readonly IGetHomologationUseCase _getHomologationUseCase;
        private readonly IGetClosedLicitations _getClosedLicitations;

        public LicitationsController(IActionResultConverter actionResultConverter, 
            IGetLicitationsUseCase getLicitationsUseCase,
            IGetjudgmentsUseCase getjudgmentsUseCase,
            IGetHomologationUseCase getHomologationUseCase,
            IGetClosedLicitations getClosedLicitations)
        {
            _actionResultConverter = actionResultConverter;
            _getLicitationsUseCase = getLicitationsUseCase;
            _getjudgmentsUseCase = getjudgmentsUseCase;
            _getHomologationUseCase = getHomologationUseCase;
            _getClosedLicitations = getClosedLicitations;
        }

        [HttpGet("Licitations")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(500, Type = typeof(bool))]
        public async Task<IActionResult> GetLicitations()
        {

            var response = await _getLicitationsUseCase.Execute();
            return _actionResultConverter.Convert(response);
        }

        [HttpGet("Licitations/judgment")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(500, Type = typeof(bool))]
        public async Task<IActionResult> GetJudgment()
        {

            var response = await _getjudgmentsUseCase.Execute();
            return _actionResultConverter.Convert(response);
        }

        [HttpGet("Licitations/homologation")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(500, Type = typeof(bool))]
        public async Task<IActionResult> GetHomologation()
        {

            var response = await _getHomologationUseCase.Execute();
            return _actionResultConverter.Convert(response);
        }

        [HttpGet("Licitations/closed")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(500, Type = typeof(bool))]
        public async Task<IActionResult> GetClosed()
        {

            var response = await _getClosedLicitations.Execute();
            return _actionResultConverter.Convert(response);
        }
    }
}
