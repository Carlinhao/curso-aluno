using CursoOnline.Cursos;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CursoOnline.Web.Models
{
    public class CursoController : Controller
    {
        public IActionResult Index()
        {
            var cursos = new List<CursoParaListagemDto>();

            return View("Index", PaginatedList<CursoParaListagemDto>.Create(cursos, Request));
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new CursoDto());
        }

        [HttpPost]
        public IActionResult Salvar(CursoDto cursoDto)
        {
            return Ok();
        }
    }
}