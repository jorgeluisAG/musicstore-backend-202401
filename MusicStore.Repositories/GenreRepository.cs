using Microsoft.EntityFrameworkCore;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Persistence;

namespace MusicStore.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext context;

        //Constructor
        public GenreRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Métodos
        public async Task<List<GenreResponseDto>> GetAsync()
        {
            var items =  await context.Set<Genre>()
                .AsNoTracking()
                .ToListAsync();

            //Mapping
            var genresResponseDto = items.Select(x=> new GenreResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
            }).ToList();
            return genresResponseDto;
        }

        public async Task<GenreResponseDto?> GetAsync(int id)
        {
            var item = await context.Set<Genre>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            var genreResponseDto = new GenreResponseDto();
            if (item is not null)
            {
                //Mapping
                genreResponseDto.Id = item.Id;
                genreResponseDto.Name = item.Name;
                genreResponseDto.Status = item.Status;

            }
            else
                throw new InvalidOperationException($"No se encontró el registro con id {id}.");

            return genreResponseDto;
        }

        public async Task<int> AddAsync(GenreRequestDto genreRequestDto)
        {
            //Mapping
            var genre = new Genre
            {
                Name = genreRequestDto.Name,
                Status = genreRequestDto.Status,
            };
            context.Set<Genre>().Add(genre);
            await context.SaveChangesAsync();
            return genre.Id;
        }

        public async Task UpdateAsync(int id, GenreRequestDto genreRequestDto)
        {
            var item = await context.Set<Genre>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item is not null)
            {
                //Mapping      
                item.Name = genreRequestDto.Name;
                item.Status = genreRequestDto.Status;
                context.Update(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"No se encontró el registro con id {id}.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var item = await context.Set<Genre>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item is not null)
            {
                context.Set<Genre>().Remove(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"No se encontró el registro con id {id}.");
            }
        }

    }
}
