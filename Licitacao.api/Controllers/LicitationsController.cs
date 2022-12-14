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
        private readonly IGetjudgmentsUseCase _getjudgmentsUseCase;
        private readonly IGetHomologationUseCase _getHomologationUseCase;
        private readonly IGetClosedLicitations _getClosedLicitations;
        private readonly IGetLicitationsDERRJ _getLicitationsDERRJ;

        public LicitationsController(IActionResultConverter actionResultConverter, 
            IGetjudgmentsUseCase getjudgmentsUseCase,
            IGetHomologationUseCase getHomologationUseCase,
            IGetClosedLicitations getClosedLicitations,
            IGetLicitationsDERRJ getLicitationsDERRJ)
        {
            _actionResultConverter = actionResultConverter;
            _getjudgmentsUseCase = getjudgmentsUseCase;
            _getHomologationUseCase = getHomologationUseCase;
            _getClosedLicitations = getClosedLicitations;
            _getLicitationsDERRJ = getLicitationsDERRJ;
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

        [HttpGet("Licitations")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(500, Type = typeof(bool))]
        public async Task<IActionResult> GetLicitationsDERRJ()
        {

            var response = await _getLicitationsDERRJ.Execute();
            return _actionResultConverter.Convert(response);
        }
    }
}
