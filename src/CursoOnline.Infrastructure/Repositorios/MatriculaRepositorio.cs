using CursoOnline.Dominio.Matriculas;
using CursoOnline.Infrastructure.Contextos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Infrastructure.Repositorios
{
    public class MatriculaRepositorio : RepositorioBase<Matricula>, IMatriculaRepositorio
    {
        public MatriculaRepositorio(ApplicationDbContext context)
            : base(context)
        {
        }

        public override List<Matricula> Consultar()
        {
            var query = Context.Set<Matricula>()
                .Include(i => i.Aluno)
                .Include(i => i.Curso)
                .ToList();

            return query;
        }
    }
}
