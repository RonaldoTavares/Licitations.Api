using Microsoft.Extensions.DependencyInjection;

namespace Licitacao.api.Configurations
{
    public class UseCaseConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IUseCase, UseCase>();
        }
    }
}
