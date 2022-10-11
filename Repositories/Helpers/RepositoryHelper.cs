using Borders.Helpers;
using Newtonsoft.Json;
using Repositories.Factories;
using Shared.Configurations;
using System;
using System.Data;
using System.Data.Common;
using System.Net.Http;
using System.Text;

namespace Repositories.Helpers
{
    public class RepositoryHelper : IRepositoryHelper
    {
        private readonly DbProviderFactory dbProviderFactory;
        private readonly string connectionString;
        public RepositoryHelper(ApplicationConfig configuration)
        {
            dbProviderFactory = DatabaseFactory.GetDbProviderFactory(configuration.Database.DbFactoryName, configuration.Database.AssemblyName);
            connectionString = configuration.Database.ConnectionString;
        }

        public IDbConnection GetConnection()
        {
            var connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = connectionString;

            return connection;
        }

        public HttpRequestMessage CreateRequest(object body, string endpoint, Uri baseAddress, HttpMethod httpMethod)
        {
            var uriBuilder = new UriBuilder($"{baseAddress}{endpoint}");
            if (httpMethod == HttpMethod.Get)
            {
                uriBuilder.Query = body.ToString();
            }
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, uriBuilder.Uri.ToString());
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            return request;
        }
    }
}