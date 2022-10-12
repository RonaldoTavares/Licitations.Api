using Borders.Entities;
using Borders.Enums;
using Borders.Models;
using Borders.Repositories;
using Borders.Services;
using Borders.Shared;
using Borders.UseCases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UseCases
{
    public class GetHomologationUseCase : IGetHomologationUseCase
    {
        private readonly ILicitationsService _licitationsService;
        private readonly ILicitationsRepository _licitationsRepository;
        public GetHomologationUseCase(ILicitationsService licitationsService, ILicitationsRepository licitationsRepository)
        {
            _licitationsService = licitationsService;
            _licitationsRepository = licitationsRepository;
        }

        public async Task<UseCaseResponse<bool>> Execute()
        {
            try
            {
                var licitations = await _licitationsRepository.GetLicitationsByStatus(LicitationStatus.judged);

                List<string> constantsJudge = new()
                {
                    "Aviso de Homologação"
                };

                foreach (Licitation licitation in licitations)
                {
                    var a = _licitationsService.GetDocumentLink("//a[@class='dropfiles_downloadlink']", constantsJudge, licitation.Link);

                    if (!a.Equals(string.Empty))
                    {
                       await _licitationsRepository.UpdateLicitationStatus(licitation.PkLicitation, LicitationStatus.homologate);
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
