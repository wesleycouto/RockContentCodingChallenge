using AutoMapper;
using RckCntnt.Application.ViewModel;
using RckCntnt.Domain.Models;

namespace RckCntnt.Application.AutoMapper
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<ArticleLikeViewModel, Article>();
        }
    }
}