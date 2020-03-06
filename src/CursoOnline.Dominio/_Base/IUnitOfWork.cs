using System.Threading.Tasks;

namespace CurosOnline.Dominio._Base
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
