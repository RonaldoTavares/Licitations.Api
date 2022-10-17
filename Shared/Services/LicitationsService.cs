using Borders.Entities;
using Borders.Enums;
using Borders.Services;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Services
{
    public class LicitationsService : ILicitationsService
    {
        public LicitationsService()
        {
        }

        public string CutInformation(string initial, string end, string text)
        {
            var textUpper = text.ToUpper();
            var initialPosition = textUpper.IndexOf(initial.ToUpper());
            var endPosition = textUpper.IndexOf(end.ToUpper());

            if (initialPosition >= 0 && endPosition > 0)
            {
                return text[(initialPosition + initial.Length)..endPosition];
            }

            return "";
        }

        public string GetDocumentLink(string selectNode, List<string> informations, string link)
        {
            HtmlWeb web = new();
            bool contains = false;
            string linkPDF = string.Empty;
            HtmlDocument document = web.Load(link);

            var results = document.DocumentNode.SelectNodes(selectNode);

            informations.ForEach(information =>
            {
                if (linkPDF.Equals(string.Empty))
                {
                    results.ToList().ForEach(result =>
                    {
                        contains = result.InnerText.ToUpper().IndexOf(information.ToUpper()) >= 0;
                        if (contains)
                            linkPDF = this.CutInformation("href=\"", "\">", result.OuterHtml);
                    });
                }
            });

            return linkPDF;
        }

        public Licitation? GetLicitation(string link, HtmlDocument document, List<SearchConstant> constants, Organ organ)
        {
            var result = document.DocumentNode.SelectNodes(constants.First(constant => constant.Type.Equals(SearchConstants.HtmlSection)).Constant);
            var textInformation = result[Convert.ToInt32(constants.First(constant => constant.Type.Equals(SearchConstants.PositionArray)).Constant)].InnerText.Replace("&nbsp;", " ");
            textInformation = CutLastText(constants.First(constant => constant.Type.Equals(SearchConstants.NoticeStart)).Constant, textInformation);
            var edital = this.CutInformation(constants.First(constant => constant.Type.Equals(SearchConstants.NoticeStart)).Constant, constants.First(constant => constant.Type.Equals(SearchConstants.NoticeEnd)).Constant, textInformation).Trim();
            textInformation = CutLastText(constants.First(constant => constant.Type.Equals(SearchConstants.ObjectStart)).Constant, textInformation);
            var objeto = this.CutInformation(constants.First(constant => constant.Type.Equals(SearchConstants.ObjectStart)).Constant, constants.First(constant => constant.Type.Equals(SearchConstants.ObjectEnd)).Constant, textInformation).Trim();
            textInformation = CutLastText(constants.First(constant => constant.Type.Equals(SearchConstants.ValueStart)).Constant, textInformation);
            var valorOrcado = this.CutInformation(constants.First(constant => constant.Type.Equals(SearchConstants.ValueStart)).Constant, constants.First(constant => constant.Type.Equals(SearchConstants.ValueEnd)).Constant, textInformation).Trim();
            textInformation = CutLastText(constants.First(constant => constant.Type.Equals(SearchConstants.DateStart)).Constant, textInformation);
            var dataAbertura = this.CutInformation(constants.First(constant => constant.Type.Equals(SearchConstants.DateStart)).Constant, constants.First(constant => constant.Type.Equals(SearchConstants.DateEnd)).Constant, textInformation).Trim();

            if (edital.Equals(string.Empty) || objeto.Equals(string.Empty) || valorOrcado.Equals(string.Empty) || dataAbertura.Equals(string.Empty) || !decimal.TryParse(valorOrcado, out decimal n) || !DateTime.TryParse(dataAbertura, out DateTime a))
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
                OrganName = organ.OrganName,
                OrganDocument = organ.OrganDocument,
                Status = LicitationStatus.Open,
                Link = link,
                CreateDate = DateTime.Now
            };

            return licitation;
        }

        public static string CutLastText(string initial, string text)
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

        public bool GetValidLink(HtmlDocument document, List<SearchConstant> constants)
        {
            HtmlNodeCollection result = document.DocumentNode.SelectNodes(constants.First(constant => constant.Type.Equals(SearchConstants.HtmlSection)).Constant);

            if (result == null)
                return false;

            var textInformation = result[Convert.ToInt32(constants.First(constant => constant.Type.Equals(SearchConstants.PositionArray)).Constant)].InnerText.Replace("&nbsp;", " ");
            textInformation = CutLastText(constants.First(constant => constant.Type.Equals(SearchConstants.EndLicitationPage)).Constant, textInformation);

            return textInformation.Equals(String.Empty);
        }
    }
}
