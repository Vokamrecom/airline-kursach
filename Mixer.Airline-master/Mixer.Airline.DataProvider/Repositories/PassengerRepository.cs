using Microsoft.EntityFrameworkCore;
using Airline.Common;
using Airline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airline.DataProvider.Repositories
{
    public interface IPassengerRepository : IRepository<Passenger>
    {
        Passenger GetByEmail(string email);
        Passenger GetPassenger(int passengerId);
        bool Login(string email, string password);
        Passenger Registration(Passenger model);
        Passenger Delete(int userId);
        Passenger EditInfo(Passenger model);
        Passenger ChangePassword(Passenger model);
        IEnumerable<Passenger> GetDefaultUsers();
        IEnumerable<Passenger> GetPassengersOffset(int pageNumber, int pageSize);
        Passenger UploadProfileImage(int userId, byte[] image);
        bool IsExists(string email);
        bool IsAdmin(string email);
        void IsThereAccess(int userId);
        string GetRole(string email);
    }
    public class PassengerRepository : BaseRepository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(AirlineContext context) : base(context)
        {
        }

        public bool Login(string email, string password)
        {
            string passwordHash = Crypto.Sha256(password + email);
            return DbSet.Any(p => p.Email == email && p.Password == passwordHash);
        }

        public Passenger Registration(Passenger model)
        {
            var pass = DbSet.Add(new Passenger
            {
                ProfileImage = null,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Password = Crypto.Sha256(model.Password + model.Email),
                Role = Constants.UserRole
            });
            Context.SaveChanges();
            var passenger = DbSet.Where(p => p.Email == model.Email).FirstOrDefault();
            return passenger;
        }

        public IEnumerable<Passenger> GetDefaultUsers()
        {
            return DbSet.Where(p => p.Role != Constants.AdminRole);
        }

        public string GetRole(string email)
        {
            return DbSet.Where(p => p.Email == email).FirstOrDefault().Role;
        }

        public Passenger GetPassenger(int passengerId)
        {
            var passenger = DbSet
                .Where(p => p.PassengerId == passengerId)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with id = {passengerId}");

            return passenger;
        }

        public Passenger GetByEmail(string email)
        {
            var passenger = DbSet
                .Where(p => p.Email == email)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with email = {email}");

            return passenger;
        }
        public Passenger Delete(int userId)
        {
            var passenger = DbSet
                .Where(p => p.PassengerId == userId)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with id = {userId}");

            DbSet.Remove(passenger);
            Context.SaveChanges();
            return passenger;
        }

        public Passenger EditInfo(Passenger model)
        {
            var passenger = DbSet
                .Where(p => p.PassengerId == model.PassengerId)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with id = {model.PassengerId}");

            passenger.Name = model.Name;
            passenger.Surname = model.Surname;
            Context.SaveChanges();
            return passenger;
        }

        public Passenger ChangePassword(Passenger model)
        {
            var passenger = DbSet
                .Where(p => p.PassengerId == model.PassengerId)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with id = {model.PassengerId}");

            passenger.Password = Crypto.Sha256(model.Password + passenger.Email);
            Context.SaveChanges();
            return passenger;
        }

        public bool IsAdmin(string email)
        {
            var passenger = DbSet
                .Where(p => p.Email == email)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with email = {email}");

            return passenger.Role == Constants.AdminRole ? true : false;
        }

        public bool IsExists(string email)
        {
            return DbSet.Any(p => p.Email == email);
        }

        public IEnumerable<Passenger> GetPassengersOffset(int pageNumber, int pageSize)
        {
            var passengers = DbSet
                .Where(p => p.Role != Constants.AdminRole)
                .Skip((pageNumber) * pageSize)
                .Take(pageSize)
                .OrderBy(t => t.PassengerId);

            return passengers;
        }

        public void IsThereAccess(int userId)
        {
            if (userId != Constants.AdminId)
                throw new ApplicationException($"User with id = {userId} does not have access to work with passengers");
        }

        public Passenger UploadProfileImage(int userId, byte[] image)
        {
            var passenger = DbSet
                .Where(p => p.PassengerId == userId)
                .FirstOrDefault();

            if (passenger == null)
                throw new ApplicationException($"Can't find passenger with id = {userId}");

            passenger.ProfileImage = image;
            Context.SaveChanges();
            return passenger;
        }
    }
}
