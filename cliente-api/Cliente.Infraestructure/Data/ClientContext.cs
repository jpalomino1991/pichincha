using Cliente.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cliente.Infraestructure.Data
{

    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<PersonEntity>().HasKey(p => p.PersonId);
            modelBuilder.Entity<ClientEntity>().HasKey(c => c.ClientId);

            base.OnModelCreating(modelBuilder);
        }
    }

}
