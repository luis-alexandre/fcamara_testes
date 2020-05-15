using Microsoft.EntityFrameworkCore;
using SuperDigital.Domain.Entity;
using SuperDigital.Domain.Repository;
using SuperDigital.Infra.Context;
using System.Threading.Tasks;

namespace SuperDigital.Infra.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : EntityBase
    {

        private SQLContext context = new SQLContext();

        public async Task<T> ObterAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public void Inserir(T item)
        {
            context.Set<T>().Add(item);
        }

        public void Atualizar(T item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
