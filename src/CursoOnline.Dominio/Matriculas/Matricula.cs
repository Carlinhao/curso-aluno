using CurosOnline.Dominio;
using CurosOnline.Dominio._Base;
using CurosOnline.Dominio.Alunos;
using CursoOnline.Cursos;
using System;

namespace CursoOnline.Dominio.Matriculas
{
    public class Matricula
    {
        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public decimal ValorPago { get; private set; }
        public bool PossuiDesconto { get; private set; }

        public Matricula(Aluno aluno, Curso curso, decimal valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(aluno == null, Resource.AlunoInvalido)
                .Quando(curso == null, Resource.CursoInvalido)
                .Quando(valor < 1, Resource.ValorPagoInvalido)
                .Quando(curso != null && valor > Convert.ToDecimal(curso.Valor), Resource.ValorPagoMaiorQueValorDoCurso)
                .Quando(curso != null && aluno != null && aluno.PublicoAlvo != curso.PublicoAlvo, Resource.PublicoAlvoDiferente)
                .DispararExcecaoSeExistir();

            Aluno = aluno;
            Curso = curso;
            ValorPago = valor;
            PossuiDesconto = valor < Convert.ToDecimal(curso.Valor);
        }
    }
}