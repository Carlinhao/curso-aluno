using CurosOnline.Dominio.Alunos;
using CursoOnline.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest.Matricula;
using System;

namespace CursoOnline.Dominio._Builders
{
    public class MatriculaBuilder
    {
        protected Aluno Aluno { get; set; }
        protected Curso Curso { get; set; }
        protected decimal ValorPago { get; set; }

        public static MatriculaBuilder Novo()
        {
            return new MatriculaBuilder
            {
                Aluno = AlunoBuilder.Novo().Build(),
                Curso = CursoBuilder.Novo().Build(),
                ValorPago = 1000M
            };
        }

        public MatriculaBuilder ComAluno(Aluno aluno)
        {
            Aluno = aluno;
            return this;
        }

        public MatriculaBuilder ComCurso(Curso curso)
        {
            Curso = curso;
            return this;
        }

        public MatriculaBuilder ComValorPago(decimal valor)
        {
            ValorPago = valor;
            return this;
        }

        public Matricula Build()
        {
            var curso = new Matricula(Aluno, Curso, ValorPago);
            return new Matricula(Aluno, Curso, ValorPago);
        }
    }
}