using Microsoft.EntityFrameworkCore;
using Airline.Common;
using Airline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airline.DataProvider.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        IEnumerable<Booking> GetDefaultUsersBookings();
        IEnumerable<Booking> GetAllByFlight(int flightId);
        Booking NewBooking(int userId, int flightId, int amount);
        Booking GetBooking(int bookingId);
        Booking CancelBooking(int bookingId);
        IEnumerable<Booking> GetUserBookings(int? userId);
        bool IsAlreadyBooked(int userId, int flightId);
        void DeleteAllBookings(int userId);
        Booking GetBooking(int userId, int flightId);
        void IsThereAccess(int userId, int? bookingId);
        string GetOwner(int bookingId);
    }
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AirlineContext context) : base(context)
        {
        }

        public IEnumerable<Booking> GetDefaultUsersBookings()
        {
            return DbSet
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Include(b => b.Passenger)
                .Where(b => b.Passenger.Role != Constants.AdminRole);
        }

        public Booking GetBooking(int Id)
        {
            var booking = DbSet
                .Include(b => b.Flight)
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Include(b => b.Passenger)
                .Where(b => b.BookingId == Id)
                .FirstOrDefault();

            if (booking == null)
                throw new ApplicationException($"Can't find booking with id = {Id}");

            return booking;
        }

        public Booking NewBooking(int userId, int flightId, int amount)
        {
            var passenger = Context.Passengers
                .Where(p => p.PassengerId == userId)
                .FirstOrDefault();
            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with id = {userId}");

            var flight = Context.Flights
                .Where(f => f.FlightId == flightId)
                .FirstOrDefault();
            if (flight == null)
                throw new ApplicationException($"Can't find flight with id = {flightId}");

            var book = DbSet
                .Add(new Booking
                {
                    PassengerId = passenger.PassengerId,
                    BookingDateTime = DateTime.Now,
                    FlightId = flight.FlightId,
                    Amount = amount,
                    Price = flight.Price,
                    IsActive = true
                });
            Context.SaveChanges();
            var booking = DbSet
                .Include(b => b.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Where(b => b.PassengerId == passenger.PassengerId && b.FlightId == flight.FlightId)
                .FirstOrDefault();

            if (booking == null)
                throw new ApplicationException($"Can't find booking user with id = {userId} on flight with id = {flightId}");

            return booking;
        }

        public Booking CancelBooking(int bookingId)
        {
            var booking = DbSet
                .Where(b => b.BookingId == bookingId)
                .FirstOrDefault();

            if (booking == null)
                throw new ApplicationException($"Can't find booking with id = {bookingId}");

            DbSet.Remove(booking);
            Context.SaveChanges();
            return booking;
        }

        public IEnumerable<Booking> GetUserBookings(int? userId)
        {
            var bookings = DbSet
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Include(t => t.Passenger)
                .Where(b => b.IsActive == true);

            if (userId.HasValue)
                bookings = bookings.Where(b => b.PassengerId == userId);
            else
                bookings = bookings.Where(b => b.Passenger.Role != Constants.AdminRole);

            return bookings;
        }

        public bool IsAlreadyBooked(int userId, int flightId)
        {
            return DbSet.Any(b => b.PassengerId == userId && b.FlightId == flightId);
        }

        public void DeleteAllBookings(int userId)
        {
            var list = DbSet
                .Where(b => b.PassengerId == userId)
                .ToList();

            foreach (var b in list)
                DbSet.Remove(b);
            Context.SaveChanges();
        }

        public Booking GetBooking(int userId, int flightId)
        {
            var booking = DbSet
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Where(b => b.PassengerId == userId && b.FlightId == flightId)
                .FirstOrDefault();

            if (booking == null)
                throw new ApplicationException($"Can't find booking user with id = {userId} on flight with id = {flightId}");

            return booking;
        }

        public void IsThereAccess(int userId, int? bookingId)
        {
            var isThereAccess = false;

            if (bookingId.HasValue)
            {
                isThereAccess = DbSet.Any(b => b.BookingId == bookingId && b.PassengerId == userId);
                if (!isThereAccess)
                    throw new ApplicationException($"User with id = {userId} does not have access to the booking with id = {bookingId}");
            }
            else
            {
                isThereAccess = (userId == Constants.AdminId);
                if (!isThereAccess)
                    throw new ApplicationException($"User with id = {userId} does not have access to work with bookings");
            }
        }
        public string GetOwner(int bookingId)
        {
            var booking = GetBooking(bookingId);
            var passenger = Context.Passengers.Where(p => p.PassengerId == booking.PassengerId).FirstOrDefault();
            return passenger.Email;
        }

        public IEnumerable<Booking> GetAllByFlight(int flightId)
        {
            return DbSet
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Include(b => b.Passenger)
                .Where(t => t.FlightId == flightId);
        }
    }
}
