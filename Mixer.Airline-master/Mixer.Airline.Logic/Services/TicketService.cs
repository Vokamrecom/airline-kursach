using Airline.DataProvider.Repositories;
using Airline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airline.Logic.Services
{
    public interface ITicketService
    {
        IEnumerable<TicketViewModel> GetAll();
        IEnumerable<TicketViewModel> GetAllByFlight(int flightId);
        void DeleteTicket(int ticketId, string passport);
        TicketViewModel NewTicket(int userId, int flightId, string name, string surname, string passport);
        IEnumerable<TicketViewModel> GetUsersTickets(int userId);
        void DeleteAllTickets(int userId);
        PaginationModel GetTicketsOffset(PaginationModel model);
        void IsThereAccess(int userId, int? ticketId);
        bool IsExists(int id);
        string GetOwner(int ticketId);
    }
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(
            ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public void DeleteAllTickets(int userId)
        {
            _ticketRepository.DeleteAllTickets(userId);
        }

        public void DeleteTicket(int ticketId, string passport)
        {
            _ticketRepository.DeleteTicket(ticketId, passport);
        }

        public IEnumerable<TicketViewModel> GetAll()
        {
            var list = _ticketRepository.GetDefaultUsersTickets();
            var tickets = list.Select(t => new TicketViewModel
            {
                TicketId = t.TicketId,
                Buyer = t.Buyer.PassengerId,
                PassengerName = t.Name,
                PassengerSurname = t.Surname,
                Passport = t.Passport,
                FlightId = t.Flight.FlightId,
                FlightNumber = t.Flight.FlightNumber,
                DepartureDateTime = t.Flight.DepartureDateTime,
                DepartureAirport = t.Flight.DepartureAirport,
                DepartureCity = t.Flight.DepAirport.City,
                Price = t.Flight.Price,
                Aircraft = t.Flight.Aircraft.AircraftCode,
                ArrivalDateTime = t.Flight.ArrivalDateTime,
                ArrivalAirport = t.Flight.ArrivalAirport,
                ArrivalCity = t.Flight.ArrAirport.City
            }).ToList();
            return tickets;
        }

        public PaginationModel GetTicketsOffset(PaginationModel model)
        {
            var list = _ticketRepository.GetUsersTickets(model.UserId);
            var tl = _ticketRepository.GetPagedList(list, model.TicketList.PageNumber, model.TicketList.PageSize);

            var ticketList = new PagedList<TicketViewModel>
            {
                RowsCount = tl.RowsCount,
                PagesCount = tl.PagesCount,
                PageSize = tl.PageSize,
                PageNumber = tl.PageNumber,
                Data = tl.Data.Select(t => new TicketViewModel
                {
                    TicketId = t.TicketId,
                    Buyer = t.Buyer.PassengerId,
                    PassengerName = t.Name,
                    PassengerSurname = t.Surname,
                    Passport = t.Passport,
                    FlightId = t.Flight.FlightId,
                    FlightNumber = t.Flight.FlightNumber,
                    DepartureDateTime = t.Flight.DepartureDateTime,
                    DepartureAirport = t.Flight.DepartureAirport,
                    DepartureCity = t.Flight.DepAirport.City,
                    Price = t.Flight.Price,
                    Aircraft = t.Flight.Aircraft.AircraftCode,
                    ArrivalDateTime = t.Flight.ArrivalDateTime,
                    ArrivalAirport = t.Flight.ArrivalAirport,
                    ArrivalCity = t.Flight.ArrAirport.City
                }),
                Message = tl.Message
            };
            model.TicketList = ticketList;
            return model;
        }

        public IEnumerable<TicketViewModel> GetUsersTickets(int userId)
        {
            var list = _ticketRepository.GetUsersTickets(userId);
            var usersTickets = list.Select(t => new TicketViewModel
            {
                TicketId = t.TicketId,
                Buyer = t.Buyer.PassengerId,
                PassengerName = t.Name,
                PassengerSurname = t.Surname,
                Passport = t.Passport,
                FlightId = t.Flight.FlightId,
                FlightNumber = t.Flight.FlightNumber,
                DepartureDateTime = t.Flight.DepartureDateTime,
                DepartureAirport = t.Flight.DepartureAirport,
                DepartureCity = t.Flight.DepAirport.City,
                Price = t.Flight.Price,
                Aircraft = t.Flight.Aircraft.AircraftCode,
                ArrivalDateTime = t.Flight.ArrivalDateTime,
                ArrivalAirport = t.Flight.ArrivalAirport,
                ArrivalCity = t.Flight.ArrAirport.City
            }).ToList();
            return usersTickets;
        }

        public bool IsExists(int id)
        {
            return _ticketRepository.IsExists(id);
        }

        public void IsThereAccess(int userId, int? ticketId)
        {
            _ticketRepository.IsThereAccess(userId, ticketId);
        }

        public TicketViewModel NewTicket(int userId, int flightId, string name, string surname, string passport)
        {
            var t = _ticketRepository.NewTicket(userId, flightId, name, surname, passport);
            var ticket = new TicketViewModel
            {
                TicketId = t.TicketId,
                Buyer = t.BuyerId,
                PassengerName = t.Name,
                PassengerSurname = t.Surname,
                Passport = t.Passport,
                FlightId = t.Flight.FlightId,
                FlightNumber = t.Flight.FlightNumber,
                DepartureDateTime = t.Flight.DepartureDateTime,
                DepartureAirport = t.Flight.DepartureAirport,
                DepartureCity = t.Flight.DepAirport.City,
                Price = t.Flight.Price,
                Aircraft = t.Flight.Aircraft.AircraftCode,
                ArrivalDateTime = t.Flight.ArrivalDateTime,
                ArrivalAirport = t.Flight.ArrivalAirport,
                ArrivalCity = t.Flight.ArrAirport.City
            };
            return ticket;
        }

        public string GetOwner(int ticketId)
        {
            return _ticketRepository.GetOwner(ticketId);
        }

        public IEnumerable<TicketViewModel> GetAllByFlight(int flightId)
        {
            var list = _ticketRepository.GetAllByFlight(flightId);
            var tickets = list.Select(t => new TicketViewModel
            {
                TicketId = t.TicketId,
                Buyer = t.Buyer.PassengerId,
                PassengerName = t.Name,
                PassengerSurname = t.Surname,
                Passport = t.Passport,
                FlightId = t.Flight.FlightId,
                FlightNumber = t.Flight.FlightNumber,
                DepartureDateTime = t.Flight.DepartureDateTime,
                DepartureAirport = t.Flight.DepartureAirport,
                DepartureCity = t.Flight.DepAirport.City,
                Price = t.Flight.Price,
                Aircraft = t.Flight.Aircraft.AircraftCode,
                ArrivalDateTime = t.Flight.ArrivalDateTime,
                ArrivalAirport = t.Flight.ArrivalAirport,
                ArrivalCity = t.Flight.ArrAirport.City
            }).ToList();
            return tickets;
        }
    }
}
