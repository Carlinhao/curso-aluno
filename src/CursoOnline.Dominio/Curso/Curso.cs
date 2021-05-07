using CurosOnline.Dominio;
using CurosOnline.Dominio._Base;

namespace CursoOnline.Cursos
{
    public class Curso : Entidade
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public decimal Valor { get; private set; }

        private Curso() { }

        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, decimal valor, string descricao)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
                .Quando(valor < 1, Resource.ValorCursoInvalido)
                .DispararExcecaoSeExistir();

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
            Descricao = descricao;
        }

        public void AlterarNome(string nomeCurso)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nomeCurso), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();
            Nome = nomeCurso;
        }

        public void AlterarCargaHoraria(double cargaHoraria)
        {
            ValidadorDeRegra.Novo()
                .Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
                .DispararExcecaoSeExistir();

            CargaHoraria = cargaHoraria;
        }

        public void AlterarValor(decimal valorCurso)
        {
            ValidadorDeRegra.Novo()
                .Quando(valorCurso < 1, Resource.ValorCursoInvalido)
                .DispararExcecaoSeExistir();

            Valor = valorCurso;
        }
    }
}