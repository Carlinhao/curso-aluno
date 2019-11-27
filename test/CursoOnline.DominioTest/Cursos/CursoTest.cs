using Bogus;
using CurosOnline.Dominio;
using CursoOnline.Cursos;
using CursoOnline.Dominio._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTest
    {
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valorDoCurso;
        private readonly string _descricao;

        public CursoTest()
        {
            var fake = new Faker();

            _nome = fake.Random.Word();
            _cargaHoraria = fake.Random.Double(50, 100);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valorDoCurso = fake.Random.Double(150, 1000);
            _descricao = fake.Lorem.Paragraph();
        }
               
        [Fact]
        public void CriarCurso()
        {
            //Arrange
            var cursoSelecionado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valorDoCurso,
                Descricao = _descricao
            };

            //Act
            var curso = new Curso(cursoSelecionado.Nome, cursoSelecionado.CargaHoraria, cursoSelecionado.PublicoAlvo,
                cursoSelecionado.Valor, cursoSelecionado.Descricao);

            //Assert
            cursoSelecionado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory(DisplayName = "Curso com nome invalido")]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerNomeInvalido(string nome)
        {
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo()
                    .ComNome(nome)
                    .Build())
                .ComMensagem(Resource.NomeInvalido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(0.99)]
        public void NaoDeveCursoTerCargaHorariaMenorQueUm(double cargaHoraria)
        {
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo()
                    .ComCargaHoraria(cargaHoraria)
                    .Build())
                .ComMensagem(Resource.CargaHorariaInvalida);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(0.99)]
        public void NaoDeveCursoTerValorMenorQueUm(double valorInvalido)
        {
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo()
                    .ComValor(valorInvalido)
                    .Build())
                .ComMensagem(Resource.ValorCursoInvalido);
        }
        
        [Fact]
        public void DeveEditarNomeCursoTest()
        {
            var nomeCurso = "José";
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarNome(nomeCurso);

            Assert.Equal(nomeCurso, curso.Nome);
        }

        [Theory(DisplayName = "Curso com nome invalido")]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalidoTest(string nomeInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarNome(nomeInvalido))
                .ComMensagem(Resource.NomeInvalido);
        }

        [Fact]
        public void DeveEditarCargaHorariaDoCursoTest()
        {
            var cargaHoraria = 50.5;
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarCargaHoraria(cargaHoraria);

            Assert.Equal(cargaHoraria, curso.CargaHoraria);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(0.99)]
        public void NaoDeveEditarCargaHorariaMenorQueUmTest(double cargaHoraria)
        {
            var cursoCargaHoraria = CursoBuilder.Novo().Build();

            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => cursoCargaHoraria.AlterarCargaHoraria(cargaHoraria))
                .ComMensagem(Resource.CargaHorariaInvalida);
        }

        [Fact]
        public void DeveAlterarValorCursoTest()
        {

            var valorCurso = 50.5;
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarValor(valorCurso);

            Assert.Equal(valorCurso, curso.Valor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(0.99)]
        public void NaoDeveAlterarComValorInvalidoCursoTest(double valor)
        {

            var cursoValor = CursoBuilder.Novo().Build();

            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => cursoValor.AlterarValor(valor))
                .ComMensagem(Resource.ValorCursoInvalido);
        }
    }
}