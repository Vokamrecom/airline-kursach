using Microsoft.EntityFrameworkCore;
using Airline.Common;
using Airline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Airline.DataProvider.Repositories
{
    public interface IFlightRepository : IRepository<Flight>
    {
        IEnumerable<Flight> GetFlights();
        Flight GetFlight(int flightId);
        IEnumerable<Flight> GetNeededFlights(string from, string to, DateTime? date, bool getNearby, bool orderAscending, bool sortByDate);
        Flight NewFlight(string flightNumber, DateTime departureDate, string departureTime, string departureAirport, string aircraftCode, DateTime arrivalDate, string arrivalTime, string arrivalAirport, int price);
        void CancelFlight(int flightId);
        void IsThereAccess(int userId);
        int GetIntentPlaces(int flightId);
        void GhangeFlightStatus(int flightId, int delay);
    }
    public class FlightRepository : BaseRepository<Flight>, IFlightRepository
    {
        public FlightRepository(AirlineContext context) : base(context)
        {
        }

        public void CancelFlight(int flightId)
        {
            var flight = DbSet
                .Where(f => f.FlightId == flightId)
                .FirstOrDefault();

            if (flight == null)
                throw new ApplicationException($"Can't find flight with id = {flightId}");

            DbSet.Remove(flight);
            Context.SaveChanges();
        }

        public IEnumerable<Flight> GetFlights()
        {
            return DbSet
                .Include(f => f.Aircraft)
                .Include(f => f.DepAirport)
                .Include(f => f.ArrAirport);
        }


        public Flight GetFlight(int flightId)
        {
            var flight = DbSet
                .Include(f => f.Aircraft)
                .Include(f => f.DepAirport)
                .Include(f => f.ArrAirport)
                .Where(f => f.FlightId == flightId)
                .FirstOrDefault();

            if (flight == null)
                throw new ApplicationException($"Can't find flight with id = {flightId}");

            return flight;
        }

        public IEnumerable<Flight> GetNeededFlights(string from, string to, DateTime? date, bool getNearby, bool orderAscending, bool sortByDate)
        {
            var flights = (IEnumerable<Flight>)DbSet
                .Include(f => f.Aircraft)
                .Include(f => f.DepAirport)
                .Include(f => f.ArrAirport);

            if (from != null)
                flights = flights
                    .Where(f => f.DepAirport.City.ToLower().Contains(from.ToLower()) || f.DepAirport.AirportCode.ToLower().Contains(from.ToLower()));

            if (to != null)
                flights = flights
                    .Where(f => f.ArrAirport.City.ToLower().Contains(to.ToLower()) || f.ArrAirport.AirportCode.ToLower().Contains(to.ToLower()));

            if (!getNearby && date.HasValue)
                flights = flights.Where(f =>
                    f.DepartureDateTime.Day == date.Value.Day &&
                    f.DepartureDateTime.Month == date.Value.Month &&
                    f.DepartureDateTime.Year == date.Value.Year);

            if(getNearby && date.HasValue)
            {
                var flightsAfter = flights.Where(f =>
                    f.DepartureDateTime.Day >= date.Value.Day || f.DepartureDateTime.Month >= date.Value.Month).Take(3);
                var flightsBefore = flights.Where(f =>
                    f.DepartureDateTime.Day <= date.Value.Day || f.DepartureDateTime.Month <= date.Value.Month).TakeLast(3);
                flights = flightsBefore.Concat(flightsAfter);
            }

            if (sortByDate)
                if (orderAscending)
                    flights = flights.OrderBy(f => f.DepartureDateTime);
                else
                    flights = flights.OrderByDescending(f => f.DepartureDateTime);
            else
                if (orderAscending)
                flights = flights.OrderBy(f => f.Price);
            else
                flights = flights.OrderByDescending(f => f.Price);

            return flights;
        }

        public Flight NewFlight(string flightNumber, DateTime departureDate, string departureTime, string departureAirport, string aircraftCode, DateTime arrivalDate, string arrivalTime, string arrivalAirport, int price)
        {
            var flight = new Flight()
            {
                FlightNumber = flightNumber,
                DepartureDateTime = new DateTime(
                    departureDate.Year,
                    departureDate.Month,
                    departureDate.Day,
                    Convert.ToInt32(departureTime.Split(new char[] { ':' })[0]),
                    Convert.ToInt32(departureTime.Split(new char[] { ':' })[1]),
                    0
                ),
                DepartureAirport = departureAirport,
                AircraftCode = aircraftCode,
                TotalPlaces = Context.Aircrafts.Where(a => a.AircraftCode == aircraftCode).FirstOrDefault().TotalPlaces,
                ArrivalDateTime = new DateTime(
                    arrivalDate.Year,
                    arrivalDate.Month,
                    arrivalDate.Day,
                    Convert.ToInt32(arrivalTime.Split(new char[] { ':' })[0]),
                    Convert.ToInt32(arrivalTime.Split(new char[] { ':' })[1]),
                    0
                ),
                ArrivalAirport = arrivalAirport,
                Price = price
            };

            DbSet.Add(flight);
            Context.SaveChanges();
            return flight;
        }

        public void IsThereAccess(int userId)
        {
            if (userId != Constants.AdminId)
                throw new ApplicationException($"User with id = {userId} does not have access to work with flights");
        }

        public int GetIntentPlaces(int flightId)
        {
            int x = 0;
            var flight = GetFlight(flightId);

            var bookings = Context.Bookings.Where(b => b.FlightId == flightId && b.IsActive == true);
            var tickets = Context.Tickets.Where(t => t.FlightId == flightId);
            foreach (var b in bookings)
                x += b.Amount;
            foreach (var t in tickets)
                x += 1;
            return x;
        }

        public void GhangeFlightStatus(int flightId, int delay)
        {
            var flight = DbSet
               .Where(f => f.FlightId == flightId)
               .FirstOrDefault();

            if (flight == null)
                throw new ApplicationException($"Can't find flight with id = {flightId}");

            flight.DepartureDateTime = flight.DepartureDateTime.AddHours(delay);
            flight.ArrivalDateTime = flight.ArrivalDateTime.AddHours(delay);

            Context.SaveChanges();
        }
    }
}