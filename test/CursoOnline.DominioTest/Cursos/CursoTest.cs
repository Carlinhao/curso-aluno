using System;
using Bogus;
using CursoOnline.Cursos;
using CursoOnline.Dominio._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTest : IDisposable
    {
        public CursoTest(ITestOutputHelper output)
        {
            var fake = new Faker();
            _output = output;
            _output.WriteLine("Construtor executado!!!");

            _nome = fake.Random.Word();
            _cargaHoraria = fake.Random.Double(50, 100);
            _publicoAlvo = PublicoAlvo.Estudantes;
            _valorDoCurso = fake.Random.Double(150, 1000);
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado!!!");
        }

        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valorDoCurso;

        [Theory(DisplayName = "Curso com nome invalido")]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerNomeInvalido(string nome)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo()
                    .ComNome(nome)
                    .Build())
                .ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(0.99)]
        public void NaoDeveCursoTerCargaHorariaMenorQueUm(double cargaHoraria)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo()
                    .ComCargaHoraria(cargaHoraria)
                    .Build())
                .ComMensagem("Carga horária menor que 1");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(0.99)]
        public void NaoDeveCursoTerValorMenorQueUm(double valor)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo()
                    .ComValor(valor)
                    .Build())
                .ComMensagem("Valor curso menor que 1");
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
                ValorDoCurso = _valorDoCurso
            };

            //Act
            var curso = new Curso(cursoSelecionado.Nome, cursoSelecionado.CargaHoraria, cursoSelecionado.PublicoAlvo,
                cursoSelecionado.ValorDoCurso);

            //Assert
            cursoSelecionado.ToExpectedObject().ShouldMatch(curso);
        }
    }
}