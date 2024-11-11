using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO.Compression;
using System.Security.Claims;
using System.Text;
using ws_api_fundades_Entity.Examples;
using ws_api_fundades_Services.Services;
using ws_api_fundades_Services.ServicesImpl;

namespace ws_api_fundades.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddBusiness(this IServiceCollection pIServiceCollection)
        {
            pIServiceCollection.AddTransient<ILogService, LogServiceImpl>();
            pIServiceCollection.AddTransient<IAuthService, AuthServiceImpl>();
            pIServiceCollection.AddTransient<IUserService, UserServiceImpl>();
            pIServiceCollection.AddTransient<ICuestionarioService, CuestionarioServiceImpl>();

            //Custom Swagger
            pIServiceCollection.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerCustomExtension>();
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de servicios de la API
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            // Configuración de swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API de Fundades",
                    Version = "v1",
                    Description = "Esta api esta destinada a registar informacion de postulatnes de trabajo.",
                    Contact = new OpenApiContact
                    {
                        Name = "Fundades",
                        Email = "fundades@fundades.org",
                        Url =  new Uri("https://fundades.org/")
                    }
                });

                // Ruta del archivo XML con comentarios
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf<EjemplosSwagger>();

            // Añadir la autenticación JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!)),
                    RoleClaimType = ClaimTypes.Role
                };
            });

            // Configuración del servicio de compresión
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal; // Puedes usar Fastest, NoCompression, o Optimal
            });


            // Otros servicios
        }

        public static void ConfigureMiddleware(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configuración
            app.UseStaticFiles();

            // Usar COMPRESION GZIP
            app.UseResponseCompression();

            // Configurar CORS
            app.UseCors(c =>
            {
                c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
            });


            // Configurar SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string path = "";
                if (!env.IsDevelopment())
                {
                    path = "/apifundades";
                }

                c.SwaggerEndpoint($"{path}/swagger/v1/swagger.json", "API de Fundades v1");
                c.InjectStylesheet($"{path}/swagger/swagger-custom.css");
                c.InjectJavascript($"{path}/swagger/swagger-custom.js");

                c.DocumentTitle = "ApiFundades";
                c.RoutePrefix = "servicios";

            });
            

            //OTROS
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
