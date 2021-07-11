using AutoMapper;
using RckCntnt.Application.Services.Interfaces;
using RckCntnt.Application.ViewModel;
using RckCntnt.Domain.Models;
using System.Threading.Tasks;

namespace RckCntnt.Application.Services
{
    public class ArticleLikeService : IArticleLikeService
    {
        public readonly IMapper _mapper;

        public ArticleLikeService(
            IMapper mapper
            )
        {
            _mapper = mapper;
        }

        public void AddArticleLike(ArticleLikeViewModel articleLikeViewModel)
        {
            var article = _mapper.Map<ArticleLikeViewModel, Article>(articleLikeViewModel);
        }

        public Task<long> GetArticleLikes(int articleId)
        {
            return Task.FromResult((long)new System.Random().Next(0, 1000000));
        }
    }
}