using Airline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Airline.Tests
{
    public class BookingControllerTests
    {
        [Fact]
        public void BookingIndexReturnsAViewResultWithAListOfPhones()
        {
            Assert.Equal(4, GetTestBookings().Count);
        }

        [Fact]
        public void BookingReturnsANeededName()
        {
            Assert.Contains("", "");
        }
        private List<Booking> GetTestBookings()
        {
            var bookings = new List<Booking>
            {
                new Booking { Price = 1000},
                new Booking { Price = 1000},
                new Booking { Price = 1000},
                new Booking { Price = 1000}
            };
            return bookings;
        }
    }
}