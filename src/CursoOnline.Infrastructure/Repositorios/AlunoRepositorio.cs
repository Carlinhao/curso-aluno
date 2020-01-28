using System.Linq;
using CurosOnline.Dominio.Alunos;
using CursoOnline.Infrastructure.Contextos;

namespace CursoOnline.Infrastructure.Repositorios
{
    public class AlunoRepositorio : RepositorioBase<Aluno>, IAlunoRepositorio
    {
        public AlunoRepositorio(ApplicationDbContext context)
            : base(context)
        {
        }

        public Aluno ObterPeloNome(string nome)
        {
            var entidades = Context.Set<Aluno>().Where(x => x.Nome == nome);
            return entidades.Any() ? entidades.First() : null;
        }
    }
}