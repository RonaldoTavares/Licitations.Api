using System;
using System.Data;
using System.Net.Http;

namespace Borders.Helpers
{
    public interface IRepositoryHelper
    {
        IDbConnection GetConnection();
        HttpRequestMessage CreateRequest(object body, string endPoint, Uri baseAddress, HttpMethod httpMethod);
    }
}
