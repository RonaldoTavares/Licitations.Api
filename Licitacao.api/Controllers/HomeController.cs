using Borders.Shared;
using Licitacao.api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Licitacao.api.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IActionResultConverter _actionResultConverter;

        public HomeController(IActionResultConverter actionResultConverter)
        {
            _actionResultConverter = actionResultConverter;
        }

        [HttpGet("home/{request}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(500, Type = typeof(bool))]
        public async Task<IActionResult> Get(bool request)
        {
            var response = new UseCaseResponse<bool>().SetResult(true);
            return _actionResultConverter.Convert(response);
        }
    }
}
