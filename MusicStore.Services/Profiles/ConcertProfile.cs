using AutoMapper;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Entities.Info;


namespace MusicStore.Services.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<Concertinfo, ConcertResponseDto>();//origin -> destination 
            CreateMap<Concert, ConcertResponseDto>();
        }
    }
}
