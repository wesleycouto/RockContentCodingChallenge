using RckCntnt.Domain.Models;

namespace RckCntnt.Domain.Interfaces.Repository
{
    public interface IArticleRepository
    {
        void Insert(Article order);

        void Update(Article article);

        Article Get(int articleId);
    }
}