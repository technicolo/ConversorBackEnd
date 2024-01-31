
using ConversorBackEnd.entityes;
using Microsoft.EntityFrameworkCore;
using ConversorBackEnd.Models.Enum;

namespace ConverContext.Data
{
    public class ConversorContext: DbContext
    {
        public DbSet<UserEntity> UserEntitys { get; set; }


        public ConversorContext(DbContextOptions<ConversorContext> options) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserEntity karen = new UserEntity()
            {
                Id = 1,
                FirstName = "Karen",
                LastName = "Lasot",
                Password = "Pa$$w0rd",
                UserName = "karenbailapiola@gmail.com",
                Role = ConversorAPI.Models.Enum.Role.Admin,
            };
            UserEntity luis = new UserEntity()
            {
                Id = 2,
                FirstName = "Luis Gonzalez",
                LastName = "Gonzales",
                Password = "lamismadesiempre",
                UserName = "elluismidetotoras@gmail.com",
            };



            modelBuilder.Entity<UserEntity>().HasData(
                karen, luis);

            modelBuilder.Entity<UserEntity>()
              .HasMany<Monedas>(u => u.Monedas)
              .WithOne(c => c.User);

            base.OnModelCreating(modelBuilder);
        }
    }
}
