using System.Collections.Generic;
using System.Linq;
using CurosOnline.Dominio._Base;
using CursoOnline.Infrastructure.Contextos;

namespace CursoOnline.Infrastructure.Repositorios
{
    public class RepositorioBase<TEntidade> : IRepositorio<TEntidade> where TEntidade : Entidade
    {
        protected readonly ApplicationDbContext Context;

        public RepositorioBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Adicionar(TEntidade entidade)
        {
            Context.Set<TEntidade>().Add(entidade);
        }

        public virtual List<TEntidade> Consultar()
        {
            var entidades = Context.Set<TEntidade>().ToList();
            return entidades.Any() ? entidades : new List<TEntidade>();
        }

        public virtual TEntidade ObterPorId(int id)
        {
            var query = Context.Set<TEntidade>().Where(x => x.Id == id);
            return query.Any() ? query.FirstOrDefault() : null;
        }
    }
}