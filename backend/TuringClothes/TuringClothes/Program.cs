
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TuringClothes.Database;
using TuringClothes.Mapper;
using TuringClothes.Repository;

namespace TuringClothes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAuthentication().AddJwtBearer(options =>
            {
                string key = "bdisub678kji@miods/3bjk970kjbhvytdtjvñkpokop";
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

            builder.Services.AddSingleton(provider =>
            provider.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>().Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters
             );

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            

            builder.Services.AddScoped<MyDatabase>();
            builder.Services.AddScoped<AuthMapper>();
            builder.Services.AddScoped<AuthRepository>();

            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                SeedDatabase(app.Services);
            }
            
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            //crea la base de datos si no está ya creada
            using (IServiceScope scope = app.Services.CreateScope())
            {
                
                MyDatabase myDatabase = scope.ServiceProvider.GetService<MyDatabase>();
                myDatabase.Database.EnsureCreated();
                
            }

            static void SeedDatabase(IServiceProvider serviceProvider)
            {
                using IServiceScope scope = serviceProvider.CreateScope();
                using MyDatabase dataBaseContext = scope.ServiceProvider.GetService<MyDatabase>();

                if (dataBaseContext.Database.EnsureCreated())
                {
                    DataSeed seeder = new DataSeed(dataBaseContext);
                    seeder.Seed();
                }
            }

            app.Run();
        }
    }
}
