using Microsoft.EntityFrameworkCore;
using SuperDigital.Domain.Entity;
using SuperDigital.Infra.Mapping;
using System;
using System.Collections.Generic;

namespace SuperDigital.Infra.Context
{
    public class SQLContext : DbContext
    {
        public SQLContext()
        {

        }

        public SQLContext(DbContextOptions<SQLContext> optionsBuilder)
            : base(optionsBuilder)
        {

        }

        public DbSet<ContaCorrente> ContaCorrente { get; set; }

        public DbSet<Lancamento> Lancamento { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(Environment.GetEnvironmentVariable("DatabaseConn"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContaCorrente>(new ContaCorrenteMap().Configure);
            modelBuilder.Entity<Lancamento>(new LancamentoMap().Configure);
        }

    }
}
