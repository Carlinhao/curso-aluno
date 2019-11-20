using CursoOnline.Cursos;

namespace CursoOnline.Dominio._Builders
{
    public class CursoBuilder
    {
        private double _cargaHoraria = 80;
        private string _nome = "Informática básica";
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudantes;
        private double _valorDoCurso = 950;

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

        public Curso Build()
        {
            return new Curso(_nome, _cargaHoraria, _publicoAlvo, _valorDoCurso);
        }
    }
}