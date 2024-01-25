using Azure;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories;
using System.Net;

namespace MusicStore.Api.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository repository;
        private readonly ILogger<GenresController> logger;

        public GenresController(IGenreRepository repository, ILogger<GenresController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new BaseResponseGeneric<ICollection<GenreResponseDto>>();
            try
            {
                response.Data = await repository.GetAsync();
                response.Success = true;
                logger.LogInformation($"Se obtuvieron todos los géneros musicales.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la informacion.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new BaseResponseGeneric<GenreResponseDto>();
            try
            {
                //var item = await repository.GetAsync(id);
                //logger.LogInformation($"Se obtuvo el géneros musicales con id {id}.");
                //return item is not null ? Ok(item) : NotFound();
                response.Data = await repository.GetAsync(id);
                response.Success = true;
                if (response.Data is null) 
                {
                    logger.LogWarning($"Género musical con id {id} no se encontró.");
                    return NotFound(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = "Ocurrió un error al obtener la informacion.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
                      
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(GenreRequestDto genreRequestDto)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var genreId = await repository.AddAsync(genreRequestDto);
                response.Data = genreId;
                response.Success = true;
                logger.LogInformation($"Géneros musicales con id {genreId} insertado.");
                return StatusCode((int)HttpStatusCode.Created, response);
                //return CreatedAtAction(nameof(Get), new { Id = genre.Id }, genre);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al insertar la informacion.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }   
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, GenreRequestDto genreRequestDto) 
        {
            var response = new BaseResponse();
            try
            {
                //var item = await repository.GetAsync(id);
                //if(item is null)
                //{
                //    logger.LogWarning($"Género musical con id {id} no se encontró.");
                //    return NotFound(response);
                //}
                await repository.UpdateAsync(id, genreRequestDto);
                response.Success = true;
                logger.LogInformation($"Géneros musicales con id {id} actualizado.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al actualizar la informacion.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) 
        {
            var response = new BaseResponse();
            try
            {

                await repository.DeleteAsync(id);
                response.Success = true;
                logger.LogInformation($"Géneros musicales con id {id} eliminado.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al eliminar la informacion.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }

    }
}
