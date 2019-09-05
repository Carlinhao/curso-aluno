using System;
using Xunit;

namespace CursoOnline.DominioTest._Util
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ArgumentException exception, string message)
        {
            if(exception.Message == message)
                Assert.True(true);
            else
                Assert.False(true, $" Estava esperando a mensagem '{ message }'");
        }
    }
}