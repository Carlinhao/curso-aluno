using CurosOnline.Dominio._Base;
using CursoOnline.Cursos;
using System;

namespace CurosOnline.Dominio.Alunos
{
    public class ArmazenadorDeAluno
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IConversorDePublicoAlvo _conversorDePublicoAlvo;

        public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio, IConversorDePublicoAlvo conversorDePublicoAlvo)
        {
            _alunoRepositorio = alunoRepositorio;
            _conversorDePublicoAlvo = conversorDePublicoAlvo;
        }

        public void ArmazenarALuno(AlunoDto alunoDto)
        {
            var alunoJaSalvo = _alunoRepositorio.ObterPorId(alunoDto.Id);

            var publicoAlvo = _conversorDePublicoAlvo.Convert(alunoDto.PublicoAlvo);

            var aluno =  new Aluno(alunoDto.Nome, alunoDto.Email, alunoDto.Cpf, publicoAlvo);

            if(alunoDto.Id > 0)
            {
                aluno = _alunoRepositorio.ObterPorId(alunoDto.Id);
                aluno.AlterarEmail(alunoDto.Email);
                aluno.AlterarNome(alunoDto.Nome);
            }

            if (alunoDto.Id == 0)
                _alunoRepositorio.Adicionar(aluno);
        }
    }
}
