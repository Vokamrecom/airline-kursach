using Airline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airline.DataProvider.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetAll();
        Notification GetNotification(int notificationId);
        IEnumerable<Notification> GetUsersNotifications(int userId);
        void DeleteAllNotifications(int userId);
        void NewNotification(int userId, string message);
        void DeleteNotification(int notificatioId);
    }
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {

        public NotificationRepository(AirlineContext context) : base(context)
        {
        }

        public void DeleteAllNotifications(int userId)
        {
            var list = DbSet
                .Where(b => b.PassengerId == userId)
                .ToList();
            foreach (var n in list)
                DbSet.Remove(n);
            Context.SaveChanges();
        }

        public void DeleteNotification(int notificatioId)
        {
            var notification = DbSet.
                Where(n => n.NotificationId == notificatioId).
                FirstOrDefault();
            if (notification != null)
                DbSet.Remove(notification);
            Context.SaveChanges();
        }

        public Notification GetNotification(int notificationId)
        {
            var notification = DbSet
                .Where(p => p.NotificationId == notificationId)
                .FirstOrDefault();

            if (notification == null)
                throw new ApplicationException($"Can't find notification with id = {notificationId}");

            return notification;
        }

        public IEnumerable<Notification> GetUsersNotifications(int userId)
        {
            return DbSet.Where(n => n.PassengerId == userId);
        }

        public void NewNotification(int userId, string message)
        {
            DbSet.Add(new Notification
            {
                PassengerId = userId,
                Message = message
            });
            Context.SaveChanges();
        }
    }
}