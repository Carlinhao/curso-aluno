using CurosOnline.Dominio;
using CurosOnline.Dominio._Base;
using CurosOnline.Dominio.Alunos;
using CursoOnline.Cursos;
using System;

namespace CursoOnline.Dominio.Matriculas
{
    public class Matricula : Entidade
    {
        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public decimal ValorPago { get; private set; }
        public bool PossuiDesconto { get; private set; }
        public double NotaDoAluno { get; set; }
        public bool CursoConcluido { get; set; }
        public bool Cancelada { get; set; }

        public Matricula() { }

        public Matricula(Aluno aluno, Curso curso, decimal valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(aluno == null, Resource.AlunoInvalido)
                .Quando(curso == null, Resource.CursoInvalido)
                .Quando(valor < 1, Resource.ValorPagoInvalido)
                .Quando(curso != null && valor > curso.Valor, Resource.ValorPagoMaiorQueValorDoCurso)
                .Quando(curso != null && aluno != null && aluno.PublicoAlvo != curso.PublicoAlvo, Resource.PublicoAlvoDiferente)
                .DispararExcecaoSeExistir();

            Aluno = aluno;
            Curso = curso;
            ValorPago = valor;
            PossuiDesconto = valor < curso.Valor;
        }

        public void InformarNota(double notaEsperada)
        {
            ValidadorDeRegra.Novo()
               .Quando(notaEsperada < 0 ||notaEsperada > 10, Resource.NotadoAlunoEhInvalida).DispararExcecaoSeExistir();
            NotaDoAluno = notaEsperada;
            CursoConcluido = true;
        }

        public void Cancelar()
        {
            ValidadorDeRegra.Novo()
                .Quando(CursoConcluido, Resource.MatriculaConcluida)
                .DispararExcecaoSeExistir();

            Cancelada = true;
        }
    }
}