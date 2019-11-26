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
            Nome = nome;
            Email = email;
            Cpf = cpf;
            PublicoAlvo = publicoAlvo;
        }
    }
}