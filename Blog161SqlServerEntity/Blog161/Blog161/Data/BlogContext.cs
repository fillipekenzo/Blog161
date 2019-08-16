using Microsoft.EntityFrameworkCore;
using Blog161.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Blog161.Data
{
    public class BlogContext : IdentityDbContext<User>
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }

        public DbSet<Categoria> Categoria { get; set; }

        public DbSet<Mensagem> Mensagem { get; set; }

        public DbSet<Comentario> Comentario { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                    Descricao = "Esporte"
                }
                );
        }
    }
}