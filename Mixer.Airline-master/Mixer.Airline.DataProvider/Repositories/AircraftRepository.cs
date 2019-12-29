using Airline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airline.DataProvider.Repositories
{
    public interface IAircraftRepository
    {
        IEnumerable<Aircraft> GetAll();
    }
    public class AircraftRepository : BaseRepository<Aircraft>, IAircraftRepository
    {
        public AircraftRepository(AirlineContext context) : base(context)
        {
        }
    }
}
