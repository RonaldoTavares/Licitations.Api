using Microsoft.Extensions.Configuration;
using Shared.Configurations;

namespace Licitacao.api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static ApplicationConfig LoadConfiguration(this IConfiguration source)
        {
            var applicationConfig = source.Get<ApplicationConfig>();
            applicationConfig.Database.ConnectionString = source.GetConnectionString("DefaultConnection");
            applicationConfig.Database.DbFactoryName = source.GetValue<string>("Database:DbFactoryName");
            applicationConfig.Database.AssemblyName = source.GetValue<string>("Database:AssemblyName");
            return applicationConfig;
        }
    }
}
