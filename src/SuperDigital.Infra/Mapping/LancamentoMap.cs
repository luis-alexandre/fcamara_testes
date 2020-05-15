using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using SuperDigital.Domain.Entity;
using System;
using System.Security.Cryptography.X509Certificates;

namespace SuperDigital.Infra.Mapping
{
    public class LancamentoMap : IEntityTypeConfiguration<Lancamento>
    {
        public void Configure(EntityTypeBuilder<Lancamento> builder)
        {
            builder.ToTable("Lancamento");

            builder.HasKey(c => c.Numero);

            builder.Property(c => c.Numero).ValueGeneratedOnAdd();

            builder.Property(c => c.Valor).IsRequired();

            builder.Property(c => c.DataLancamento).IsRequired();

            builder.HasOne(c => c.ContaCorrente)
                   .WithMany(x => x.Lancamentos)
                   .HasForeignKey(p => p.ContaCorrenteNumero);

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development"))
            {
                builder.HasData(new Lancamento
                {
                    Numero = 1,
                    ContaCorrenteNumero = 1001,
                    DataLancamento = DateTime.Now,
                    Tipo = LancamentoTipo.Credito,
                    Valor = 10050.52m
                },
                new Lancamento
                {
                    Numero = 2,
                    ContaCorrenteNumero = 1002,
                    DataLancamento = DateTime.Now,
                    Tipo = LancamentoTipo.Credito,
                    Valor = 10050.52m
                });
            }
        }
    }
}
