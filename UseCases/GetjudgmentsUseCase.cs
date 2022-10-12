using Borders.Entities;
using Borders.Enums;
using Borders.Models;
using Borders.Repositories;
using Borders.Services;
using Borders.Shared;
using Borders.UseCases;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class GetjudgmentsUseCase : IGetjudgmentsUseCase
    {
        private readonly ILicitationsService _licitationsService;
        private readonly ILicitationsRepository _licitationsRepository;
        public GetjudgmentsUseCase(ILicitationsService licitationsService, ILicitationsRepository licitationsRepository)
        {
            _licitationsService = licitationsService;
            _licitationsRepository = licitationsRepository;
        }

        public async Task<UseCaseResponse<bool>> Execute()
        {
            try
            {
                var licitations = await _licitationsRepository.GetLicitationsByStatus(LicitationStatus.Open);
                List<string> constantsJudge = new()
                {
                    "Julgamento de Proposta de Preços",
                    "Julgamento de Licitação",
                    "Julgamento Proposta de Preços"
                };

                foreach (Licitation licitation in licitations)
                {
                    var a = _licitationsService.GetDocumentLink("//a[@class='dropfiles_downloadlink']", constantsJudge, licitation.Link);

                    if (!a.Equals(string.Empty))
                    {
                        await _licitationsRepository.UpdateLicitationStatus(licitation.PkLicitation, LicitationStatus.judged);
                    }

                    //var b = GetPdfDownload(a);
                }

                return new UseCaseResponse<bool>().SetResult(true);
            }
            catch(Exception e)
            {
                ErrorMessage errMsg = new("", $"Unespected error. Error: {e.Message}");
                return new UseCaseResponse<bool>().SetInternalServerError(e.Message, new[] { errMsg });
            }
        }

        private static string GetPdfDownload(string link)
        {
            string response = string.Empty;

            using (WebClient client = new WebClient())
            {
                var bytes = client.DownloadData(link);
                response = Encoding.Default.GetString(bytes);
            }

            return response;
        }
    }
}
