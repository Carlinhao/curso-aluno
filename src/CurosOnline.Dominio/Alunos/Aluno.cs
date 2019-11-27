using CurosOnline.Dominio._Base;
using CursoOnline.Cursos;

namespace CurosOnline.Dominio.Alunos
{
    public class Aluno : Entidade
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public PublicoAlvo PublicoAlvo { get; set; }

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
    }
}