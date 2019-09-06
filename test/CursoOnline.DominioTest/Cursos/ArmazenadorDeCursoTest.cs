using Bogus;
using CursoOnline.DominioTest._Util;
using Moq;
using System;
using CursoOnline.DominioTest._Builders;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;

        public ArmazenadorDeCursoTest()
        {
            var faker = new Faker();
            _cursoDto = new CursoDto()
            {
                Nome = faker.Random.Words(),
                CargaHoraria = faker.Random.Double(50, 100),
                PublicoAlvo = "Estudantes",
                ValorDoCurso = faker.Random.Double(100, 950)
            };
            
            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }
        
        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);
            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(x => x.Nome == _cursoDto.Nome)));
        }

        [Fact]
        public void NaoDeveInformarPublicAlvoInvalido()
        {
            _cursoDto.PublicoAlvo =  "Medico";
            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                                                    .ComMensagem("Publico Alvo invalido");
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(x => x.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);
            
            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Nome do curso já consta no banco de dados.");
        }
    }
}