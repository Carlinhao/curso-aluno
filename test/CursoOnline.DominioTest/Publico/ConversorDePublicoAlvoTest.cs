using CurosOnline.Dominio;
using CurosOnline.Dominio._Base;
using CursoOnline.Cursos;
using CursoOnline.DominioTest._Util;
using Xunit;

namespace CursoOnline.DominioTest.Publico
{
    public class ConversorDePublicoAlvoTest
    {
        private readonly ConversorDePublicoAlvo conversor;

        public ConversorDePublicoAlvoTest()
        {
            conversor = new ConversorDePublicoAlvo();
        }
        
        [Theory]
        [InlineData(PublicoAlvo.Empregado, "Empregado")]
        [InlineData(PublicoAlvo.Universitario, "Universitario")]
        [InlineData(PublicoAlvo.Estudante, "Estudante")]
        [InlineData(PublicoAlvo.Empreendedor, "Empreendedor")]
        public void DeveConverterPublicoAlvoTest(PublicoAlvo publicoAlvo, string publicoAlvoString)
        {
            var publicoAlvoConvertido = conversor.Convert(publicoAlvoString);
            
            Assert.Equal(publicoAlvo, publicoAlvoConvertido);
        }

        [Fact]
        public void NaoDeveConverterQuandoPublicoAlvoEhInvalidoTest()
        {
            var publicAlvoInvalido = "ZeDroguinha";
            
            Assert.Throws<ExcecaoDeDominio>(() => conversor.Convert(publicAlvoInvalido)).ComMensagem(Resource.PublicoAlvoInvalido);
        }
    }
}