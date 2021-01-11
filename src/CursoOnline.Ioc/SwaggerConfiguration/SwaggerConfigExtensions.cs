using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace CursoOnline.Ioc.SwaggerConfiguration
{
    public static class SwaggerConfigExtensions
    {
        public static void SwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "v1",
                        Version = "v1",
                        Description = "Curso Online",
                        Contact = new OpenApiContact
                        {
                            Name = "Carlos Silva",
                            Url = new Uri("https://github.com/Carlinhao")
                        }
                    });
            });
        }

        public static void SwaggerConfigure(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
