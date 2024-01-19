using Microsoft.AspNetCore.Mvc;
using MusicStore.Entities;
using MusicStore.Repositories;

namespace MusicStore.Api.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly GenreRepository repository;

        public GenresController(GenreRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        public ActionResult<List<Genre>> Get()
        {            
            return repository.Get();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Genre> Get(int id)
        {
            var registro = repository.Get(id);
            return registro is not null ? registro : NotFound();
        }

        [HttpPost]
        public ActionResult Post(Genre genre)
        {
            repository.Add(genre);
            return Ok(genre);         
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Genre genre) 
        {
            repository.Update(id, genre);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) 
        { 
            repository.Delete(id);
            return NoContent();
        }

    }
}
