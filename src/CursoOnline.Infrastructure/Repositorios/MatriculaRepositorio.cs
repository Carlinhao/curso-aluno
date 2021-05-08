using CursoOnline.Dominio.Matriculas;
using CursoOnline.Infrastructure.Contextos;
using System;
using System.Collections.Generic;
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
    }
}
