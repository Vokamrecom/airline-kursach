using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Airline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airline.DataProvider
{
    public class AirlineContext : DbContext
    {
        public AirlineContext(DbContextOptions<AirlineContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>()
                .HasMany(a => a.Flights)
                .WithOne(f => f.Aircraft)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Airport>()
                .HasMany(a => a.Flights1)
                .WithOne(f => f.ArrAirport)
                .HasForeignKey(e => e.ArrivalAirport)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Airport>()
                .HasMany(a => a.Flights2)
                .WithOne(f => f.DepAirport)
                .HasForeignKey(e => e.DepartureAirport)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Bookings)
                .WithOne(b => b.Flight)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Tickets)
                .WithOne(b => b.Flight)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Passenger>()
                .HasMany(p => p.Bookings)
                .WithOne(b => b.Passenger)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Passenger>()
                .HasMany(p => p.Notifications)
                .WithOne(n => n.Passenger)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Passenger>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.Buyer)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
