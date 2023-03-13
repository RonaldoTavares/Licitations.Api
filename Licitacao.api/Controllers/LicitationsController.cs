using Borders.UseCases;
using Licitacao.api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licitacao.api.Controllers
{
    [Route("api")]
    [ApiController]
    public class LicitationsController : Controller
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly IGetHomologationUseCase _getHomologationUseCase;

        public LicitationsController(IActionResultConverter actionResultConverter, 
            IGetHomologationUseCase getHomologationUseCase)
        {
            _actionResultConverter = actionResultConverter;
            _getHomologationUseCase = getHomologationUseCase;
        }

        [HttpGet("Licitations/Atualizacoes")]
        [ProducesResponseType(200, Type = typeof(List<string>))]
        [ProducesResponseType(500, Type = typeof(List<string>))]
        public async Task<IActionResult> GetHomologation(string date)
        {
            var response = await _getHomologationUseCase.Execute(date);
            return _actionResultConverter.Convert(response);
        }
    }
}
