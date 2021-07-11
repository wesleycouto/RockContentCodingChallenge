using RckCntnt.Application.ViewModel;
using System.Threading.Tasks;

namespace RckCntnt.Application.Services.Interfaces
{
    public interface IArticleLikeService
    {
        void AddArticleLike(ArticleLikeViewModel articleLikeViewModel);

        Task<long> GetArticleLikes(int articleId);
    }
}