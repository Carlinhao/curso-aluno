using CursoOnline.DominioTest.Cursos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CursoOnline.Web.Util;

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