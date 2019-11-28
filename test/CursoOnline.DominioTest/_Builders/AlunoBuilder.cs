using CurosOnline.Dominio.Alunos;
using CursoOnline.Cursos;
using System;

namespace CursoOnline.DominioTest._Builders
{
    public class AlunoBuilder
    {
        private string _nome = "Paul Stone";
        private string _cpf = "58947037001";
        private string _email = "teste@teste.com";
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Universitario;
        private int _id;

        public static AlunoBuilder Novo()
        {
            return new AlunoBuilder();
        }

        public AlunoBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public AlunoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public AlunoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public AlunoBuilder ComCpf(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public AlunoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public Aluno Build()
        {
            var aluno = new Aluno(_nome, _email, _cpf, _publicoAlvo);

            if(aluno.Id > 0)
            {
                var propertyInfo = aluno.GetType().GetProperty("Id");
                propertyInfo.SetValue(aluno, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return aluno;
        }
    }
}