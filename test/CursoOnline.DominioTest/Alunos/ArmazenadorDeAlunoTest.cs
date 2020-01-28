using Bogus;
using CurosOnline.Dominio.Alunos;
using CursoOnline.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Alunos
{
    public class ArmazenadorDeAlunoTest
    {
        private readonly Faker _faker;
        private readonly AlunoDto _alunoDto;
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;
        private readonly Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvoMock;

        public ArmazenadorDeAlunoTest()
        {
            _faker = new Faker();

            _alunoDto = new AlunoDto
            {
                Cpf = GeradorCpf.GerarCpf(),
                Email = _faker.Person.Email,
                Nome = _faker.Person.FullName,
                PublicoAlvo = PublicoAlvo.Universitario.ToString()
            };
            _alunoRepositorioMock = new Mock<IAlunoRepositorio>();
            _conversorDePublicoAlvoMock = new Mock<IConversorDePublicoAlvo>(); 
            
            _armazenadorDeAluno = new ArmazenadorDeAluno(_alunoRepositorioMock.Object, _conversorDePublicoAlvoMock.Object);
        }

        [Fact]
        public void DeveAdicionarAlunoTest()
        {
            _armazenadorDeAluno.ArmazenarALuno(_alunoDto);
            _alunoRepositorioMock.Verify(x => x.Adicionar(
                It.Is<Aluno>(z => z.Nome == _alunoDto.Nome)));
        }

        [Fact]
        public void DeveEditarAluno()
        {
            _alunoDto.Id = 35;
            _alunoDto.Nome = _faker.Person.FullName;
            var alunoJaSalvo = AlunoBuilder.Novo().Build();

            _alunoRepositorioMock.Setup(x => x.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);
            
            _armazenadorDeAluno.ArmazenarALuno(_alunoDto);
            
            Assert.Equal(_alunoDto.Nome, alunoJaSalvo.Nome);
        }

        [Fact]
        public void NadDeveEditarCpfAluno()
        {
            
        }
    }
}
