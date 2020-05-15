using SuperDigital.Domain.Entity;
using SuperDigital.Domain.Exceptions;
using SuperDigital.Domain.Repository;
using SuperDigital.Domain.Service;
using System;
using System.Threading.Tasks;

namespace SuperDigital.Application
{
    public class Movimento : IMovimento
    {
        IRepository<ContaCorrente> _repository;

        public Movimento(IRepository<ContaCorrente> repository)
        {
            _repository = repository;
        }

        public async Task<ContaCorrente> ObterMovimentacaoAsync(int contaNumero)
        {
            if(contaNumero <= 0)
            {
                throw new ArgumentException("contaNumero");
            }

            var result = await _repository.ObterAsync(contaNumero);

            return result;
        }

        public async Task<bool> RealizarTransferenciaAsync(int contaOrigemNumero, int contaDestinoNumero, decimal valor)
        {
            if(valor <= 0)
            {
                throw new ArgumentException("Valor inválido", nameof(valor));
            }

            var contaOrigem = await _repository.ObterAsync(contaOrigemNumero);
            if (contaOrigem == null)
            {
                throw new EntityNotFoundException("Conta origem não encontrada.");
            }

            var contaDestino = await _repository.ObterAsync(contaDestinoNumero);
            if(contaDestino == null)
            {
                throw new EntityNotFoundException("Conta destino não encontrada.");
            }

            contaOrigem.AdicionarLancamento(new Lancamento { Valor = valor, Tipo = LancamentoTipo.Debito });
            contaDestino.AdicionarLancamento(new Lancamento { Valor = valor, Tipo = LancamentoTipo.Credito });

            _repository.Atualizar(contaOrigem);
            _repository.Atualizar(contaDestino);

            return await _repository.CommitAsync() > 0;
        }
    }
}
