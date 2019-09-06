using Bogus;
using CursoOnline.DominioTest._Util;
using Moq;
using System;
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
                PublicoAlvo = "Estudante",
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
            _cursoDto.PublicoAlvo = "Médico";

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                                                    .ComMensagem("Publico Alvo inválido");
        }
    }
    
    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
    }
    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            Enum.TryParse(typeof(PublicoAlvo), cursoDto.PublicoAlvo, out var publicoAlvo);

            if (publicoAlvo == null)
                throw new ArgumentException("Publico Alvo inválido");
            var curso = 
                new Curso(cursoDto.Nome, cursoDto.CargaHoraria, (PublicoAlvo)publicoAlvo, cursoDto.ValorDoCurso);
            _cursoRepositorio.Adicionar(curso);
        }
    }
    
    public class CursoDto
    {
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double ValorDoCurso { get; set; }
    }
}