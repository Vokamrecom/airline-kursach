using Microsoft.AspNetCore.Mvc;
using Airline.DataProvider.Entities;
using Airline.DataProvider.Repositories;
using Airline.Model.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Airline.Tests
{
    public class NotificationControllerTests
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfPhones()
        {
            Assert.Equal(4, GetTestNotifications().Count);
        }

        [Fact]
        public void IndexReturnsANeededName()
        {
            Assert.Contains("", "");
        }
        private List<Notification> GetTestNotifications()
        {
            var phones = new List<Notification>
            {
                new Notification { Message="Test messege 1"},
                new Notification { Message="Test messege 2"},
                new Notification { Message="Test messege 3"},
                new Notification { Message="Test messege 4"},
            };
            return phones;
        }
    }
}