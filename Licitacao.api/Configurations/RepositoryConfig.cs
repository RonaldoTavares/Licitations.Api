using Borders.Helpers;
using Borders.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Helpers;
using Repositories.Repositories;
using Shared.Configurations;

namespace Licitacao.api.Configurations
{
    public class RepositoryConfig
    {
        public static void ConfigureServices(IServiceCollection services, ApplicationConfig applicationConfig)
        {
            services.AddSingleton<IRepositoryHelper, RepositoryHelper>();
            services.AddSingleton<ILicitationsRepository, LicitationsRepository>();
        }
    }
}
