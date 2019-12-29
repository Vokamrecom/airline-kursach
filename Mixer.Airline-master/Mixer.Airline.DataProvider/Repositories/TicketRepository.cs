using Microsoft.EntityFrameworkCore;
using Airline.Common;
using Airline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airline.DataProvider.Repositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        bool IsExists(int userId, int flightId);
        IEnumerable<Ticket> GetAllByFlight(int flightId);
        Ticket NewTicket(int userId, int flightId, string name, string surname, string passport);
        Ticket GetTicket(int userId, int flightId);
        Ticket GetTicket(int ticketId);
        void DeleteTicket(int ticketId, string passport);
        IEnumerable<Ticket> GetDefaultUsersTickets();
        IEnumerable<Ticket> GetUsersTickets(int? userId);
        void DeleteAllTickets(int userId);
        void IsThereAccess(int userId, int? ticketId);
        string GetOwner(int ticketId);
    }
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(AirlineContext context) : base(context)
        {
        }

        public IEnumerable<Ticket> GetDefaultUsersTickets()
        {
            return DbSet
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Include(t => t.Buyer)
                .Where(t => t.Buyer.Role != Constants.AdminRole);
        }

        public Ticket NewTicket(int userId, int flightId, string name, string surname, string passport)
        {
            var tic = DbSet.Add(new Ticket
            {
                BuyerId = userId,
                FlightId = flightId,
                Name = name,
                Surname = surname,
                Passport = passport
            });

            var booking = Context.Bookings
                .Where(b => b.PassengerId == userId && b.FlightId == flightId)
                .FirstOrDefault();

            if (booking == null)
                throw new ApplicationException($"Can't find booking user with id = {userId} on flight with id = {flightId}");

            booking.IsActive = false;
            booking.Amount--;
            Context.SaveChanges();
            var ticket = DbSet
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Where(t => t.BuyerId == userId &&
                    t.FlightId == flightId &&
                    t.Name == name &&
                    t.Surname == surname &&
                    t.Passport == passport
                )
                .FirstOrDefault();

            if (ticket == null)
                throw new ApplicationException($"Can't find ticket user with id = {userId} on flight with id = {flightId}");

            return ticket;
        }

        public IEnumerable<Ticket> GetUsersTickets(int? userId)
        {
            var tickets = (IEnumerable<Ticket>)DbSet
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Include(t => t.Buyer);

            if (userId.HasValue)
                tickets = tickets.Where(t => t.BuyerId == userId);
            else
                tickets = tickets.Where(t => t.Buyer.Role != Constants.AdminRole);

            tickets = tickets
                .OrderBy(t => t.TicketId);

            return tickets;
        }

        public Ticket GetTicket(int userId, int flightId)
        {
            var ticket = DbSet
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Include(t => t.Buyer)
                .Where(t => t.BuyerId == userId && t.FlightId == flightId)
                .FirstOrDefault();

            if (ticket == null)
                throw new ApplicationException($"Can't find ticket user with id = {userId} on flight with id = {flightId}");

            return ticket;
        }

        public Ticket GetTicket(int ticketId)
        {
            var ticket = DbSet
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Include(t => t.Buyer)
                .Where(t => t.TicketId == ticketId)
                .FirstOrDefault();

            if (ticket == null)
                throw new ApplicationException($"Can't find ticket with id = {ticketId}");

            return ticket;
        }

        public bool IsExists(int userId, int flightId)
        {
            return DbSet.Any(p => p.BuyerId == userId && p.FlightId == flightId);
        }

        public void DeleteAllTickets(int userId)
        {
            var list = DbSet
                .Where(b => b.BuyerId == userId)
                .ToList();

            foreach (var ticket in list)
            {
                var booking = Context.Bookings
                    .Where(b => b.PassengerId == ticket.BuyerId && b.FlightId == ticket.FlightId)
                    .FirstOrDefault();

                if (booking == null)
                    throw new ApplicationException($"Can't find booking user with id = {userId} on flight with id = {ticket.FlightId}");

                booking.IsActive = true;
                booking.Amount++;
                DbSet.Remove(ticket);
            }
            Context.SaveChanges();
        }

        public IEnumerable<Ticket> GetTicketsOffset(int? userId, int pageNumber, int pageSize)
        {
            var tickets = (IEnumerable<Ticket>)DbSet
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Include(t => t.Buyer);

            if (userId.HasValue)
                tickets = tickets.Where(t => t.BuyerId == userId);
            else
                tickets = tickets.Where(t => t.Buyer.Role != Constants.AdminRole);

            tickets = tickets
                .OrderBy(t => t.TicketId)
                .Skip((pageNumber) * pageSize)
                .Take(pageSize);

            return tickets;
        }

        public void DeleteTicket(int ticketId, string passport)
        {
            var ticket = DbSet
                .Where(t => t.TicketId == ticketId)
                .FirstOrDefault();

            if (passport != null)
                ticket = DbSet
                .Where(t => t.TicketId == ticketId && t.Passport == passport)
                .FirstOrDefault();

            if (ticket == null)
                throw new ApplicationException($"Can't find ticket with id = {ticketId} for passenger with passport {passport}");

            var booking = Context.Bookings
                .Where(b => b.PassengerId == ticket.BuyerId && b.FlightId == ticket.FlightId)
                .FirstOrDefault();

            if (booking != null)
            {
                booking.IsActive = true;
                booking.Amount++;
            }
            //throw new ApplicationException($"Can't find booking for ticket id = {ticketId}, buyer = {ticket.Buyer}, flight = {ticket.FlightId}");
            DbSet.Remove(ticket);
            Context.SaveChanges();
        }

        public void IsThereAccess(int userId, int? ticketId)
        {
            var isThereAccess = false;

            if (ticketId.HasValue)
            {
                isThereAccess = DbSet.Any(t => t.TicketId == ticketId && t.BuyerId == userId);
                if (!isThereAccess)
                    throw new ApplicationException($"User with id = {userId} does not have access to the ticket with id = {ticketId}");
            }
            else
            {
                isThereAccess = (userId == Constants.AdminId);
                if (!isThereAccess)
                    throw new ApplicationException($"User with id = {userId} does not have access to work with tickets");
            }
        }

        public string GetOwner(int ticketId)
        {
            var ticket = GetTicket(ticketId);
            var passenger = Context.Passengers.Where(p => p.PassengerId == ticket.BuyerId).FirstOrDefault();
            return passenger.Email;
        }

        public IEnumerable<Ticket> GetAllByFlight(int flightId)
        {
            return DbSet
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Include(t => t.Buyer)
                .Where(t => t.FlightId == flightId);
        }
    }
}
