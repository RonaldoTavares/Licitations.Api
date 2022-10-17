using Borders.Entities;
using Borders.Enums;
using Borders.Models;
using Borders.Repositories;
using Borders.Services;
using Borders.Shared;
using Borders.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class GetClosedLicitations : IGetClosedLicitations
    {
        private readonly ILicitationsService _licitationsService;
        private readonly ILicitationsRepository _licitationsRepository;
        public GetClosedLicitations(ILicitationsService licitationsService, ILicitationsRepository licitationsRepository)
        {
            _licitationsService = licitationsService;
            _licitationsRepository = licitationsRepository;
        }

        public async Task<UseCaseResponse<bool>> Execute()
        {
            try
            {
                var licitations = await _licitationsRepository.GetLicitationsByStatus(LicitationStatus.homologate, "");
                List<string> constantsJudge = new()
                {
                    "Extrato de contrato",
                    "Objeto Contratado"
                };

                foreach (Licitation licitation in licitations)
                {
                    var a = _licitationsService.GetDocumentLink("//a[@class='dropfiles_downloadlink']", constantsJudge, licitation.Link);

                    if (!a.Equals(string.Empty))
                    {
                        await _licitationsRepository.UpdateLicitationStatus(licitation.PkLicitation, LicitationStatus.closed);
                    }
                }

                return new UseCaseResponse<bool>().SetResult(true);
            }
            catch (Exception e)
            {
                ErrorMessage errMsg = new("", $"Unespected error. Error: {e.Message}");
                return new UseCaseResponse<bool>().SetInternalServerError(e.Message, new[] { errMsg });
            }
        }
    }
}
