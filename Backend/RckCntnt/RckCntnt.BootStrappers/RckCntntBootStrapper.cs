using Microsoft.Extensions.DependencyInjection;
using RckCntnt.Application.Services;
using RckCntnt.Application.Services.Interfaces;

namespace RckCntnt.BootStrappers
{
    public class RckCntntBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IArticleLikeService, ArticleLikeService>();

            #endregion Services
        }
    }
}