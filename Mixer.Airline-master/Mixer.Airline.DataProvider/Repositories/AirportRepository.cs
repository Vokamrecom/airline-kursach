using Airline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airline.DataProvider.Repositories
{
    public interface IAirportRepository
    {   
        IEnumerable<Airport> GetAll();
    }
    public class AirportRepository : BaseRepository<Airport>, IAirportRepository
    {
        public AirportRepository(AirlineContext context) : base(context)
        {
        }
    }
}
