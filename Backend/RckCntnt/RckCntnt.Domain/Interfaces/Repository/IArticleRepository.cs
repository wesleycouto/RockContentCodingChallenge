using RckCntnt.Domain.Models;

namespace RckCntnt.Domain.Interfaces.Repository
{
    public interface IArticleRepository
    {
        void Insert(Article article);

        void Update(Article article);

        Article Get(int articleId);
    }
}