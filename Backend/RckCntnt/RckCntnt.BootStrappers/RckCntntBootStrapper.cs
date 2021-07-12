using Microsoft.Extensions.DependencyInjection;
using RckCntnt.Application.Services;
using RckCntnt.Application.Services.Interfaces;
using RckCntnt.Domain.Interfaces.Repository;
using RckCntnt.Infra.Repository;

namespace RckCntnt.BootStrappers
{
    public class RckCntntBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IArticleLikeService, ArticleLikeService>();

            #endregion Services

            #region Services

            services.AddScoped<IArticleRepository, ArticleRepository>();

            #endregion Services
        }
    }
}