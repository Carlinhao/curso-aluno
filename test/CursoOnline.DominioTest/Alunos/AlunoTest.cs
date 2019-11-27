using Bogus;
using CurosOnline.Dominio;
using CurosOnline.Dominio.Alunos;
using CursoOnline.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Alunos
{
    public class AlunoTest
    {
        private readonly string _nome;
        private readonly string _cpf;
        private readonly string _email;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly Faker _faker;

        public AlunoTest()
        {
            _faker = new Faker();

            _nome = _faker.Person.FullName;
            _cpf = GeradorCpf.GerarCpf();
            _email = _faker.Person.Email;
            _publicoAlvo = PublicoAlvo.Universitario;
        }

        [Fact]
        public void DeveCriarAlunoTest()
        {
            //Arrange
            var aluno = new
            {
                Nome = _nome,
                Cpf = _cpf,
                Email = _email,
                PublicoAlvo = _publicoAlvo
            };

            //Act
            var curso = new Aluno(aluno.Nome,
                                  aluno.Email,
                                  aluno.Cpf,
                                  aluno.PublicoAlvo);

            //Assert
            aluno.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("987654321789")]
        [InlineData(null)]
        [InlineData("")]
        public void NaoDeveAlunoTerUmCpfInvalidoTest(string cpf)
        {
            Assert.Throws<ExcecaoDeDominio>(() => AlunoBuilder
                                                  .Novo()
                                                  .ComCpf(cpf)
                                                  .Build())                                                  
                                                  .ComMensagem(Resource.CpfInvalido);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NaoDeveAlunoTerNomeInvalido(string nome)
        {
            Assert.Throws<ExcecaoDeDominio>(() => AlunoBuilder
                                                  .Novo()
                                                  .ComNome(nome)
                                                  .Build())
                                                  .ComMensagem(Resource.NomeInvalido);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NaoDeveAlunoTerEmailInvalido(string email)
        {
            Assert.Throws<ExcecaoDeDominio>(() => AlunoBuilder
                                                  .Novo()
                                                  .ComEmail(email)
                                                  .Build())
                                                  .ComMensagem(Resource.EmailInvalido);
        }
    }
}