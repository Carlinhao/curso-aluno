using CursoOnline.Cursos;
using System;

namespace CursoOnline.Dominio._Builders
{
    public class CursoBuilder
    {
        private double _cargaHoraria = 80;
        private string _nome = "Informática básica";
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
        private double _valorDoCurso = 950;
        private string _descricao = "Progração .NetCore";
        private int _id;

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public CursoBuilder ComValor(double valorCurso)
        {
            _valorDoCurso = valorCurso;
            return this;
        }

        public CursoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public Curso Build()
        {
            var curso = new Curso(_nome, _cargaHoraria, _publicoAlvo, _valorDoCurso, _descricao);

            if (_id > 0)
            {
                var propertyInfo = curso.GetType().GetProperty("Id");
                propertyInfo.SetValue(curso, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }
            return curso;
        }
    }
}