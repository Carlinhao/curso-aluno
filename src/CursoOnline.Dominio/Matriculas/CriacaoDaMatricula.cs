﻿using CurosOnline.Dominio;
using CurosOnline.Dominio._Base;
using CurosOnline.Dominio.Alunos;
using CurosOnline.Dominio.Matriculas;
using CursoOnline.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Matriculas
{
    public class CriacaoDaMatricula
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly ICursoRepositorio _cursoRepositorio;
        private readonly IMatriculaRepositorio _matriculaRepositorio;

        public CriacaoDaMatricula(IAlunoRepositorio alunoRepositorio, ICursoRepositorio cursoRepositorio, IMatriculaRepositorio matriculaRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
            _cursoRepositorio = cursoRepositorio;
            _matriculaRepositorio = matriculaRepositorio;
        }

        public void Criar(MatriculaDto matriculaDto)
        {
            var curso = _cursoRepositorio.ObterPorId(matriculaDto.CursoId);
            var aluno = _alunoRepositorio.ObterPorId(matriculaDto.AlunoId);

            ValidadorDeRegra.Novo()
                .Quando(curso == null, Resource.CursoNaoEncontrado)
                .Quando(aluno == null, Resource.AlunoNaoEncontrado)
                .DispararExcecaoSeExistir();

            var matricula = new CursoOnline.Dominio.Matriculas
                .Matricula(aluno, curso, matriculaDto.ValorPago);

            _matriculaRepositorio.Adicionar(matricula);
        }
    }
}
