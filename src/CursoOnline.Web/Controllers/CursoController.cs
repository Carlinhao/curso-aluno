using System.Collections.Generic;
using System.Linq;
using CurosOnline.Dominio._Base;
using CursoOnline.Cursos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Web.Controllers
{
    public class CursoController : Controller
    {
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly IRepositorio<Curso> _cursoRepositorio;


        public CursoController(ArmazenadorDeCurso armazenadorDeCurso, IRepositorio<Curso> curso)
        {
            _armazenadorDeCurso = armazenadorDeCurso;
            _cursoRepositorio = curso;
        }
        
        public IActionResult Index()
        {
            var cursos = _cursoRepositorio.Consultar();

            if (cursos.Any())
            {
                var CursosDtos = cursos.Select(x => new CursoParaListagemDto
                {
                    Id = x.Id,
                    CargaHoraria = x.CargaHoraria,
                    Nome = x.Nome,
                    PublicoAlvo = x.PublicoAlvo.ToString(),
                    Valor = x.Valor
                });
                return View("Index", PaginatedList<CursoParaListagemDto>.Create(CursosDtos, Request));
            }
            return View("Index", PaginatedList<CursoParaListagemDto>.Create(null, Request));
        }
        
        public IActionResult Novo()
        {
            return View("NovoOuEditar", new CursoDto());
        }

        [HttpPost]
        public IActionResult Salvar(CursoDto model)
        {
            _armazenadorDeCurso.Armazenar(model);
            return Ok();
        }
    }
}