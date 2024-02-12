using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Entities.Info;
using System.Reflection;


namespace MusicStore.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.Entity<Genre>().Property(x =>  x.Name).HasMaxLength(50);

            modelBuilder.Entity<Concertinfo>().HasNoKey();
        }

        //Entities to tables
        //public DbSet<Genre> Genres { get; set; }


    }
}
