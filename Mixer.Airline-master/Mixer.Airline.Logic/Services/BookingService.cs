using Airline.DataProvider.Entities;
using Airline.DataProvider.Repositories;
using Airline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airline.Logic.Services
{
    public interface IBookingService
    {
        IEnumerable<BookingViewModel> GetAll();
        IEnumerable<BookingViewModel> GetAllByFlight(int flightId);
        IEnumerable<BookingViewModel> GetUsersBooking(int userId);
        bool IsExists(int id);
        BookingViewModel GetById(int bookingId);
        bool IsAlreadyBooked(int userId, int flightId);
        BookingViewModel NewBooking(int userId, int flightId, int amount);
        void CancelBooking(int bookingId);
        void DeleteAllBookings(int userId);
        PaginationModel GetBookingsOffset(PaginationModel model);
        void IsThereAccess(int userId, int? bookingId);
        string GetOwner(int bookingId);
    }
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IEnumerable<BookingViewModel> GetAll()
        {
            var list = _bookingRepository.GetDefaultUsersBookings();

            var bookings = list
                .Select(b => new BookingViewModel
                {
                    BookingId = b.BookingId,
                    PassengerId = b.Passenger.PassengerId,
                    PassengerName = b.Passenger.Name,
                    PassengerSurname = b.Passenger.Surname,
                    PassengerEmail = b.Passenger.Email,
                    FlightId = b.Flight.FlightId,
                    FlightNumber = b.Flight.FlightNumber,
                    DepartureDateTime = b.Flight.DepartureDateTime,
                    DepartureAirport = b.Flight.DepartureAirport,
                    DepartureCity = b.Flight.DepAirport.City,
                    Amount = b.Amount,
                    Price = b.Price,
                    Aircraft = b.Flight.Aircraft.AircraftCode,
                    ArrivalDateTime = b.Flight.ArrivalDateTime,
                    ArrivalAirport = b.Flight.ArrivalAirport,
                    ArrivalCity = b.Flight.ArrAirport.City,
                    IsActive = true
                }).ToList();

            if (bookings.Count == 0)
                throw new ApplicationException("There are no active bookings");

            return bookings;
        }

        public bool IsExists(int id)
        {
            return _bookingRepository.IsExists(id);
        }

        public BookingViewModel GetById(int bookingId)
        {
            var b = _bookingRepository.GetBooking(bookingId);
            if (b == null)
                throw new ApplicationException($"Can't find booking with id = {bookingId}");
            var booking = new BookingViewModel
            {
                BookingId = b.BookingId,
                PassengerId = b.Passenger.PassengerId,
                PassengerName = b.Passenger.Name,
                PassengerSurname = b.Passenger.Surname,
                PassengerEmail = b.Passenger.Email,
                FlightId = b.Flight.FlightId,
                FlightNumber = b.Flight.FlightNumber,
                DepartureDateTime = b.Flight.DepartureDateTime,
                DepartureAirport = b.Flight.DepartureAirport,
                DepartureCity = b.Flight.DepAirport.City,
                Amount = b.Amount,
                Price = b.Price,
                Aircraft = b.Flight.Aircraft.AircraftCode,
                ArrivalDateTime = b.Flight.ArrivalDateTime,
                ArrivalAirport = b.Flight.ArrivalAirport,
                ArrivalCity = b.Flight.ArrAirport.City,
                IsActive = true
            };
            return booking;
        }

        public void CancelBooking(int bookingId)
        {
            _bookingRepository.CancelBooking(bookingId);
        }

        public IEnumerable<BookingViewModel> GetUsersBooking(int userId)
        {
            var list = _bookingRepository.GetUserBookings(userId);
            var usersBookings = list
                .Select(b => new BookingViewModel
                {
                    BookingId = b.BookingId,
                    PassengerId = b.Passenger.PassengerId,
                    PassengerName = b.Passenger.Name,
                    PassengerSurname = b.Passenger.Surname,
                    PassengerEmail = b.Passenger.Email,
                    FlightId = b.Flight.FlightId,
                    FlightNumber = b.Flight.FlightNumber,
                    DepartureDateTime = b.Flight.DepartureDateTime,
                    DepartureAirport = b.Flight.DepartureAirport,
                    DepartureCity = b.Flight.DepAirport.City,
                    Amount = b.Amount,
                    Price = b.Price,
                    Aircraft = b.Flight.Aircraft.AircraftCode,
                    ArrivalDateTime = b.Flight.ArrivalDateTime,
                    ArrivalAirport = b.Flight.ArrivalAirport,
                    ArrivalCity = b.Flight.ArrAirport.City,
                    IsActive = true
                }).ToList();

            return usersBookings;
        }

        public BookingViewModel NewBooking(int userId, int flightId, int amount)
        {
            if (_bookingRepository.IsAlreadyBooked(userId, flightId))
                return new BookingViewModel() { Message = "You already have a booking on this flight" };

            var b = _bookingRepository.NewBooking(userId, flightId, amount);
            BookingViewModel booking = new BookingViewModel
            {
                BookingId = b.BookingId,
                PassengerId = b.Passenger.PassengerId,
                PassengerName = b.Passenger.Name,
                PassengerSurname = b.Passenger.Surname,
                PassengerEmail = b.Passenger.Email,
                FlightId = flightId,
                FlightNumber = b.Flight.FlightNumber,
                DepartureDateTime = b.Flight.DepartureDateTime,
                DepartureAirport = b.Flight.DepartureAirport,
                DepartureCity = b.Flight.DepAirport.City,
                Amount = b.Amount,
                Price = b.Price,
                Aircraft = b.Flight.Aircraft.AircraftCode,
                ArrivalDateTime = b.Flight.ArrivalDateTime,
                ArrivalAirport = b.Flight.ArrivalAirport,
                ArrivalCity = b.Flight.ArrAirport.City,
                IsActive = true,
                Message = null
            };
            return booking;
        }

        public void DeleteAllBookings(int userId)
        {
            _bookingRepository.DeleteAllBookings(userId);
        }

        public bool IsAlreadyBooked(int userId, int flightId)
        {
            return _bookingRepository.IsAlreadyBooked(userId, flightId);
        }

        public PaginationModel GetBookingsOffset(PaginationModel model)
        {
            var list = _bookingRepository.GetUserBookings(model.UserId);
            var bl = _bookingRepository.GetPagedList(list, model.BookingList.PageNumber, model.BookingList.PageSize);

            var bookingList = new PagedList<BookingViewModel>
            {
                RowsCount = bl.RowsCount,
                PagesCount = bl.PagesCount,
                PageSize = bl.PageSize,
                PageNumber = bl.PageNumber,
                Data = bl.Data.Select(b => new BookingViewModel()
                {
                    BookingId = b.BookingId,
                    PassengerId = b.Passenger.PassengerId,
                    PassengerName = b.Passenger.Name,
                    PassengerSurname = b.Passenger.Surname,
                    PassengerEmail = b.Passenger.Email,
                    FlightId = b.Flight.FlightId,
                    FlightNumber = b.Flight.FlightNumber,
                    DepartureDateTime = b.Flight.DepartureDateTime,
                    DepartureAirport = b.Flight.DepartureAirport,
                    DepartureCity = b.Flight.DepAirport.City,
                    Amount = b.Amount,
                    Price = b.Price,
                    Aircraft = b.Flight.Aircraft.AircraftCode,
                    ArrivalDateTime = b.Flight.ArrivalDateTime,
                    ArrivalAirport = b.Flight.ArrivalAirport,
                    ArrivalCity = b.Flight.ArrAirport.City,
                    IsActive = b.IsActive
                }),
                Message = bl.Message
            };
            model.BookingList = bookingList;
            return model;
        }

        public void IsThereAccess(int userId, int? bookingId)
        {
            _bookingRepository.IsThereAccess(userId, bookingId);
        }

        public string GetOwner(int bookingId)
        {
            return _bookingRepository.GetOwner(bookingId);
        }

        public IEnumerable<BookingViewModel> GetAllByFlight(int flightId)
        {
            var list = _bookingRepository.GetAllByFlight(flightId);
            var bookings = list.Select(b => new BookingViewModel
            {
                BookingId = b.BookingId,
                PassengerId = b.Passenger.PassengerId,
                PassengerName = b.Passenger.Name,
                PassengerSurname = b.Passenger.Surname,
                PassengerEmail = b.Passenger.Email,
                FlightId = b.Flight.FlightId,
                FlightNumber = b.Flight.FlightNumber,
                DepartureDateTime = b.Flight.DepartureDateTime,
                DepartureAirport = b.Flight.DepartureAirport,
                DepartureCity = b.Flight.DepAirport.City,
                Amount = b.Amount,
                Price = b.Price,
                Aircraft = b.Flight.Aircraft.AircraftCode,
                ArrivalDateTime = b.Flight.ArrivalDateTime,
                ArrivalAirport = b.Flight.ArrivalAirport,
                ArrivalCity = b.Flight.ArrAirport.City,
                IsActive = true
            }).ToList();
            return bookings;
        }
    }
}
