using MongoDB.Bson.Serialization.Attributes;

namespace RckCntnt.Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Article
    {
        public int ArticleId { get; set; }
        public long LikesQty { get; set; }
    }
}