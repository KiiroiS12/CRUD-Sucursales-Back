using API.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddControllers();

            //Documentación API
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Prueba Técnica API",
                        Description = "Backend Prueba Técnica",
                        TermsOfService = new Uri("https://example.com/contact"),
                        Contact = new OpenApiContact
                        {
                            Name = "Leimar Nazarit",
                            Url = new Uri("https://example.com/contact")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "License",
                            Url = new Uri("https://example.com/contact")
                        }
                    });
            });

            services.AddEndpointsApiExplorer();

            //Database mapping, in profiles on configuration
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("database")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsApi");

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
