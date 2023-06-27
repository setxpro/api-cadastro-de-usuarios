using CrudSimples.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudSimples.Connection
{
    public class ConnectionDatabase : DbContext // DbContext -> Class base
    {
        // Para eu passar a connection string via parametro
        // <ConnectionDatabase> tipo da class | base(options) vem da class base
        public ConnectionDatabase(DbContextOptions<ConnectionDatabase> options): base(options)
        {
            this.Database.EnsureCreated(); // Vai criar o banco automaticamente quando iniciar a aplicação (Entity Framework)
        }

        // Referência a entidade
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapear a entidade
            // # ID
            modelBuilder.Entity<User>()
                .ToTable("usuarios"); // Mapear o nome da tabela que eu quero
            modelBuilder.Entity<User>()
                .Property(c => c.Id) // Acessar as propriedades da minha entidade
                .HasColumnName("id") // Nome que eu quero para a coluna
                .ValueGeneratedOnAdd(); // Vai gerar um novo ID automaticamente

            // # NAME
            modelBuilder.Entity<User>()
               .Property(c => c.Name) // Acessar as propriedades da minha entidade
               .HasColumnName("name"); // Nome que eu quero para a coluna
            // # AGE
            modelBuilder.Entity<User>()
              .Property(c => c.Age) // Acessar as propriedades da minha entidade
              .HasColumnName("age"); // Nome que eu quero para a coluna

            // # EMAIL
            modelBuilder.Entity<User>()
              .Property(c => c.Email) // Acessar as propriedades da minha entidade
              .HasColumnName("email"); // Nome que eu quero para a coluna

            // # PASSWORD
            modelBuilder.Entity<User>()
              .Property(c => c.Password) // Acessar as propriedades da minha entidade
              .HasColumnName("password"); // Nome que eu quero para a coluna

            // # USERNAME
            modelBuilder.Entity<User>()
              .Property(c => c.UserName) // Acessar as propriedades da minha entidade
              .HasColumnName("username"); // Nome que eu quero para a coluna

        }
    }
}
