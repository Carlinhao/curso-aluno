using CurosOnline.Dominio._Base;

namespace CursoOnline.Cursos
{
    public interface ICursoRepositorio : IRepositorio<Curso>
    {
        Curso ObterPeloNome(string nome);
    }
}