using CurosOnline.Dominio;
using Xunit;

namespace CursoOnline.DominioTest._Util
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ExcecaoDeDominio exception, string message)
        {
            if (exception.MensagensDeErro.Contains(message))
                Assert.True(true);
            else
                Assert.False(true, $" Estava esperando a mensagem '{message}'");
        }
    }
}