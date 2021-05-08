using CurosOnline.Dominio;
using CursoOnline.Dominio._Builders;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matricula
{
    public class ConclusaoDaMatriculaTest
    {
        private Mock<IMatriculaRepositorio> _matriculaRepositorio;
        private ConclusaoDaMatricula _conclusaoDaMatricula;

        public ConclusaoDaMatriculaTest()
        {
            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();
            _conclusaoDaMatricula = new ConclusaoDaMatricula(_matriculaRepositorio.Object);
        }

        [Fact(DisplayName = "Informando nota do aluno")]
        [Trait("Categoria: ", "ConclusaoMatricula")]
        public void DeveInformarNotaAluno()
        {
            // Arrange
            var notaDoAlunoEsperado = 8;
            var matricula = MatriculaBuilder.Novo().Build();            
            _matriculaRepositorio.Setup(r => r.ObterPorId(matricula.Id)).Returns(matricula);          

            // Act
            _conclusaoDaMatricula.ConcluirMatricula(matricula.Id, notaDoAlunoEsperado);

            // Assert
            Assert.Equal(notaDoAlunoEsperado, matricula.NotaDoAluno);
        }

        [Fact(DisplayName = "Exbir mensagem quando matricula não for encontrada")]
        [Trait("Categoria: ", "ConclusaoMatricula")]
        public void DeveExibirMensagemQuandoMatriculaNaoForEncontrada()
        {
            // Arrange
            Dominio.Matriculas.Matricula matriculaInvalida = null;
            const int idMatriculaInvalida = 1;
            const double notaAluno = 0.1;

            // Act
            _matriculaRepositorio.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);


            // Assert
            Assert.Throws<ExcecaoDeDominio>(() => _conclusaoDaMatricula.ConcluirMatricula(idMatriculaInvalida, notaAluno))
                .ComMensagem(Resource.MatriculaNaoEncontrada);
        }
    }
}
