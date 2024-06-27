using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
//Son las instrucciones de mapeo del ORM
namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        //dbset recive una entidad y permite traducirlo a una tabla y la tabla con ese nombre se traduce a entidad
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Sneaker> sneakers { get; set; }
        public DbSet<ReservationSneaker> ReservationSneakers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        //---Fuent API---
        // Este método (OnModelCreating) se usa para definir cómo se deben mapear las entidades del dominio a las tablas de la base de datos
        // y establecer sus relaciones, conversiones (el enum a cadena) y datos iniciales (Seed Data).
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tabla intermedia para que se puedan duplicar las zapatillas en las reservaciones
            modelBuilder.Entity<ReservationSneaker>()
                .HasKey(rs => rs.Id);
            //reservationsenaker tiene una reservacion, esta tiene muchas reservationsneaker con una HasForeignKey ... 
            modelBuilder.Entity<ReservationSneaker>()
                .HasOne(rs => rs.Reservation)
                .WithMany(r => r.ReservationSneakers)
                .HasForeignKey(rs => rs.ReservationId);
            modelBuilder.Entity<ReservationSneaker>()
                .HasOne(rs => rs.Sneaker)
                .WithMany(s => s.ReservationSneakers)
                .HasForeignKey(s => s.SneakerId);


            //Cardinalidad, Relación uno a muchos entre Reservation y usuario 
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.IdUser);



            //has many: Tiene muchos --has one: tiene uno -- whith many: con muchos -- has foreignkey: tiene una Fkey



            // ---Configura las propiedades enum para almacenarse como cadenas---
            modelBuilder.Entity<Sneaker>()
                .Property(s => s.Brand)
                .HasConversion(new EnumToStringConverter<Sneaker.SneakerBrand>());

            modelBuilder.Entity<Sneaker>()
                .Property(s => s.Category)
                .HasConversion(new EnumToStringConverter<Sneaker.SneakerCategory>());

            modelBuilder.Entity<User>()
                .Property(s => s.Type)
                .HasConversion(new EnumToStringConverter<User.UserType>());

            modelBuilder.Entity<Reservation>()
                .Property(s => s.State)
                .HasConversion(new EnumToStringConverter<Reservation.ReservationState>());



            //--Crea primeos datos en la base de datos.--
            modelBuilder.Entity<User>().HasData(CreateUserSeedData());
            modelBuilder.Entity<Sneaker>().HasData(CreateSneakerSeedData());

        }

        private User[] CreateUserSeedData()
        {
            return new User[]
            {
            new User { Id = 1, Name = "Ana", Password = "Pass1", EmailAddress = "Ana@example.com", Type = User.UserType.Admin },
            new User { Id = 2, Name = "Delfina", Password = "Pass2", EmailAddress = "delfina@example.com", Type = User.UserType.Admin },
            new User { Id = 3, Name = "Juan", Password = "Pass3", EmailAddress = "juan.doe@example.com", Type = User.UserType.Client  },
            new User { Id = 4, Name = "Victoria", Password = "Pass4", EmailAddress = "vicky.sosa@example.com", Type = User.UserType.Client },
            };
        }

        private Sneaker[] CreateSneakerSeedData()
        {
            return new Sneaker[]
            {
            new Sneaker { Id = 1, Name = "Air Max", Brand = Sneaker.SneakerBrand.Nike, Price = 120, Category = Sneaker.SneakerCategory.Casual, Stock = 50 },
            new Sneaker { Id = 2, Name = "Classic", Brand = Sneaker.SneakerBrand.Adidas, Price = 100, Category = Sneaker.SneakerCategory.Casual, Stock = 30 },
            new Sneaker { Id = 3, Name = "ZoomX", Brand = Sneaker.SneakerBrand.Nike, Price = 150, Category = Sneaker.SneakerCategory.Running, Stock = 20 },
            new Sneaker { Id = 4, Name = "Superstar", Brand = Sneaker.SneakerBrand.Adidas, Price = 80, Category = Sneaker.SneakerCategory.Running, Stock = 40 },
            new Sneaker { Id = 5, Name = "Gel-Kayano", Brand = Sneaker.SneakerBrand.Adidas, Price = 140, Category = Sneaker.SneakerCategory.Sports, Stock = 25 },
            new Sneaker { Id = 6, Name = "Chuck Taylor", Brand = Sneaker.SneakerBrand.Converse, Price = 60, Category = Sneaker.SneakerCategory.Casual, Stock = 35 },
            new Sneaker { Id = 7, Name = "Ultraboost", Brand = Sneaker.SneakerBrand.Adidas, Price = 180, Category = Sneaker.SneakerCategory.Sports, Stock = 15 },
            new Sneaker { Id = 8, Name = "Pegasus", Brand = Sneaker.SneakerBrand.Nike, Price = 110, Category = Sneaker.SneakerCategory.Running, Stock = 45 },
            new Sneaker { Id = 9, Name = "Pegaboot", Brand = Sneaker.SneakerBrand.Adidas, Price = 110, Category = Sneaker.SneakerCategory.Running, Stock = 55 },
            };
        }


    }
}