using Microsoft.EntityFrameworkCore;
using Movimiento.Domain.Entities;

namespace Movimiento.Infraestructure.Data
{

    public class MovimientoContext : DbContext
    {
        public MovimientoContext(DbContextOptions<MovimientoContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<MovementEntity>().HasKey(m => m.MovementId);
            
            base.OnModelCreating(modelBuilder);
        }
    }

}
