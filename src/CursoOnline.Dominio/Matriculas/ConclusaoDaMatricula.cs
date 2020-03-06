using CurosOnline.Dominio;
using CurosOnline.Dominio._Base;
using System;

namespace CursoOnline.Dominio.Matriculas
{
    public class ConclusaoDaMatricula
    {
        private readonly IMatriculaRepositorio _matriculaRepositorio;

        public ConclusaoDaMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            _matriculaRepositorio = matriculaRepositorio;
        }

        public void ConcluirMatricula(int idMatricula, double notaDoAlunoEsperado)
        {
            var matricula = _matriculaRepositorio.ObterPorId(idMatricula);

            ValidadorDeRegra.Novo()
                .Quando(matricula == null, Resource.MatriculaNaoEncontrada)
                .DispararExcecaoSeExistir();

            matricula.InformarNota(notaDoAlunoEsperado);
        }
    }
}
