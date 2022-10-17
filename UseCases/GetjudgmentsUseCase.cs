using Borders.Entities;
using Borders.Enums;
using Borders.Models;
using Borders.Repositories;
using Borders.Services;
using Borders.Shared;
using Borders.UseCases;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class GetjudgmentsUseCase : IGetjudgmentsUseCase
    {
        private readonly ILicitationsService _licitationsService;
        private readonly ILicitationsRepository _licitationsRepository;
        private readonly IOrgansRepository _organsRepository;
        private readonly ISearchConstantsRepository _searchConstantsRepository;

        public GetjudgmentsUseCase(ILicitationsService licitationsService, ILicitationsRepository licitationsRepository, IOrgansRepository organsRepository, ISearchConstantsRepository searchConstantsRepository)
        {
            _licitationsService = licitationsService;
            _licitationsRepository = licitationsRepository;
            _organsRepository = organsRepository;
            _searchConstantsRepository = searchConstantsRepository;
        }

        public async Task<UseCaseResponse<bool>> Execute()
        {
            try
            {
                var organs = await _organsRepository.GetActiveOrgans();

                foreach (Organ organ in organs)
                {
                    var constants = await _searchConstantsRepository.GetConstantsByDocumentOrgan(organ.OrganDocument);
                    var licitations = await _licitationsRepository.GetLicitationsByStatus(LicitationStatus.Open, organ.OrganDocument);
                    var web = new HtmlWeb();

                    foreach (Licitation licitation in licitations)
                    {
                        HtmlDocument document = web.Load(licitation.Link);

                        var result = document.DocumentNode.SelectNodes(constants.First(constant => constant.Type.Equals(SearchConstants.HtmlSection)).Constant);
                        var textInformation = result[Convert.ToInt32(constants.First(constant => constant.Type.Equals(SearchConstants.PositionArray)).Constant)].InnerText.Replace("&nbsp;", " ");

                        var judgeConstants = new List<SearchConstant>();
                        
                        constants.ForEach(constant =>
                        {
                            if (constant.Type.Equals(SearchConstants.Judgment))
                            {
                                judgeConstants.Add(constant);
                            }
                        });

                        var judged = false;

                        foreach (SearchConstant constant in judgeConstants)
                        {
                            if(textInformation.ToUpper().Contains(constant.Constant.ToUpper(), StringComparison.CurrentCulture))
                            {
                                judged = true;
                            }
                        }

                        if (judged)
                        {
                            await _licitationsRepository.UpdateLicitationStatus(licitation.PkLicitation, LicitationStatus.judged);
                        }
                    }
                }

                return new UseCaseResponse<bool>().SetResult(true);
            }
            catch(Exception e)
            {
                ErrorMessage errMsg = new("", $"Unespected error. Error: {e.Message}");
                return new UseCaseResponse<bool>().SetInternalServerError(e.Message, new[] { errMsg });
            }
        }

        //private static string GetPdfDownload(string link)
        //{
        //    string response = string.Empty;

        //    using (WebClient client = new WebClient())
        //    {
        //        var bytes = client.DownloadData(link);
        //        response = Encoding.Default.GetString(bytes);
        //    }

        //    return response;
        //}
    }
}
