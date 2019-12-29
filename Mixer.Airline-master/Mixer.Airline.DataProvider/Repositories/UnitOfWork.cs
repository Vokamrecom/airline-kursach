using System;
using System.Collections.Generic;
using System.Text;

namespace Airline.DataProvider.Repositories
{
    public interface IUnitOfWork
    {
        IAircraftRepository Aircrafts { get; }
        IAirportRepository Airports { get; }
        IBookingRepository Bookings { get; }
        IFlightRepository Flights { get; }
        INotificationRepository Notifications { get; }
        IPassengerRepository Passengers { get; }
        void Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private AirlineContext _context;

        public UnitOfWork(AirlineContext context)
        {
            _context = context;
        }

        private AircraftRepository _aircraftRepository;
        private AirportRepository _airportRepository;
        private BookingRepository _bookingRepository;
        private FlightRepository _flightRepository;
        private NotificationRepository _notificationRepository;
        private PassengerRepository _passengerRepository;
        private TicketRepository _ticketRepository;

        public IAircraftRepository Aircrafts
        {
            get
            {
                if (_aircraftRepository == null)
                {
                    _aircraftRepository = new AircraftRepository(_context);
                }
                return _aircraftRepository;
            }
        }
        public IAirportRepository Airports
        {
            get
            {
                if (_airportRepository == null)
                {
                    _airportRepository = new AirportRepository(_context);
                }
                return _airportRepository;
            }
        }

        public IBookingRepository Bookings
        {
            get
            {
                if (_bookingRepository == null)
                {
                    _bookingRepository = new BookingRepository(_context);
                }
                return _bookingRepository;
            }
        }

        public IFlightRepository Flights
        {
            get
            {
                if (_flightRepository == null)
                {
                    _flightRepository = new FlightRepository(_context);
                }
                return _flightRepository;
            }
        }

        public INotificationRepository Notifications
        {
            get
            {
                if (_notificationRepository == null)
                {
                    _notificationRepository = new NotificationRepository(_context);
                }
                return _notificationRepository;
            }
        }

        public IPassengerRepository Passengers
        {
            get
            {
                if (_passengerRepository == null)
                {
                    _passengerRepository = new PassengerRepository(_context);
                }
                return _passengerRepository;
            }
        }

        public ITicketRepository Tickets
        {
            get
            {
                if (_ticketRepository == null)
                {
                    _ticketRepository = new TicketRepository(_context);
                }
                return _ticketRepository;
            }
        }

        public void Save() => _context.SaveChanges();
    }
}
