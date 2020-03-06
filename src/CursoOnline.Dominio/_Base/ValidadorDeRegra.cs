using System.Collections.Generic;
using System.Linq;

namespace CurosOnline.Dominio._Base
{
    public class ValidadorDeRegra
    {
        private readonly List<string> _erros;

        public ValidadorDeRegra()
        {
            _erros = new List<string>();
        }

        public static ValidadorDeRegra Novo()
        {
            return new ValidadorDeRegra();
        }

        public ValidadorDeRegra Quando(bool temErro, string mensagemDeErro)
        {
            if (temErro)
                _erros.Add(mensagemDeErro);

            return this;
        }

        public void DispararExcecaoSeExistir()
        {
            if (_erros.Any())
                throw new ExcecaoDeDominio(_erros);
        }
    }
}