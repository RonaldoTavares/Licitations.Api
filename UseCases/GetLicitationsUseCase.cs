using Borders.Entities;
using Borders.Enums;
using Borders.Repositories;
using Borders.Shared;
using Borders.UseCases;
using HtmlAgilityPack;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UseCases
{
    public class GetLicitationsUseCase : IGetLicitationsUseCase
    {
        private readonly ILicitationsRepository _licitationsRepository;

        public GetLicitationsUseCase(ILicitationsRepository licitationsRepository)
        {
            _licitationsRepository = licitationsRepository;
        }

        public async Task<UseCaseResponse<List<Licitation>>> Execute()
        {
            try
            {
                int lastLicitation = 2770;
      
                HtmlWeb  web = new();

                var retorno = string.Empty;
                var activate = false;
                var licitations = new List<Licitation>();

                while (retorno.Equals(string.Empty) || activate)
                {
                    var link = $"http://www.der.mg.gov.br/{lastLicitation}";
                    HtmlDocument document = web.Load(link);

                    retorno = GetInformation(document, "//div[@id='errorboxheader']");

                    if (retorno.Equals(string.Empty))
                    {
                        activate = false;
                        var licitation = GetLicitation(document, link);
                        if (licitation != null)
                            licitations.Add(licitation);
                    }
                    else
                    {
                        activate = !activate;
                    }

                    lastLicitation++;
                }

                foreach (Licitation licitation in licitations)
                {
                    await _licitationsRepository.CreateLicitation(licitation);
                }

                return new UseCaseResponse<List<Licitation>>().SetResult(licitations);
            }
            catch(Exception e)
            {
                ErrorMessage errMsg = new ErrorMessage("", $"Unespected error closing lead. Error: {e.Message}");
                return new UseCaseResponse<List<Licitation>>().SetInternalServerError(e.Message, new[] { errMsg });
            }
        }

        private static string GetInformation(HtmlDocument document, string htmlSection)
        {
            var result = document.DocumentNode.SelectNodes(htmlSection);
            return result != null ?result[0].InnerText.Trim() : string.Empty;
        }

        private static string CutInformation(string initial, string end, string text)
        {
            var initialPosition = text.IndexOf(initial) + initial.Length;
            var endPosition = text.IndexOf(end) - initialPosition;

            if(initialPosition > 0 && endPosition > 0)
            {
                return text.Substring(initialPosition, endPosition);
            }

            return "";
        }

        private static Licitation? GetLicitation(HtmlDocument document, string link)
        {

            var corpo = GetInformation(document, "//section[@class='article-content clearfix']//p");

            var edital = GetInformation(document, "//h1[@class='article-title']");
            var objeto = CutInformation("Objeto: ", "Valor Orçado: ", corpo).Trim();
            var valorOrcado = CutInformation("Valor Orçado: R$ ", "Data Abertura: ", corpo).Trim();
            var dataAbertura = CutInformation("Data Abertura: ", " às ", corpo).Trim();

            if (corpo.Equals(string.Empty) || objeto.Equals(string.Empty) || dataAbertura.Equals(string.Empty) || valorOrcado.Equals(string.Empty) || !decimal.TryParse(valorOrcado, out _))
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
                OrganName = "DER - DEPARTAMENTO DE EDIFICACOES E ESTRADAS DE RODAGEM DO ESTADO DE MINAS GERAIS",
                OrganDocument = "17309790000194",
                Status = LicitationStatus.Open,
                Link = link,
                CreateDate = DateTime.Now
            };

            return licitation;
        }
    }
}
