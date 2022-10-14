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
            services.AddSingleton<IGetLicitationsUseCase, GetLicitationsUseCase>();
            services.AddSingleton<IGetjudgmentsUseCase, GetjudgmentsUseCase>();
            services.AddSingleton<IGetHomologationUseCase, GetHomologationUseCase>();
            services.AddSingleton<IGetClosedLicitations, GetClosedLicitations>();
            services.AddSingleton<IGetLicitationsDERRJ, GetLicitationsDERRJ>();
        }
    }
}
