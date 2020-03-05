using CurosOnline.Dominio;
using CurosOnline.Dominio._Base;
using CurosOnline.Dominio.Alunos;
using CurosOnline.Dominio.Matriculas;
using CursoOnline.Cursos;
using CursoOnline.Dominio._Builders;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matricula
{
    public class CriacaoDeMatriculaTest
    {
        private Aluno aluno;
        private Curso curso;
        private readonly Mock<ICursoRepositorio> _cursoRepositorio;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorio;
        private readonly CriacaoDaMatricula criacaoDaMatricula;
        
        public CriacaoDeMatriculaTest()
        {
            _cursoRepositorio = new Mock<ICursoRepositorio>();
            _alunoRepositorio = new Mock<IAlunoRepositorio>();
            criacaoDaMatricula = new CriacaoDaMatricula(_alunoRepositorio.Object, _cursoRepositorio.Object);
        }
        
        [Fact(DisplayName = "Validando Aluno e Curso com publico Alvo diferente")]
        [Trait("Category: ", "Matricula")]
        public void NaoDeveCursoEAlunoTerPublicoAlvoDiferentes()
        {
            // Arrange
            curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Build();
            aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Estudante).Build();
            

            _cursoRepositorio.Setup(x => x.ObterPorId(curso.Id)).Returns(curso);
            _alunoRepositorio.Setup(x => x.ObterPorId(aluno.Id)).Returns(aluno);
            
            var matriculaDto = new MatriculaDto(aluno.Id, curso.Id);
            

            Assert.Throws<ExcecaoDeDominio>(() => criacaoDaMatricula.Criar(matriculaDto))
                .ComMensagem(Resource.PublicoAlvoDiferente);
        }
    }

    public class CriacaoDaMatricula
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly ICursoRepositorio _cursoRepositorio;

        public CriacaoDaMatricula(IAlunoRepositorio alunoRepositorio, ICursoRepositorio cursoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
            _cursoRepositorio = cursoRepositorio;
        }

        public void Criar(MatriculaDto matriculaDto)
        {
            var curso = _cursoRepositorio.ObterPorId(matriculaDto.CursoId);
            var aluno = _alunoRepositorio.ObterPorId(matriculaDto.AlunoId);
            ValidadorDeRegra.Novo()
                .Quando(aluno.PublicoAlvo != curso.PublicoAlvo, Resource.PublicoAlvoDiferente)
                .DispararExcecaoSeExistir();
            throw new System.NotImplementedException();
        }
    }
}