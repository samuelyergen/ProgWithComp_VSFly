using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreApp2021_1
{
    public class WWWingsContext : DbContext
    {
        public DbSet<Flight> FlightSet { get; set; }
        public DbSet<Pilot> PilotSet { get; set; }
        public DbSet<Passenger> PassengerSet { get; set; }
        public DbSet<Booking> BookingSet { get; set; }

        //App = string use to identify us
        public static string ConnectionString { get; set; } = @"Server=(localDB)\MSSQLLocalDB;Database=WWWings_2021Step1;Trusted_Connection=True;MultipleActiveResultSets=true;App=EFCoreApp2021";

        public WWWingsContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(ConnectionString);

            builder.UseLazyLoadingProxies();
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //composed key
            builder.Entity<Booking>().HasKey(x => new { x.FlightNo, x.PassengerID });

            //map many-to-many relationship
            //no more manual mapping
            builder.Entity<Booking>()
                .HasOne(x => x.Flight)
                .WithMany(x => x.BookingSet)
                .HasForeignKey(x => x.FlightNo)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                .HasOne(x => x.Passenger)
                .WithMany(x => x.BookingSet)
                .HasForeignKey(x => x.PassengerID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
