using Borders.Services;
using HtmlAgilityPack;
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
    }
}
