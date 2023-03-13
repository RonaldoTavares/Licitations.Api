using Borders.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configurations;
using UseCases;

namespace Licitacao.api.Configurations
{
    public class UseCaseConfig
    {
        public static void ConfigureServices(IServiceCollection services, ApplicationConfig applicationConfig)
        {
            services.AddSingleton<IGetHomologationUseCase, GetHomologationUseCase>();
        }
    }
}
