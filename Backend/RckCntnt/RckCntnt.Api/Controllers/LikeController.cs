using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RckCntnt.Application.Services.Interfaces;
using RckCntnt.Application.ViewModel;
using System.Threading.Tasks;

namespace RckCntnt.Api.Controllers
{
    /// <summary>
    /// Teste?
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILogger<LikeController> _logger;
        private readonly IBusControl _bus;
        private readonly IArticleLikeService _articleLikeService;

        public LikeController(
            IArticleLikeService articleLikeService,
            ILogger<LikeController> logger,
            IBusControl bus)
        {
            _logger = logger;
            _bus = bus;

            _articleLikeService = articleLikeService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] int articleId)
        {
            try
            {
                await _bus.Publish(new ArticleLikeViewModel { ArticleId = articleId });

                _logger.LogInformation($"Like Received for Article: {articleId}");
            }
            catch (System.Exception)
            {
                _logger.LogError($"Error trying to send message: {articleId}");
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(int articleId)
        {
            var likesCount = await _articleLikeService.GetArticleLikes(articleId);

            return Ok(new { articleId, likesCount });
        }
    }
}