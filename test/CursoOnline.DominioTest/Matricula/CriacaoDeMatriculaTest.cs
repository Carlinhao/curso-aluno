using CurosOnline.Dominio;
using CurosOnline.Dominio._Base;
using CurosOnline.Dominio.Alunos;
using CurosOnline.Dominio.Matriculas;
using CursoOnline.Cursos;
using CursoOnline.Dominio._Builders;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matricula
{
    public class CriacaoDeMatriculaTest
    {
        private readonly Aluno _aluno;
        private readonly Curso _curso;
        private readonly Mock<ICursoRepositorio> _cursoRepositorio;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorio;
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;
        private readonly CriacaoDaMatricula _criacaoDaMatricula;
        private readonly MatriculaDto _matriculaDto;

        public CriacaoDeMatriculaTest()
        {
            _cursoRepositorio = new Mock<ICursoRepositorio>();
            _alunoRepositorio = new Mock<IAlunoRepositorio>();
            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();
            _criacaoDaMatricula = new CriacaoDaMatricula(_alunoRepositorio.Object, _cursoRepositorio.Object, _matriculaRepositorio.Object);

            _curso = CursoBuilder.Novo().ComId(45).ComPublicoAlvo(PublicoAlvo.Empreendedor).Build();
            _cursoRepositorio.Setup(x => x.ObterPorId(_curso.Id)).Returns(_curso);

            _aluno = AlunoBuilder.Novo().ComId(23).ComPublicoAlvo(PublicoAlvo.Estudante).Build();
            _alunoRepositorio.Setup(x => x.ObterPorId(_aluno.Id)).Returns(_aluno);

            _matriculaDto = new MatriculaDto(_aluno.Id, _curso.Id);
        }
        
        [Fact(DisplayName = "Exibir mensagem quando curso não existir")]
        [Trait("Category: ", "Matricula")]
        public void DeveExibirMensagemQuandoCursoNaoForEncontrado()
        {
            // Arrange
            Curso cursoInvalido = null;

            // Act
            _cursoRepositorio.Setup(r => r.ObterPorId(_matriculaDto.CursoId)).Returns(cursoInvalido);

            // Assert
            Assert.Throws<ExcecaoDeDominio>(() => _criacaoDaMatricula.Criar(_matriculaDto))
                .ComMensagem(Resource.CursoNaoEncontrado);
        }

        [Fact(DisplayName = "Exibir mensagem quando não existir aluno")]
        [Trait("Category: ", "Matricula")]
        public void DeveExibirMensagemQuandoAlunoNaoForEncontrado()
        {
            // Arrange
            Aluno alunoInvalido = null;

            // Act
            _alunoRepositorio.Setup(r => r.ObterPorId(_matriculaDto.AlunoId)).Returns(alunoInvalido);

            // Assert
            Assert.Throws<ExcecaoDeDominio>(() => _criacaoDaMatricula.Criar(_matriculaDto))
                .ComMensagem(Resource.AlunoNaoEncontrado);
        }

        [Fact(DisplayName = "Criando a matricula")]
        [Trait("Category: ", "Matricula")]
        public void DeveCriarMatricula()
        {
            // Arrange
            _criacaoDaMatricula.Criar(_matriculaDto);

            _matriculaRepositorio.Verify(x => x.Adicionar(It.Is<CursoOnline.Dominio.Matriculas.Matricula>(m => m.Aluno == _aluno)));
        }
    }

    public class CriacaoDaMatricula
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly ICursoRepositorio _cursoRepositorio;
        private readonly IMatriculaRepositorio _matriculaRepositorio;

        public CriacaoDaMatricula(IAlunoRepositorio alunoRepositorio, ICursoRepositorio cursoRepositorio, IMatriculaRepositorio matriculaRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
            _cursoRepositorio = cursoRepositorio;
            _matriculaRepositorio = matriculaRepositorio;
        }

        public void Criar(MatriculaDto matriculaDto)
        {
            var curso = _cursoRepositorio.ObterPorId(matriculaDto.CursoId);
            var aluno = _alunoRepositorio.ObterPorId(matriculaDto.AlunoId);

            ValidadorDeRegra.Novo()
                .Quando(curso == null, Resource.CursoNaoEncontrado)
                .Quando(aluno == null, Resource.AlunoNaoEncontrado)
                .DispararExcecaoSeExistir();
        }
    }
}