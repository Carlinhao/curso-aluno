using CurosOnline.Dominio.Alunos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Cursos;
using CursoOnline.DominioTest._Builders;

namespace CursoOnline.Dominio._Builders
{
    public class MatriculaBuilder
    {
        protected Aluno Aluno;
        protected Curso Curso;
        protected decimal ValorPago;

        public static MatriculaBuilder Novo()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Build();
            return new MatriculaBuilder
            {
                Aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Build(),
                Curso = curso,
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