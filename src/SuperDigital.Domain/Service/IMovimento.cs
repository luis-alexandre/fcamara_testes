using SuperDigital.Domain.Entity;
using System.Threading.Tasks;

namespace SuperDigital.Domain.Service
{
    public interface IMovimento
    {
        Task<bool> RealizarTransferenciaAsync(int contaOrigemNumero, int ContaDestinoNumero, decimal valor);

        Task<ContaCorrente> ObterMovimentacaoAsync(int contaNumero);
    }
}
