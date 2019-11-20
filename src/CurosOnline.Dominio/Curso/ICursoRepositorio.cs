using CurosOnline.Dominio._Base;

namespace CursoOnline.Cursos
{
    public interface ICursoRepositorio : IRepositorio<Curso>
    {
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}