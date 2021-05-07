using CurosOnline.Dominio._Base;
using CurosOnline.Dominio.Alunos;
using CursoOnline.Cursos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Infrastructure.Contextos;
using CursoOnline.Infrastructure.Repositorios;
using CursoOnline.Ioc.SwaggerConfiguration;
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
            service.AddScoped(typeof(IAlunoRepositorio), typeof(AlunoRepositorio));
            service.AddScoped(typeof(IMatriculaRepositorio), typeof(MatriculaRepositorio));
            service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            service.AddScoped(typeof(IConversorDePublicoAlvo), typeof(ConversorDePublicoAlvo));
            service.AddScoped<ArmazenadorDeCurso>();
            service.AddScoped<ArmazenadorDeAluno>();
            service.AddScoped<CriacaoDaMatricula>();
            service.SwaggerServices();
        }
    }
}