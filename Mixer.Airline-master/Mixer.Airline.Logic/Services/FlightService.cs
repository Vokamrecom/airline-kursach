using Airline.DataProvider.Entities;
using Airline.DataProvider.Repositories;
using Airline.Model.Models;
using Airline.Model.PaginationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airline.Logic.Services
{
    public interface IFlightService
    {
        IEnumerable<FlightViewModel> GetAll();
        FlightViewModel GetById(int flightId);
        FlightPaginationModel GetNeededFlightsOffset(FlightPaginationModel model);
        PaginationModel GetFlightsOffset(PaginationModel model);
        NewFlightViewModel NewFlight(NewFlightViewModel model);
        void CancelFlight(int flightId);
        bool IsExists(int flightId);
        void IsThereAccess(int userId);
        void GhangeFlightStatus(int flightId, int delay);
    }
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ITicketRepository _ticketRepository;

        public FlightService(
            IFlightRepository flightRepository,
            IBookingRepository bookingRepository,
            ITicketRepository ticketRepository)
        {
            _flightRepository = flightRepository;
            _bookingRepository = bookingRepository;
            _ticketRepository = ticketRepository;
        }

        public void CancelFlight(int FlightId)
        {
            foreach (var t in _ticketRepository.GetAll().Where(t => t.FlightId == FlightId))
                _ticketRepository.DeleteTicket(t.TicketId, null);
            foreach (var t in _bookingRepository.GetAll().Where(t => t.FlightId == FlightId))
                _bookingRepository.CancelBooking(t.BookingId);
            _flightRepository.CancelFlight(FlightId);
        }

        public IEnumerable<FlightViewModel> GetAll()
        {
            var list = _flightRepository.GetAll();
            var flights = list.Select(f => new FlightViewModel
            {
                FlightId = f.FlightId,
                FlightNumber = f.FlightNumber,
                DepartureDateTime = f.DepartureDateTime,
                DepartureAirport = f.DepAirport.AirportCode,
                DepartureCity = f.DepAirport.City,
                Aircraft = f.Aircraft.Model,
                FreePlaces = f.Aircraft.TotalPlaces - _flightRepository.GetIntentPlaces(f.FlightId),
                ArrivalDateTime = f.ArrivalDateTime,
                ArrivalAirport = f.ArrAirport.AirportCode,
                ArrivalCity = f.ArrAirport.City,
                Price = f.Price
            }).ToList();

            return flights;
        }

        public FlightViewModel GetById(int flightId)
        {
            var f = _flightRepository.GetFlight(flightId);
            var flight = new FlightViewModel
            {
                FlightId = f.FlightId,
                FlightNumber = f.FlightNumber,
                DepartureDateTime = f.DepartureDateTime,
                DepartureAirport = f.DepAirport.AirportCode,
                DepartureCity = f.DepAirport.City,
                Aircraft = f.Aircraft.Model,
                FreePlaces = f.Aircraft.TotalPlaces - _flightRepository.GetIntentPlaces(f.FlightId),
                ArrivalDateTime = f.ArrivalDateTime,
                ArrivalAirport = f.ArrAirport.AirportCode,
                ArrivalCity = f.ArrAirport.City,
                Price = f.Price
            };
            return flight;
        }

        public FlightPaginationModel GetNeededFlightsOffset(FlightPaginationModel model)
        {
            var list = _flightRepository.GetNeededFlights(model.From, model.To, model.Date, model.GetNearby, model.OrderAscending, model.SortByDate);
            var fl = _flightRepository.GetPagedList(list, model.FlightList.PageNumber, model.FlightList.PageSize);
            var flightList = new PagedList<FlightViewModel>
            {
                RowsCount = fl.RowsCount,
                PagesCount = fl.PagesCount,
                PageSize = fl.PageSize,
                PageNumber = fl.PageNumber,
                Data = fl.Data.Select(f => new FlightViewModel
                {
                    FlightId = f.FlightId,
                    FlightNumber = f.FlightNumber,
                    DepartureDateTime = f.DepartureDateTime,
                    DepartureAirport = f.DepAirport.AirportCode,
                    DepartureCity = f.DepAirport.City,
                    Aircraft = f.Aircraft.Model,
                    FreePlaces = f.Aircraft.TotalPlaces - _flightRepository.GetIntentPlaces(f.FlightId),
                    ArrivalDateTime = f.ArrivalDateTime,
                    ArrivalAirport = f.ArrAirport.AirportCode,
                    ArrivalCity = f.ArrAirport.City,
                    Price = f.Price
                }).ToList(),
                Message = fl.Message
            };
            model.FlightList = flightList;
            return model;
        }

        public PaginationModel GetFlightsOffset(PaginationModel model)
        {
            var list = _flightRepository.GetNeededFlights(null, null, null, false, true, true);
            var fl = _flightRepository.GetPagedList(list, model.FlightList.PageNumber, model.FlightList.PageSize);

            var flightList = new PagedList<FlightViewModel>
            {
                RowsCount = fl.RowsCount,
                PagesCount = fl.PagesCount,
                PageSize = fl.PageSize,
                PageNumber = fl.PageNumber,
                Data = fl.Data.Select(f => new FlightViewModel
                {
                    FlightId = f.FlightId,
                    FlightNumber = f.FlightNumber,
                    DepartureDateTime = f.DepartureDateTime,
                    DepartureAirport = f.DepAirport.AirportCode,
                    DepartureCity = f.DepAirport.City,
                    Aircraft = f.Aircraft.Model,
                    FreePlaces = f.Aircraft.TotalPlaces - _flightRepository.GetIntentPlaces(f.FlightId),
                    ArrivalDateTime = f.ArrivalDateTime,
                    ArrivalAirport = f.ArrAirport.AirportCode,
                    ArrivalCity = f.ArrAirport.City,
                    Price = f.Price
                }).ToList(),
                Message = fl.Message
            };
            model.FlightList = flightList;
            return model;
        }

        public NewFlightViewModel NewFlight(NewFlightViewModel model)
        {
            _flightRepository.NewFlight(
                model.FlightNumber,
                model.DepartureDate,
                model.DepartureTime,
                model.DepartureAirport,
                model.AircraftCode,
                model.ArrivalDate,
                model.ArrivalTime,
                model.ArrivalAirport,
                model.Price
            );
            return model;
        }

        public bool IsExists(int flightId)
        {
            return _flightRepository.IsExists(flightId);
        }

        public void IsThereAccess(int userId)
        {
            _flightRepository.IsThereAccess(userId);
        }

        public void GhangeFlightStatus(int flightId, int delay)
        {
            _flightRepository.GhangeFlightStatus(flightId, delay);
        }
    }
}
