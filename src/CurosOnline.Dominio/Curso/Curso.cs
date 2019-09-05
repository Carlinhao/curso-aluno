using System;

namespace CursoOnline.DominioTest.Cursos
{
    public class Curso
    {
        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valorDoCurso)
        {
            if(string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome inválido");
            if(cargaHoraria < 1)
                throw new ArgumentException("Carga horária menor que 1");
            if(valorDoCurso < 1)
                throw new ArgumentException("Valor curso menor que 1");
            
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            ValorDoCurso = valorDoCurso;
        }

        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double ValorDoCurso { get; private set; }
    }
}