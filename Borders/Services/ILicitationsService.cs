using Borders.Entities;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace Borders.Services
{
    public interface ILicitationsService
    {
        string CutInformation(string initial, string end, string text);
        string GetDocumentLink(string selectNode, List<string> informations, string link);
        Licitation? GetLicitation(string link, HtmlDocument document, List<SearchConstant> constants, Organ organ);
        bool GetValidLink(HtmlDocument document, List<SearchConstant> constants);
    };
}
