using ConversorDeMonedasBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ConversorDeMonedasBack.Data
{
    public class CurrencyConverterContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Conversion> Conversions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public CurrencyConverterContext(DbContextOptions<CurrencyConverterContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // RELACIÓN MUCHOS A MUCHOS ENTRE USER Y CURRENCY DE NAVEGACION UNIDIRECCIONAL SIN USAR ENTIDAD INTERMEDIA CON EF CORE
            modelBuilder.Entity<User>()
                .HasMany(u => u.Currencies)
                .WithMany()
                .UsingEntity("UsersCurrecies");


            // RELACION 1, N SUSCRIPCION/USUARIO
            modelBuilder.Entity<Subscription>()
                .HasMany(s => s.Users)
                .WithOne(u => u.Subscription)
                .HasForeignKey(u => u.SubscriptionId);

            // RELACION 1, N USUARIO/CONVERSION
            modelBuilder.Entity<User>()
                .HasMany(u => u.Conversions)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
           // base.OnModelCreating(modelBuilder);
        }
    }
}

