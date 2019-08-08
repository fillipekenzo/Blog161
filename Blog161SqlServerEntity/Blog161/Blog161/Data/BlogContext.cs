using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog161.Models;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public DbSet<Blog161.Models.Categoria> Categoria { get; set; }

    public DbSet<Blog161.Models.Mensagem> Mensagem { get; set; }

    public DbSet<Blog161.Models.Comentario> Comentario { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria
            {
                Id = 1,
                Descricao = "Alimentação"
            },
            new Categoria
            {
                Id = 2,
                Descricao = "Tecnologia"
            },
            new Categoria
            {
                Id = 3,
                Descricao = "Lifestyle"
            },
            new Categoria
            {
                Id = 4,
                Descricao = "Jogos"
            },
            new Categoria
            {
                Id = 5,
                Descricao = "Espeorte"
            }
            );
    }
}
