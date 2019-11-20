using System;

namespace CursoOnline.Cursos
{
    public class Curso
    {
        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valorDoCurso)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome inválido");
            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária menor que 1");
            if (valorDoCurso < 1)
                throw new ArgumentException("Valor curso menor que 1");

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            ValorDoCurso = valorDoCurso;
        }

        public string Nome { get; }
        public double CargaHoraria { get; }
        public PublicoAlvo PublicoAlvo { get; }
        public double ValorDoCurso { get; }
    }
}