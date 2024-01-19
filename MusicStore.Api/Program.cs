using MusicStore.Entities;
using MusicStore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registering services
builder.Services.AddSingleton<GenreRepository>();

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
app.MapGet("api/holamundo", () => "hola mundo" );
app.MapGet("api/genresminimal", (GenreRepository repository) =>
{
    return repository.Get();
});

app.MapPost("api/genresminimal", (Genre genre, GenreRepository repository) =>
{
    repository.Add(genre);
    return Results.Ok(genre);
});


app.MapControllers();

app.Run();
