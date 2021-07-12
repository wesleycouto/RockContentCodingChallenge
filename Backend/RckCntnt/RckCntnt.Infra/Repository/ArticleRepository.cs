using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using RckCntnt.Domain.Interfaces.Repository;
using RckCntnt.Domain.Models;
using RckCntnt.Infra.Factory;
using System;
using System.Linq.Expressions;

namespace RckCntnt.Infra.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ILogger<ArticleRepository> _logger;
        private readonly IMapper _mapper;

        public ArticleRepository(
            IMapper mapper,
            ILogger<ArticleRepository> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
        }

        public void Insert(Article article)
        {
            try
            {
                var collection = MongoDBConnectionFactory<Article>.GetCollection();

                collection.InsertOne(article);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to insert a new article. ArticleId: {article.ArticleId}", ex);
                throw;
            }
        }

        public void Update(Article article)
        {
            try
            {
                var collection = MongoDBConnectionFactory<Article>.GetCollection();

                Expression<Func<Article, bool>> filter = x => x.ArticleId.Equals(article.ArticleId);

                var _article = collection.Find(filter).FirstOrDefault();

                _article = _mapper.Map<Article>(article);
                ReplaceOneResult result = collection.ReplaceOne(filter, _article);

                article.LikesQty = 1;
                Insert(article);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to update a new article. ArticleId: {article.ArticleId}", ex);
                throw;
            }
        }

        public Article Get(int articleId)
        {
            try
            {
                var collection = MongoDBConnectionFactory<Article>.GetCollection();

                Expression<Func<Article, bool>> filter = x => x.ArticleId.Equals(articleId);

                return collection.Find(filter).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to update a new article. ArticleId: {articleId}", ex);
                throw;
            }
        }
    }
}