using System;
using System.Collections.Generic;

namespace CurosOnline.Dominio
{
    public class ExcecaoDeDominio : ArgumentException
    {
        public List<string> MensagensDeErro { get; set; }

        public ExcecaoDeDominio(List<string> mensagensDeErros)
        {
            MensagensDeErro = mensagensDeErros;
        }
    }
}