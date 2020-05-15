using System;

namespace SuperDigital.Domain.Entity
{
    public class Lancamento : EntityBase
    {
        public decimal Valor { get; set; }

        public LancamentoTipo Tipo { get; set; }

        public DateTime DataLancamento { get; set; } = DateTime.Now;

        public int ContaCorrenteNumero { get; set; }

        public virtual ContaCorrente ContaCorrente { get; set; }
    }
}
