using Airline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Airline.Tests
{
    public class TicketControllerTests
    {
        [Fact]
        public void TicketIndexReturnsAViewResultWithAListOfPhones()
        {
            Assert.Equal(4, GetTestTickets().Count);
        }

        [Fact]
        public void TickeReturnsANeededName()
        {
            Assert.Contains("", "");
        }
        private List<Ticket> GetTestTickets()
        {
            var tickets = new List<Ticket>
            {
                new Ticket { Name="Max"},
                new Ticket { Name="Vanya"},
                new Ticket { Name="Artme"},
                new Ticket { Name="Petya"},
            };
            return tickets;
        }
    }
}