using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperDigital.Domain.Entity;
using System;

namespace SuperDigital.Infra.Mapping
{
    public class ContaCorrenteMap : IEntityTypeConfiguration<ContaCorrente>
    {
        public void Configure(EntityTypeBuilder<ContaCorrente> builder)
        {
            builder.ToTable("ContaCorrente");

            builder.HasKey(c => c.Numero);

            builder.HasMany<Lancamento>(c => c.Lancamentos)
                   .WithOne().HasForeignKey(p => p.ContaCorrenteNumero);

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development"))
            {
                builder.HasData(new ContaCorrente
                {
                    Numero = 1001
                },
                new ContaCorrente
                {
                    Numero = 1002
                });
            }
        }
    }
}
