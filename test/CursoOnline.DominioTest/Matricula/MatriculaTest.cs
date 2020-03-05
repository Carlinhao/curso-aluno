using CurosOnline.Dominio;
using CurosOnline.Dominio._Base;
using CurosOnline.Dominio.Alunos;
using CursoOnline.Cursos;
using CursoOnline.Dominio._Builders;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Matricula
{
    public class MatriculaTest
    {
        [Fact(DisplayName = "Teste criando matricula")]
        [Trait("Categoria: ", "Matricula")]
        public void DeveCriarMatricula()
        {
            // Arrange
            var matriculaEsperada = new
            {
                Aluno = AlunoBuilder.Novo().Build(),
                Curso = CursoBuilder.Novo().Build(),
                ValorPago = 1000M
            };
            // Act
            var matricula = new Matricula(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorPago);

            // Assert
            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemAluno()
        {   
            // Arrange and Act
            Aluno alunoInvalido = null;

            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo()
                    .ComAluno(alunoInvalido)
                    .Build())
                .ComMensagem(Resource.AlunoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemCurso()
        {
            // Arrange and Act
            Curso cursoInvalido = null;

            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo()
                    .ComCurso(cursoInvalido)
                    .Build())
                .ComMensagem(Resource.CursoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoInvalido()
        {
            // Arrange and Act
            const decimal valorPagoInvalido = -10M;

            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo()
                    .ComValorPago(valorPagoInvalido)
                    .Build())
                .ComMensagem(Resource.ValorPagoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoMaiorQueValorDoCurso()
        {
            // Arrange and Act
            const decimal valorPagoInvalido = -10M;

            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo()
                    .ComValorPago(valorPagoInvalido)
                    .Build())
                .ComMensagem(Resource.ValorPagoInvalido);
        }
    }

    public class Matricula
    {
        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public decimal ValorPago { get; private set; }

        public Matricula(Aluno aluno, Curso curso, decimal valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(aluno == null, Resource.AlunoInvalido)
                .Quando(curso == null, Resource.CursoInvalido)
                .Quando(valor < 1, Resource.ValorPagoInvalido)
                .DispararExcecaoSeExistir();

            Aluno = aluno;
            Curso = curso;
            ValorPago = valor;
        }
    }
}
