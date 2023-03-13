using Borders.Entities;
using Borders.Enums;
using Borders.Models;
using Borders.Repositories;
using Borders.Shared;
using Borders.UseCases;
using HtmlAgilityPack;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCases
{
    public class GetHomologationUseCase : IGetHomologationUseCase
    {
        private readonly ILicitationsLinkRepository _licitationsRepository;

        public GetHomologationUseCase(ILicitationsLinkRepository licitationsRepository)
        {
            _licitationsRepository = licitationsRepository;
        }

        public async Task<UseCaseResponse<List<string>>> Execute(string date)
        {
            try
            {
                var licitations = await _licitationsRepository.GetLicitationsLinksByStatus(LicitationLinkStatus.Ativo);

                var web = new HtmlWeb();
                var response = new List<string>();
                var serchText = date == string.Empty || date == null ? DateTime.Now.ToString("dd/MM/yyyy") : date;

                foreach (LicitationLink licitation in licitations)
                {
                    HtmlDocument document = web.Load(licitation.Link);

                    var result = document.DocumentNode.InnerText;

                    if (result.Contains(serchText))
                    {
                        response.Add(licitation.Link);
                    }
                }

                return new UseCaseResponse<List<string>>().SetResult(response);
            }
            catch (Exception e)
            {
                ErrorMessage errMsg = new("", $"Unespected error. Error: {e.Message}");
                return new UseCaseResponse<List<string>>().SetInternalServerError(e.Message, new[] { errMsg });
            }
        }
    }
}
