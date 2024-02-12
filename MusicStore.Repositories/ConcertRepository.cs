using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Entities.Info;
using MusicStore.Persistence;

namespace MusicStore.Repositories
{
    public class ConcertRepository : RepositoryBase<Concert>, IConcertRepository
    {
        public ConcertRepository(ApplicationDbContext context) : base(context)
        {

        }

        //public override async Task<ICollection<Concert>> GetAsync()
        //{
        //    //eager loading approach
        //    return await context.Set<Concert>()
        //        .Include(x=>x.Genre)
        //        .AsNoTracking()
        //        .ToListAsync();
        //}

        public async Task<ICollection<Concertinfo>> GetAsync(string? title)
        {
            //optimized eager loading approach
            return await context.Set<Concert>()
                .Include(x => x.Genre)
                .Where(x => x.Title.Contains(title ?? string.Empty))
                .AsNoTracking()
                .Select(x => new Concertinfo
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Place = x.Place,
                    UnitPrice = x.UnitPrice,
                    Genre = x.Genre.Name,
                    GenreId = x.GenreId,
                    DataEvent = x.DataEvent.ToShortDateString(),
                    TimeEvent = x.DataEvent.ToShortTimeString(),
                    ImageUrl = x.ImageUrl,
                    TicketsQuantity = x.TicketsQuantity,
                    Finalized = x.Finalized,
                    Status = x.Status ? "Activo" : "Inactivo"
                })
                .ToListAsync();

            ////lazy loading approach
            //return await context.Set<Concert>()
            //    //.Include(x => x.Genre)
            //    .Where(x => x.Title.Contains(title ?? string.Empty))
            //    .AsNoTracking()
            //    .Select(x => new Concertinfo
            //    {
            //        Id = x.Id,
            //        Title = x.Title,
            //        Description = x.Description,
            //        Place = x.Place,
            //        UnitPrice = x.UnitPrice,
            //        Genre = x.Genre.Name,
            //        GenreId = x.GenreId,
            //        DataEvent = x.DataEvent.ToShortDateString(),
            //        TimeEvent = x.DataEvent.ToShortTimeString(),
            //        ImageUrl = x.ImageUrl,
            //        TicketsQuantity = x.TicketsQuantity,
            //        Finalized = x.Finalized,
            //        Status = x.Status ? "Activo" : "Inactivo"
            //    })
            //    .ToListAsync();

            //raw query
            //var query = context.Set<Concertinfo>().FromSqlRaw("usp_ListConcerts {0}", title ?? string.Empty);
            //return await query.ToListAsync();
        }
    }
}
