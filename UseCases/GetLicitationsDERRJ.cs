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
using System.Threading.Tasks;

namespace UseCases
{
    public class GetLicitationsDERRJ : IGetLicitationsDERRJ
    {
        private readonly ILicitationsService _licitationsService;
        private readonly ILicitationsRepository _licitationsRepository;

        public GetLicitationsDERRJ(ILicitationsService licitationsService, ILicitationsRepository licitationsRepository)
        {
            _licitationsService = licitationsService;
            _licitationsRepository = licitationsRepository;
        }

        public async Task<UseCaseResponse<bool>> Execute()
        {
            try
            {
                int lastLicitation = 1500;
                bool validLink = true;
                int runFind = 3;

                HtmlWeb web = new();

                var licitations = new List<Licitation>();

                while (validLink || runFind > 0)
                {
                    var link = $"https://www.der.rj.gov.br/licitacao_completo.asp?ident={lastLicitation}";
                    HtmlDocument document = web.Load(link);

                     validLink = GetValidLink("//body", document);

                    if (validLink)
                    {
                        runFind = 3;
                        var licitation = GetLicitation("//body", link, document);
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

                return new UseCaseResponse<bool>().SetResult(true);
            }
            catch (Exception e)
            {
                ErrorMessage errMsg = new("", $"Unespected error closing lead. Error: {e.Message}");
                return new UseCaseResponse<bool>().SetInternalServerError(e.Message, new[] { errMsg });
            }
        }

        public string CutLastText(string initial, string text)
        {
            var textUpper = text.ToUpper();
            var textCount = text.Length;
            var initialPosition = textUpper.IndexOf(initial.ToUpper());

            if (initialPosition > 0)
            {
                return text[(initialPosition)..(textCount)];
            }

            return "";
        }

        private bool GetValidLink(string htmlSection, HtmlDocument document)
        {
            var result = document.DocumentNode.SelectNodes(htmlSection);
            var textInformation = result[0].InnerText.Replace("&nbsp;", " ");
            textInformation = CutLastText("An error occurred on the server when processing the URL.", textInformation);

            return textInformation.Equals(String.Empty);
        }

        private Licitation? GetLicitation(string htmlSection, string link, HtmlDocument document)
        {
            var result = document.DocumentNode.SelectNodes(htmlSection);
            var textInformation = result[0].InnerText.Replace("&nbsp;", " ");
            textInformation = CutLastText("REF: ", textInformation);
            var edital = _licitationsService.CutInformation("REF: ", "TIPO: ", textInformation).Trim();
            textInformation = CutLastText("OBJETO: ", textInformation);
            var objeto = _licitationsService.CutInformation("OBJETO: ", "ORÇAMENTO OFICIAL: ", textInformation).Trim();
            textInformation = CutLastText("ORÇAMENTO OFICIAL: R$ ", textInformation);
            var valorOrcado = _licitationsService.CutInformation("ORÇAMENTO OFICIAL: R$ ", " (", textInformation).Trim();
            textInformation = CutLastText("DATA DA LICITAÇÃO: ", textInformation);
            var dataAbertura = _licitationsService.CutInformation("DATA DA LICITAÇÃO: ", " às", textInformation).Trim();

            if (edital.Equals(string.Empty) || objeto.Equals(string.Empty) || valorOrcado.Equals(string.Empty) || dataAbertura.Equals(string.Empty))
            {
                return null;
            }

            var licitation = new Licitation()
            {
                PkLicitation = Guid.NewGuid(),
                Notice = edital,
                Object = objeto,
                Value = Convert.ToDecimal(valorOrcado),
                OpeningDate = DateTime.Parse(dataAbertura),
                OrganName = "DER RJ",
                OrganDocument = "28521870000125",
                Status = LicitationStatus.Open,
                Link = link,
                CreateDate = DateTime.Now
            };

            return licitation;
        }
    }
}
