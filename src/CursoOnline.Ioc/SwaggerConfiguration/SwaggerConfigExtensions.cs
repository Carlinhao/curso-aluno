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
                        Title = "Curso Online",
                        Version = "v1",
                        Description = "Gerenciamento de curso e matricula de aluno.",
                        Contact = new OpenApiContact
                        {
                            Name = "Carlos Silva",
                            Url = new Uri("https://github.com/Carlinhao/curso-aluno")
                        }
                    });
            });
        }

        public static void SwaggerConfigure(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso Online v1");
            });
        }
    }
}
