using CurosOnline.Dominio._Base;

namespace CurosOnline.Dominio.Alunos
{
    public interface IAlunoRepositorio : IRepositorio<Aluno>
    {
        Aluno ObterPeloNome(string nome);
    }
}
