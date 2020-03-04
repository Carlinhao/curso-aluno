using CursoOnline.Cursos;
using System;

namespace CurosOnline.Dominio._Base
{
    public class ConversorDePublicoAlvo : IConversorDePublicoAlvo
    {
        public PublicoAlvo Convert(string publicoAlvo)
        {
            ValidadorDeRegra.Novo()
                .Quando(!Enum.TryParse<PublicoAlvo>(publicoAlvo, out var publicoAlvoConvertido), Resource.PublicoAlvoInvalido)
                .DispararExcecaoSeExistir();
            
            return publicoAlvoConvertido;
        }
    }
}