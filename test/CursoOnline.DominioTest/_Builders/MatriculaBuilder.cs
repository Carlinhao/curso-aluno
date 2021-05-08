using CurosOnline.Dominio.Alunos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Cursos;
using CursoOnline.DominioTest._Builders;
using System;

namespace CursoOnline.Dominio._Builders
{
    public class MatriculaBuilder
    {
        protected Aluno Aluno;
        protected Curso Curso;
        protected decimal ValorPago;
        protected double NotaDoAluno;

        public static MatriculaBuilder Novo()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Build();
            return new MatriculaBuilder
            {
                Aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Build(),
                Curso = curso,
                ValorPago = Convert.ToDecimal(curso.Valor)
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

        public MatriculaBuilder ComNotaDoAluno(double notaDoAluno)
        {
            NotaDoAluno = notaDoAluno;
            return this;
        }

        public Matricula Build()
        {
            return new Matricula(Aluno, Curso, ValorPago);
        }
    }
}