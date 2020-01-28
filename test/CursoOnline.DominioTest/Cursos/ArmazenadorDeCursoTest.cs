using Bogus;
using CurosOnline.Dominio;
using CursoOnline.Cursos;
using CursoOnline.Dominio._Builders;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvoMock;

        public ArmazenadorDeCursoTest()
        {
            var faker = new Faker();
            _cursoDto = new CursoDto
            {
                Nome = faker.Random.Words(),
                CargaHoraria = faker.Random.Double(50, 100),
                PublicoAlvo = "Estudante",
                Valor = faker.Random.Double(100, 950)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _conversorDePublicoAlvoMock = new Mock<IConversorDePublicoAlvo>(); 
            
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object, _conversorDePublicoAlvoMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);
            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(x => x.Nome == _cursoDto.Nome)));
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComId(432).ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(x => x.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Nome do curso já consta no banco de dados.");
        }

        [Fact]
        public void DeveAltarDadosCursoTest()
        {
            _cursoDto.Id = 23;
            var curso = CursoBuilder.Novo().Build();

            _cursoRepositorioMock.Setup(a => a.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Armazenar(_cursoDto);

            Assert.Equal(_cursoDto.Nome, curso.Nome);
            Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
            Assert.Equal(_cursoDto.Valor, curso.Valor);
        }

        [Fact]
        public void NaoDeveSalvarMesmoIdAoEditarCurso()
        {
            _cursoDto.Id = 23;
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorioMock.Setup(a => a.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()), Times.Never);
        }
    }
}