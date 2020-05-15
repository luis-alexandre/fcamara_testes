using System;
using System.Collections.Generic;

namespace SuperDigital.Domain.Entity
{
    public class ContaCorrente : EntityBase
    {
        public virtual List<Lancamento> Lancamentos { get; set; } = new List<Lancamento>();

        public void AdicionarLancamento(Lancamento lancamento)
        {
            if(lancamento == null)
            {
                throw new ArgumentNullException("lancamento");
            }

            Lancamentos.Add(lancamento);
        }
    }
}
