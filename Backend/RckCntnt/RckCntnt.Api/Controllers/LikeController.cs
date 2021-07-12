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
        public async Task<IActionResult> Post([FromBody] ArticleLikeViewModel articleLike)
        {
            try
            {
                await _bus.Publish(articleLike);

                _logger.LogInformation($"Like Received for Article: {articleLike.ArticleId}");
            }
            catch (System.Exception)
            {
                _logger.LogError($"Error trying to send message: {articleLike.ArticleId}");
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpGet("{articleId}")]
        public async Task<IActionResult> Get([FromRoute] int articleId)
        {
            try
            {
                var likesCount = await _articleLikeService.GetArticleLikes(articleId);

                return Ok(new { likesCount });
            }
            catch (System.Exception)
            {
                _logger.LogError($"Error trying to get ArticleLikes: {articleId}");
                return StatusCode(500);
            }
        }
    }
}