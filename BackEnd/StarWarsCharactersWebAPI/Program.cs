using StarWarsCharactersWebAPI.Services.Interfaces;
using StarWarsCharactersWebAPI.Services;
using StarWarsCharactersWebAPI.Models;
using System.Text.Json.Serialization;

namespace StarWarsCharactersWebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        // Add Star Wars services
        builder.Services.AddHttpClient();
        builder.Services.AddMemoryCache();
        builder.Services.AddScoped<ICharacterCacheService, CharacterCacheService>();
        builder.Services.AddScoped<ICharacterService, CharacterService>();
        builder.Services.AddScoped<ICharacterPaginationService, CharacterPaginationService>();
        builder.Services.AddScoped<ISearchService<Character>, SearchService<Character>>();

        // Add CORS services
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin() // Allow any origin
                       .AllowAnyMethod() // Allow any HTTP method
                       .AllowAnyHeader(); // Allow any header
            });
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        // Use CORS policy
        app.UseCors("AllowAllOrigins");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
