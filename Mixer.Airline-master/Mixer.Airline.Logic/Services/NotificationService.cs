using Airline.DataProvider.Repositories;
using Airline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airline.Logic.Services
{
    public interface INotificationService
    {
        IEnumerable<NotificationViewModel> GetAll();
        NotificationView GetUsersNotifications(string email);
        void DeleteAllNotifications(int userId);
        void NewNotification(string email, int ticketId, int bookingId, int flightId, int delay);
        void DeleteNotification(int notificatioId);
    }
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFlightRepository _flightRepository;

        public NotificationService(
            INotificationRepository notificationRepository,
            IBookingRepository bookingRepository,
            ITicketRepository ticketRepository,
            IPassengerRepository passengerRepository,
            IFlightRepository flightRepository)
        {
            _notificationRepository = notificationRepository;
            _bookingRepository = bookingRepository;
            _ticketRepository = ticketRepository;
            _passengerRepository = passengerRepository;
            _flightRepository = flightRepository;
        }

        public void DeleteAllNotifications(int userId)
        {
            _notificationRepository.DeleteAllNotifications(userId);
        }

        public void DeleteNotification(int notificatioId)
        {
            _notificationRepository.DeleteNotification(notificatioId);
        }

        public IEnumerable<NotificationViewModel> GetAll()
        {
            var list = _notificationRepository.GetAll();
            var notifications = list.Select(n => new NotificationViewModel
            {
                NotificationId = n.NotificationId,
                UserId = n.PassengerId,
                Message = n.Message
            }).ToList();
            return notifications;
        }

        public NotificationView GetUsersNotifications(string email)
        {
            var list = _notificationRepository.GetUsersNotifications(_passengerRepository.GetByEmail(email).PassengerId);
            var notifications = list.Select(n => new NotificationViewModel
            {
                NotificationId = n.NotificationId,
                UserId = n.PassengerId,
                Message = n.Message
            }).ToList();
            var notView = new NotificationView
            {
                List = notifications,
                Count = notifications.Count()
            };
            return notView;
        }

        public void NewNotification(string email, int ticketId = 0, int bookingId = 0, int flightId = 0, int delay = 0)
        {
            string message = "";
            var user = _passengerRepository.GetByEmail(email);
            if (ticketId != 0)
            {
                var ticket = _ticketRepository.GetTicket(ticketId);
                var flight = _flightRepository.GetFlight(ticket.FlightId);
                message += String.Format("Dear {0} {1}, we apologize but your ticket on aircraft which is departing from {2} at {3}" +
                    " to {4} has been deleted by the moderator.",
                    user.Surname, user.Name, flight.DepartureAirport, flight.DepartureDateTime, flight.ArrivalAirport);
            }
            else if (bookingId != 0)
            {
                var booking = _bookingRepository.GetBooking(bookingId);
                var flight = _flightRepository.GetFlight(booking.FlightId);
                message += String.Format("Dear {0} {1}, we apologize but your booking on flight from {2} to {3} at {4} has been deleted by the moderator.",
                    user.Surname, user.Name, flight.DepartureAirport, flight.ArrivalAirport, flight.DepartureDateTime);
            }
            else if (flightId != 0 && delay != 0)
            {
                var flight = _flightRepository.GetFlight(flightId);
                message += String.Format("Dear {0} {1}, we apologize but flight from {2} to {3}  {4} has been delayed by {5} hours and now departing at {6}",
                    user.Surname, user.Name, flight.DepartureAirport, flight.ArrivalAirport, flight.DepartureDateTime.ToString("d"), delay, flight.DepartureDateTime.ToString("t"));
            }
            else if (flightId != 0 && delay == 0)
            {
                var flight = _flightRepository.GetFlight(flightId);
                message += String.Format("Dear {0} {1}, we apologize but flight from {2} to {3} {4} has been deleted by moderator and all your bookings and tickets have been canceled.",
                    user.Surname, user.Name, flight.DepartureAirport, flight.ArrivalAirport, flight.DepartureDateTime.ToString("d"), delay, flight.DepartureDateTime.ToString("t"));
            }
            _notificationRepository.NewNotification(user.PassengerId, message);
        }
    }
}
