using Borders.UseCases;
using Microsoft.Extensions.DependencyInjection;
using UseCases;

namespace Licitacao.api.Configurations
{
    public class UseCaseConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGetLicitationsUseCase, GetLicitationsUseCase>();
        }
    }
}
