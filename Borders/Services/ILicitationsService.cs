using System.Collections.Generic;

namespace Borders.Services
{
    public interface ILicitationsService
    {
        string CutInformation(string initial, string end, string text);
        string GetDocumentLink(string selectNode, List<string> informations, string link);
    };
}
