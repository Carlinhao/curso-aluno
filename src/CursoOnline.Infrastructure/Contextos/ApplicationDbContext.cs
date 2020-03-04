using CurosOnline.Dominio.Alunos;
using CursoOnline.Cursos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CursoOnline.Infrastructure.Contextos
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public async Task Commit()
        {
            await SaveChangesAsync();
        } 
    }
}