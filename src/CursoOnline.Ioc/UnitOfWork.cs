using CurosOnline.Dominio._Base;
using CursoOnline.Infrastructure.Contextos;
using System.Threading.Tasks;

namespace CursoOnline.Ioc
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Commit()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
