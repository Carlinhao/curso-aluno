using CurosOnline.Dominio;
using CurosOnline.Dominio.Alunos;
using CursoOnline.Cursos;
using CursoOnline.Dominio._Builders;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.DominioTest.Matricula
{
    public class MatriculaTest
    {
        [Fact(DisplayName = "Criando matricula")]
        [Trait("Categoria: ", "Matricula")]
        public void DeveCriarMatricula()
        {
            // Arrange
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Build();
            var matriculaEsperada = new
            {
                Aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Build(),
                Curso = curso,
                ValorPago = Convert.ToDecimal(curso.Valor)
            };

            // Act
            var matricula = new CursoOnline.Dominio.Matriculas.Matricula(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorPago);

            // Assert
            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }

        [Fact(DisplayName = "Criando matricula sem aluno")]
        [Trait("Categoria: ", "Matricula")]
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

        [Fact(DisplayName = "Criando matricula sem curso")]
        [Trait("Categoria: ", "Matricula")]
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

        [Fact(DisplayName = "Criando matricula com pagamento inválido")]
        [Trait("Categoria: ", "Matricula")]
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

        [Fact(DisplayName = "Criando matricula com pagamento maior que valor do curso")]
        [Trait("Categoria: ", "Matricula")]
        public void NaoDeveCriarMatriculaComValorPagoMaiorQueValorDoCurso()
        {
            // Arrange
            var curso = CursoBuilder.Novo().ComValor(100).Build();
            decimal valorMaiorQueCurso = Convert.ToDecimal(curso.Valor + 1);

            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo().ComCurso(curso)
                    .ComValorPago(valorMaiorQueCurso)
                    .Build())
                .ComMensagem(Resource.ValorPagoMaiorQueValorDoCurso);
        }

        [Fact(DisplayName = "Validando Desconto na Matrícula")]
        [Trait("Categoria: ", "Matricula")]
        public void DeveIndicarDescontaNaMatricula()
        {
            // Arrange
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).ComValor(100).Build();
            decimal valorComDesconto = Convert.ToDecimal(curso.Valor - 1);

            var matriculaComDesconto = MatriculaBuilder.Novo().ComCurso(curso)
                    .ComValorPago(valorComDesconto)
                    .Build();

            Assert.True(matriculaComDesconto.PossuiDesconto);
        }
        
        [Fact(DisplayName = "Validando Aluno e Curso com publico Alvo diferente")]
        [Trait("Category: ", "Matricula")]
        public void NaoDeveCursoEAlunoTerPublicoAlvoDiferentes()
        {
            // Arrange
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Build();
            var aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Estudante).Build();
            

            Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo().ComAluno(aluno).ComCurso(curso).Build())
                .ComMensagem(Resource.PublicoAlvoDiferente);
        }
    }
}
