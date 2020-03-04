using System;
using CurosOnline.Dominio._Base;
using CursoOnline.Cursos;

namespace CurosOnline.Dominio.Alunos
{
    public class Aluno : Entidade
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }

        public Aluno(string nome, string email, string cpf, PublicoAlvo publicoAlvo)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .Quando(!ValidadorCpf.IsCpf(cpf), Resource.CpfInvalido)
                .Quando(string.IsNullOrEmpty(cpf), Resource.CpfInvalido)
                .Quando(!ValidadorEmail.IsEmail(email), Resource.EmailInvalido)
                .DispararExcecaoSeExistir();
            Nome = nome;
            Email = email;
            Cpf = cpf;
            PublicoAlvo = publicoAlvo;
        }

        public void AlterarNome(string nome)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();
            Nome = nome;
        }

        public void AlterarEmail(string email)
        {
            ValidadorDeRegra.Novo()
                .Quando(!ValidadorEmail.IsEmail(email), Resource.EmailInvalido)
                .DispararExcecaoSeExistir();
            Email = email;
        }
    }
}