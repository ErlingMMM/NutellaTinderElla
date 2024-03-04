using Microsoft.OpenApi.Models;
using WebMovieApi.Services.Characters;
using WebMovieApi.Services.Franchises;
using WebMovieApi.Services.Movies;
using WebMovieApi.Data.Models;
using WebMovieApi.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using AutoMapper;


namespace WebMovieApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Movies API",
                    Description = "API for managing the best of movies",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Erling & Ida",
                        Url = new Uri("https://example.com/contact")
                    }
                });
                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });


            builder.Services.AddDbContext<MovieDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            // Add our service
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IFranchiseService, FranchiseService>();
            builder.Services.AddScoped<ICharacterService, CharacterService>();
            // Add automapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAutoMapper(typeof(FranchiseService));
            builder.Services.AddAutoMapper(typeof(CharacterService));
            builder.Services.AddAutoMapper(typeof(MovieService));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                }
                );
                app.UseSwaggerUI(options =>
                {
                    app.UseSwaggerUI(c =>
                    {
                        c.RoutePrefix = "swagger";
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    });
                });
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}