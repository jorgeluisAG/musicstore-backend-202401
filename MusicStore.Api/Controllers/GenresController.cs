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
                //Mapping
                var genresDb = await repository.GetAsync();
                var genresResponseDb = genresDb.Select(x => new GenreResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status,
                }).ToList();

                response.Data = genresResponseDb;
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

                var genreDb = await repository.GetAsync(id);
                if (genreDb is null) 
                {
                    logger.LogWarning($"Género musical con id {id} no se encontró.");
                    return NotFound(response);
                }
                else
                {
                    var genreResponseDto = new GenreResponseDto()
                    {
                        Id = genreDb.Id,
                        Name = genreDb.Name,
                        Status = genreDb.Status,
                    };
                    response.Data = genreResponseDto;
                    response.Success = true;
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
                var genreDb = new Genre()
                {
                    Name = genreRequestDto.Name,
                    Status = genreRequestDto.Status,
                };

                var genreId = await repository.AddAsync(genreDb);
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
                var genreDb = await repository.GetAsync(id);
                if (genreDb is null)
                {
                    response.ErrorMessage = "No se encontró el registro.";
                    return NotFound(response);
                }
                genreDb.Name = genreRequestDto.Name;
                genreDb.Status = genreRequestDto.Status;

                await repository.UpdateAsync();
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
                var genreDb = await repository.GetAsync(id);
                if (genreDb is null)
                {
                    response.ErrorMessage = "No se encontró el registro.";
                    return NotFound(response);
                }
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
