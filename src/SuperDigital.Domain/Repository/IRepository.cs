using SuperDigital.Domain.Entity;
using System.Threading.Tasks;

namespace SuperDigital.Domain.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        void Inserir(T item);

        void Atualizar(T item);

        Task<T> ObterAsync(int id);

        Task<int> CommitAsync();
    }
}
