using Airline.Common;
using Airline.DataProvider.Entities;
using Airline.DataProvider.Repositories;
using Airline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airline.Logic.Services
{
    public interface IPassengerService
    {
        IEnumerable<PassengerViewModel> GetAll();
        PassengerViewModel GetById(int Id);
        PassengerViewModel GetByEmail(string email);
        LoginViewModel Login(LoginViewModel model);
        RegistrationViewModel Registration(RegistrationViewModel model);
        void Delete(int userId);
        EditUserInfoViewModel EditInfo(EditUserInfoViewModel model);
        ChangePasswordViewModel ChangePassword(ChangePasswordViewModel model);
        PaginationModel GetPassengersOffset(PaginationModel model);
        PassengerViewModel UploadProfileImage(int userId, byte[] image);
        bool IsExists(int id);
        bool IsExists(string email);
        bool IsAdmin(string email);
        void IsThereAccess(int userId);
        string GetRole(string email);
    }
     public class PassengerService : IPassengerService
    {
        public readonly IPassengerRepository _passengerRepository;
        public readonly ITicketRepository _ticketRepository;
        public readonly IBookingRepository _bookingRepository;
        public readonly INotificationRepository _notificationRepository;

        public PassengerService(
            IPassengerRepository passengerRepository,
            ITicketRepository ticketRepository,
            IBookingRepository bookingRepository,
            INotificationRepository notificationRepository)
        {
            _passengerRepository = passengerRepository;
            _ticketRepository = ticketRepository;
            _bookingRepository = bookingRepository;
            _notificationRepository = notificationRepository;
        }

        public IEnumerable<PassengerViewModel> GetAll()
        {
            var list = _passengerRepository.GetDefaultUsers();
            var passengers = list
                .Select(p => new PassengerViewModel
            {
                PassengerId = p.PassengerId,
                ProfileImage = p.ProfileImage,
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Email,
                Password = p.Password,
                Role = p.Role
            }).ToList();

            return passengers;
        }

        public PassengerViewModel GetById(int Id)
        {
            var p = _passengerRepository.GetById(Id);
            var passenger = new PassengerViewModel
            {
                PassengerId = p.PassengerId,
                ProfileImage = p.ProfileImage,
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Email,
                Password = p.Password,
                Role = p.Role
            };
            return passenger;
        }

        public PassengerViewModel GetByEmail(string email)
        {
            var p = _passengerRepository.GetByEmail(email);
            var passenger = new PassengerViewModel
            {
                PassengerId = p.PassengerId,
                ProfileImage = p.ProfileImage,
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Email,
                Password = p.Password,
                Role = p.Role
            };
            return passenger;
        }

        public bool IsExists(string email)
        {
            return _passengerRepository.IsExists(email);
        }

        public bool IsExists(int id)
        {
            return _passengerRepository.IsExists(id);
        }

        public LoginViewModel Login(LoginViewModel model)
        {
            if (!_passengerRepository.Login(model.Email, model.Password))
                return new LoginViewModel() { Message = "Incorrect login or password" };
            return model;
        }

        public RegistrationViewModel Registration(RegistrationViewModel model)
        {
            if (_passengerRepository.IsExists(model.Email))
                return new RegistrationViewModel() { Message = "User with the same login is already exist" };
            var p = _passengerRepository
                .Registration(new Passenger {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Password = model.Password,
                    Role = Constants.UserRole
                });
            var passenger = new RegistrationViewModel()
            {
                Id = p.PassengerId,
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Email,
                Password = p.Password,
                Role = p.Role,
                Message = null
            };
            return passenger;
        }

        public EditUserInfoViewModel EditInfo(EditUserInfoViewModel model)
        {
            var passenger = _passengerRepository.GetPassenger(model.Id);
            passenger.Name = model.Name;
            passenger.Surname = model.Surname;
            _passengerRepository.EditInfo(passenger);
            return model;
        }

        public ChangePasswordViewModel ChangePassword(ChangePasswordViewModel model)
        {
            var passenger = _passengerRepository.GetPassenger(model.Id);
            passenger.Password = model.NewPassword;
            _passengerRepository.ChangePassword(passenger);
            return model;
        }

        public bool IsAdmin(string email)
        {
            return _passengerRepository.IsAdmin(email);
        }

        public void Delete(int userId)
        {
            if (!_passengerRepository.IsExists(userId))
                throw new ApplicationException($"Can't find user with id = {userId}");
            _ticketRepository.DeleteAllTickets(userId);
            _bookingRepository.DeleteAllBookings(userId);
            _notificationRepository.DeleteAllNotifications(userId);
            _passengerRepository.Delete(userId);
        }

        public PaginationModel GetPassengersOffset(PaginationModel model)
        {
            var list = _passengerRepository.GetDefaultUsers();
            var pl = _passengerRepository.GetPagedList(list, model.PassengerList.PageNumber, model.PassengerList.PageSize);

            var passengerList = new PagedList<PassengerViewModel>
            {
                RowsCount = pl.RowsCount,
                PagesCount = pl.PagesCount,
                PageSize = pl.PageSize,
                PageNumber = pl.PageNumber,
                Data = pl.Data.Select(p => new PassengerViewModel
                {
                    PassengerId = p.PassengerId,
                    ProfileImage = p.ProfileImage,
                    Name = p.Name,
                    Surname = p.Surname,
                    Email = p.Email,
                    Password = p.Password,
                    Role = p.Role
                }),
                Message = pl.Message
            };

            model.PassengerList = passengerList;
            return model;
        }

        public void IsThereAccess(int userId)
        {
            _passengerRepository.IsThereAccess(userId);
        }

        public PassengerViewModel UploadProfileImage(int userId, byte[] image)
        {
            var p = _passengerRepository.UploadProfileImage(userId, image);
            var passenger = new PassengerViewModel
            {
                PassengerId = p.PassengerId,
                ProfileImage = p.ProfileImage,
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Email,
                Password = p.Password,
                Role = p.Role
            };
            return passenger;
        }

        public string GetRole(string email)
        {
            return _passengerRepository.GetRole(email);
        }
    }
}