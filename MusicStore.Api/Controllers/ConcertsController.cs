using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto;
using MusicStore.Dto.Request;
using MusicStore.Entities;
using MusicStore.Repositories;
using MusicStore.Services.Interface;

namespace MusicStore.Api.Controllers
{
    [ApiController]
    [Route("api/concerts")]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertService service;

        public ConcertsController(IConcertService service)
        {
            this.service = service;
        }


        [HttpGet("title")]
        public async Task<IActionResult> Get(string? title)
        {
            var response = await service.GetAsync(title);
            return response.Success ? Ok(response) : BadRequest(response);

            // title == null ? string.Empty : title      Es equivalente y refactorizando a     title ?? string.Empty   
            //var concerts = await repository.GetAsync(title); //.GetAsync(x => x.Title.Contains(title ?? string.Empty), x => x.DataEvent);
            //return Ok(concerts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        //[HttpPost]
        //public async Task<IActionResult> Post(ConcertRequestDto concertRequestDto)
        //{
        //    var response = new BaseResponseGeneric<int>();
        //    try
        //    {
        //        // validating genre Id
        //        var genre = await genreRepository.GetAsync(concertRequestDto.GenreId);
        //        if (genre is null)
        //        {
        //            response.ErrorMessage = $"El id del género {concertRequestDto.GenreId} es incorrecto.";
        //            logger.LogWarning(response.ErrorMessage);
        //            return BadRequest(response);
        //        }

        //        //Mapping
        //        var concertDb = new Concert
        //        {
        //            Title = concertRequestDto.Title,
        //            Description = concertRequestDto.Description,
        //            Place = concertRequestDto.Place,
        //            UnitPrice = concertRequestDto.UnitPrice,
        //            GenreId = concertRequestDto.GenreId,
        //            DataEvent = concertRequestDto.DataEvent,
        //            ImageUrl = concertRequestDto.ImageUrl,
        //            TicketsQuantity = concertRequestDto.TicketsQuantity,
        //        };
        //        response.Data = await repository.AddAsync(concertDb);
        //        response.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorMessage = "Ocurrió un error al guardar la información.";
        //        logger.LogError(ex, ex.Message);
        //    }
        //    return Ok(response);
        //}
    }
}
