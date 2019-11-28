using System.Linq;
using CurosOnline.Dominio.Alunos;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Web.Controllers
{
    public class AlunoController : Controller
    {
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly IAlunoRepositorio _alunoRepositorio;
        
        public AlunoController(ArmazenadorDeAluno armazenadorDeAluno, IAlunoRepositorio alunoRepositorio)
        {
            _armazenadorDeAluno = armazenadorDeAluno;
            _alunoRepositorio = alunoRepositorio;
        }

        public IActionResult Index()
        {
            var aluno = _alunoRepositorio.Consultar();
            
            if (aluno.Any())
            {
                var alunoDto = aluno.Select(x => new AlunoParaListagemDto
                {
                    Cpf = x.Cpf,
                    Email = x.Email,
                    Id = x.Id,
                    Nome = x.Nome
                });
                return View("Index", PaginatedList<AlunoParaListagemDto>.Create(alunoDto, Request));
            }
            return View("Index", PaginatedList<AlunoParaListagemDto>.Create(null, Request));
        }
        
        [HttpPost]
        public IActionResult Salvar(AlunoDto model)
        {
            _armazenadorDeAluno.ArmazenarALuno(model);
            return Ok();
        }
    }
}