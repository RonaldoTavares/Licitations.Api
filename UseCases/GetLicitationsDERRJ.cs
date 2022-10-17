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
using System.Threading.Tasks;

namespace UseCases
{
    public class GetLicitationsDERRJ : IGetLicitationsDERRJ
    {
        private readonly ILicitationsService _licitationsService;
        private readonly ILicitationsRepository _licitationsRepository;
        private readonly ISearchConstantsRepository _searchConstantsRepository;
        private readonly IOrgansRepository _organsRepository;

        public GetLicitationsDERRJ(ILicitationsService licitationsService, ILicitationsRepository licitationsRepository, ISearchConstantsRepository searchConstantsRepository, IOrgansRepository organsRepository)
        {
            _licitationsService = licitationsService;
            _licitationsRepository = licitationsRepository;
            _searchConstantsRepository = searchConstantsRepository;
            _organsRepository = organsRepository;
        }

        public async Task<UseCaseResponse<bool>> Execute()
        {
            try
            {
                var organs = await _organsRepository.GetActiveOrgans();

                foreach (Organ organ in organs)
                {
                    var constants = await _searchConstantsRepository.GetConstantsByDocumentOrgan(organ.OrganDocument);
                    var lastLicitation = organ.LastLicitation;
                    var licitations = new List<Licitation>();
                    var web = new HtmlWeb();

                    var validLink = true;
                    var runFind = 3;

                    while (validLink || runFind > 0)
                    {
                        var link = $"{constants.First(constant => constant.Type.Equals(SearchConstants.LinkWebPage)).Constant}{lastLicitation}";
                        HtmlDocument document = web.Load(link);

                        validLink = _licitationsService.GetValidLink(document, constants);

                        if (validLink)
                        {
                            runFind = 3;
                            var licitation = _licitationsService.GetLicitation(link, document, constants, organ);
                            if (licitation != null)
                                licitations.Add(licitation);
                        }
                        else
                        {
                            runFind--;
                        }

                        lastLicitation++;
                    }

                    foreach (Licitation licitation in licitations)
                    {
                        await _licitationsRepository.CreateLicitation(licitation);
                    }

                    await _organsRepository.UpdateLastLicitation(organ.OrganDocument, lastLicitation - 3);
                }

                return new UseCaseResponse<bool>().SetResult(true);
            }
            catch (Exception e)
            {
                ErrorMessage errMsg = new("", $"Unespected error closing lead. Error: {e.Message}");
                return new UseCaseResponse<bool>().SetInternalServerError(e.Message, new[] { errMsg });
            }
        }
    }
}
