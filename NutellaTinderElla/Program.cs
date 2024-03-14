using Microsoft.OpenApi.Models;
using NutellaTinderElla.Services.UserData;
using NutellaTinderEllaApi.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NutellaTinderElla.Services.Matching;
using NutellaTinderElla.Services.Messaging;

namespace NutellaTinderEllaApi
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
                    Title = "Cruella Nutella Tinder Ella API",
                    Description = "API for the best dating app. Gender.0 = Man, Gender.1 = Woman, Gender.2 = Nonbinary, Seeking.0 = Friendship, Seeking.1 = Relationship, Seeking.2 = Not sure, Seeking.3 = Casual, GenderPreference.0 = Man, GenderPreference.1 = Women, GenderPreference.2 = All, GenderPreference.3 = NonBinary",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Tindeeeer",
                        Url = new Uri("https://example.com/contact")
                    }
                });
                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });


            builder.Services.AddDbContext<TinderDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            // Add our service

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILikeService, LikeService>();
            builder.Services.AddScoped<ISwipeService, SwipeService>();
            builder.Services.AddScoped<IMatchService, MatchService>();
            builder.Services.AddScoped<IMessageService, MessageService>();

            // Add automapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddAutoMapper(typeof(UserService));


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