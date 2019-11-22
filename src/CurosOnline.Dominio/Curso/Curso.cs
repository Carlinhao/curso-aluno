using System;
using CurosOnline.Dominio._Base;

namespace CursoOnline.Cursos
{
    public class Curso : Entidade
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

        private Curso() { }

        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor, string descricao)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome inválido");
            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária menor que 1");
            if (valor < 1)
                throw new ArgumentException("Valor curso menor que 1");

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
            Descricao = descricao;
        }
    }
}