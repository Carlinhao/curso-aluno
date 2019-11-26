using CurosOnline.Dominio._Base;
using CursoOnline.Cursos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Infrastructure.Contextos;
using CursoOnline.Infrastructure.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CursoOnline.Ioc
{
    public static class StartUpIoc
    {
        public static void ConfigureServices(IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration["ConnectionString"]));
            service.AddScoped(typeof(IRepositorio<>), typeof(RepositorioBase<>));
            service.AddScoped(typeof(ICursoRepositorio), typeof(CursoRepositorio));
            service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            service.AddScoped<ArmazenadorDeCurso>();
        }
    }
}