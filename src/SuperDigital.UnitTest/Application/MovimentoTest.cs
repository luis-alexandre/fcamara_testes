using Moq;
using SuperDigital.Application;
using SuperDigital.Domain.Entity;
using SuperDigital.Domain.Exceptions;
using SuperDigital.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SuperDigital.UnitTest.Application
{
    public class MovimentoTest
    {
        private const int CONTA_CORRENTE_ORIGEM_NUMERO = 103030;
        private const int CONTA_CORRENTE_DESTINO_NUMERO = 54646;

        private readonly Mock<IRepository<ContaCorrente>> _repositoryMock;
        private readonly Movimento _movimento;

        public MovimentoTest()
        {
            _repositoryMock = new Mock<IRepository<ContaCorrente>>();
            _movimento = new Movimento(_repositoryMock.Object);
        }

        [Fact]
        public async Task ContaNumero_MaiorQueZero_RetornaContaCorrente()
        {
            var contaCorrenteResult = new ContaCorrente
            {
                Numero = CONTA_CORRENTE_ORIGEM_NUMERO
            };

            contaCorrenteResult.AdicionarLancamento(new Lancamento { Valor = 12.56m, Tipo = LancamentoTipo.Credito });
            contaCorrenteResult.AdicionarLancamento(new Lancamento { Valor = 12.56m, Tipo = LancamentoTipo.Debito });

            _repositoryMock.Setup(x => x.ObterAsync(It.IsAny<int>())).ReturnsAsync(contaCorrenteResult);

            var contaCorrente = await _movimento.ObterMovimentacaoAsync(CONTA_CORRENTE_ORIGEM_NUMERO);

            Assert.NotNull(contaCorrente);
        }

        [Fact]
        public async Task Valor_Invalido_ThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _movimento.RealizarTransferenciaAsync(0, 
                                                                                                    CONTA_CORRENTE_DESTINO_NUMERO,
                                                                                                    -15.65m));
        }

        [Fact]
        public async Task ContaOrigem_NaoEncontrada_ThrowsEntityNotFoundException()
        {
            _repositoryMock.Setup(x => x.ObterAsync(It.IsAny<int>())).ReturnsAsync((ContaCorrente)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _movimento.RealizarTransferenciaAsync(CONTA_CORRENTE_ORIGEM_NUMERO, 
                                                                                                          CONTA_CORRENTE_DESTINO_NUMERO, 
                                                                                                          15.65m));
        }

        [Fact]
        public async Task Inputs_Validos_RealizaTransferencia()
        {
            var contaCorrenteResult = new ContaCorrente
            {
                Numero = CONTA_CORRENTE_ORIGEM_NUMERO
            };

            contaCorrenteResult.AdicionarLancamento(new Lancamento { Valor = 12.56m, Tipo = LancamentoTipo.Credito });
            contaCorrenteResult.AdicionarLancamento(new Lancamento { Valor = 12.56m, Tipo = LancamentoTipo.Debito });

            _repositoryMock.Setup(x => x.ObterAsync(It.IsAny<int>())).ReturnsAsync(contaCorrenteResult);

            _repositoryMock.Setup(x => x.Atualizar(It.IsAny<ContaCorrente>())).Verifiable();

            _repositoryMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            var completed = await _movimento.RealizarTransferenciaAsync(CONTA_CORRENTE_ORIGEM_NUMERO,
                                                                        CONTA_CORRENTE_DESTINO_NUMERO,
                                                                        18.65m);

            Assert.True(completed);
        }
    }
}
