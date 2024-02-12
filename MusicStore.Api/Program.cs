using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Persistence;
using MusicStore.Repositories;
using MusicStore.Services.Implementation;
using MusicStore.Services.Interface;
using MusicStore.Services.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuring context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//Registering services
builder.Services.AddTransient<IGenreRepository,GenreRepository>();
builder.Services.AddTransient<IConcertRepository, ConcertRepository>();
builder.Services.AddTransient<IConcertService, ConcertService>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ConcertProfile>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// esto minimalistas se usan en ocasiones No es que no sea recomendable, pero siempre va el "Depende" de lo que necesites
//app.MapGet("api/holamundo", () => "hola mundo" );
//app.MapGet("api/genresminimal", (GenreRepository repository) =>
//{
//    return repository.Get();
//});
//
//app.MapPost("api/genresminimal", (Genre genre, GenreRepository repository) =>
//{
//    repository.Add(genre);
//    return Results.Ok(genre);
//});


app.MapControllers();

app.Run();
