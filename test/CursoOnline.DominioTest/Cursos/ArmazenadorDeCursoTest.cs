using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        [Fact]
        public void DeveAdicionarCurso()
        {
            var cursoDto = new CursoDto()
            {
                Nome = "Curso A",
                CargaHoraria = 80,
                PublicoAlvoId = 1,
                ValorDoCurso = 950.00
            };

            var cursoRepositorioMock = new Mock<ICursoRepositorio>();
            
            var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRepositorioMock.Object);
            
            armazenadorDeCurso.Armazenar(cursoDto);
            
            cursoRepositorioMock.Verify(r => r.Adicionar(It.Is<Curso>(x => x.Nome == cursoDto.Nome)));
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
            var curso = 
                new Curso(cursoDto.Nome, cursoDto.CargaHoraria, PublicoAlvo.Estudantes, cursoDto.ValorDoCurso);
            _cursoRepositorio.Adicionar(curso);
        }
    }
    
    public class CursoDto
    {
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public int PublicoAlvoId { get; set; }
        public double ValorDoCurso { get; set; }
    }
}