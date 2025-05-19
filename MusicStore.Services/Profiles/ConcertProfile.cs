using AutoMapper;
using MusicStore.Dto.Request;
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
            CreateMap<Concert, ConcertResponseDto>()
                .ForMember(d => d.DataEvent, o => o.MapFrom(x => x.DataEvent.ToShortDateString()))
                .ForMember(d => d.TimeEvent, o => o.MapFrom(x => x.DataEvent.ToShortTimeString()))
                .ForMember(d => d.Status, o => o.MapFrom(x => x.Status ? "Activo" : "Inactivo"));

            CreateMap<ConcertRequestDto, Concert>()
                .ForMember(d => d.DataEvent, o => o.MapFrom(x => Convert.ToDateTime($"{x.DataEvent} {x.TimeEvent}")));
        }
    }
}
