using MassTransit;
using Microsoft.Extensions.Logging;
using RckCntnt.Application.Services.Interfaces;
using RckCntnt.Application.ViewModel;
using System;
using System.Threading.Tasks;

namespace RckCntnt.Api.Consumers
{
    public class LikeConsumer : IConsumer<ArticleLikeViewModel>
    {
        private readonly ILogger<LikeConsumer> _logger;
        private readonly IArticleLikeService _articleLikeService;

        public LikeConsumer(
            ILogger<LikeConsumer> logger,
            IArticleLikeService articleLikeService
            )
        {
            _logger = logger;
            _articleLikeService = articleLikeService;
        }

        public Task Consume(ConsumeContext<ArticleLikeViewModel> context)
        {
            try
            {
                var articleLike = context.Message;

                _logger.LogInformation($"Like on Article: {articleLike.ArticleId}");

                _articleLikeService.AddArticleLike(articleLike);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on LikeConsumer", ex);
            }

            return Task.CompletedTask;
        }
    }
}