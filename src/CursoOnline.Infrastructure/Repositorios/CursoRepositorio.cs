using System.Linq;
using CursoOnline.Cursos;
using CursoOnline.Infrastructure.Contextos;

namespace CursoOnline.Infrastructure.Repositorios
{
    public class CursoRepositorio : RepositorioBase<Curso>, ICursoRepositorio
    {
        public CursoRepositorio(ApplicationDbContext context) 
            : base(context)
        {
        }

        public Curso ObterPeloNome(string nome)
        {
            var entidades = Context.Set<Curso>().Where(x => x.Nome == nome);
            return entidades.Any() ? entidades.First() : null;
        }
    }
}