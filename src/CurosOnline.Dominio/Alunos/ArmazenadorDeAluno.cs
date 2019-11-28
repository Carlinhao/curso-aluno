using CurosOnline.Dominio._Base;
using CursoOnline.Cursos;
using System;

namespace CurosOnline.Dominio.Alunos
{
    public class ArmazenadorDeAluno
    {
        private readonly IAlunoRepositorio _alunoRepositorio;

        public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public void ArmazenarALuno(AlunoDto alunoDto)
        {
            var alunoJaSalvo = _alunoRepositorio.ObterPorId(alunoDto.Id);

            ValidadorDeRegra.Novo()
                .Quando(!Enum.TryParse<PublicoAlvo>(alunoDto.PublicoAlvo, out var publicoAlvo), Resource.PublicoAlvoInvalido)
                .DispararExcecaoSeExistir();

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
