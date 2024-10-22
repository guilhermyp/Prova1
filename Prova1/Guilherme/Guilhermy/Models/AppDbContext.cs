using System;
using Microsoft.EntityFrameworkCore;

namespace Guilhermy.Models;

public class AppDbContext : DbContext
{
    public DbSet<Funcionario> Funcionarios { get; set; }

    public DbSet<Folha> Folhas { get; set; }

     public DbSet<Nome> Nome { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source= Guilherme_Guilhermy.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Funcionario>()
            .HasKey(a => a.FuncionarioId);


        modelBuilder.Entity<Folha>()
            .HasKey(m => m.FolhaId);

        modelBuilder.Entity<Nome>()
            .HasKey(c  =>c.Nome);

        modelBuilder.Entity<Cpf>()
            .HasKey(h  =>h.Cpf);

    }

}
