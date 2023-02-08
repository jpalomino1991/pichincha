using Cuenta.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cuenta.Infraestructure.Data
{

    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<AccountEntity>().HasKey(x => x.AccountId);

            base.OnModelCreating(modelBuilder);
        }
    }

}
