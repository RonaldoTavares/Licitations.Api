using Borders.Services;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configurations;
using Shared.Services;

namespace Licitacao.api.Configurations
{
    public class ServiceConfig
    {
        public static void ConfigureServices(IServiceCollection services, ApplicationConfig applicationConfig)
        {
            services.AddSingleton<ILicitationsService, LicitationsService>();
        }
    }
}
