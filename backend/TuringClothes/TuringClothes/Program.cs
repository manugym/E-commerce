
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.ML;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TuringClothes.Database;
using TuringClothes.Pagination;

using TuringClothes.Repository;
using TuringClothes.Services;

namespace TuringClothes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    Description = "Escribe **_SOLO_** tu token JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>(true, JwtBearerDefaults.AuthenticationScheme);
            });

            builder.Services.AddAuthentication().AddJwtBearer(options =>
            {
                string key = "bdisub678kji@m32iods/3bjk970kjbhvytdtjvñkpokop";
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
            builder.Services.AddScoped<AuthRepository>();
            builder.Services.AddScoped<ProductRepository>();
            builder.Services.AddScoped<CartRepository>();
            builder.Services.AddScoped<CatalogService>();
            builder.Services.AddScoped<SmartSearchService>();
            builder.Services.AddScoped<ReviewService>();
            builder.Services.AddScoped<IUnitofWork, UnitOfWork>();
            


            builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>()
                .FromFile("TuringModel1.mlnet");

            if (builder.Environment.IsDevelopment())
            {
                //Permite CORS
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
                });
            }
            var app = builder.Build();

            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) { 
                app.UseSwagger();
                app.UseSwaggerUI();
                //permite CORS
                app.UseCors();
                //rellena la base de datos con DataSeed
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
