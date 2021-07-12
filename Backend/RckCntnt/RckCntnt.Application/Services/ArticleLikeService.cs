using AutoMapper;
using RckCntnt.Application.Services.Interfaces;
using RckCntnt.Application.ViewModel;
using RckCntnt.Domain.Interfaces.Repository;
using RckCntnt.Domain.Models;
using System.Threading.Tasks;

namespace RckCntnt.Application.Services
{
    public class ArticleLikeService : IArticleLikeService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;

        public ArticleLikeService(
            IMapper mapper,
            IArticleRepository articleRepository
            )
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
        }

        public void AddArticleLike(ArticleLikeViewModel articleLikeViewModel)
        {
            var article = _articleRepository.Get(articleLikeViewModel.ArticleId);

            //#Refactor: WorkAround so I dont have to create another way to create new Articles, as it was not the idea of the challenge.
            if (article == null)
            {
                _articleRepository.Insert(new Article
                {
                    ArticleId = articleLikeViewModel.ArticleId,
                    LikesQty = 1
                });

                return;
            }

            article.LikesQty++;

            _articleRepository.Update(article);
        }

        public Task<long> GetArticleLikes(int articleId)
        {
            var article = _articleRepository.Get(articleId);

            return Task.FromResult(article != null ? article.LikesQty : 0);
        }
    }
}