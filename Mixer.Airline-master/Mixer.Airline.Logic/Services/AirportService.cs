using Airline.DataProvider.Repositories;
using Airline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airline.Logic.Services
{
    public interface IAirportService
    {
        IEnumerable<AirportViewModel> GetAll();
    }
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _airportRepository;

        public AirportService(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public IEnumerable<AirportViewModel> GetAll()
        {
            var list = _airportRepository.GetAll();

            var airports = list.Select(a => new AirportViewModel()
            {
                AirportCode = a.AirportCode,
                Name = a.Name,
                City = a.City
            }).ToList();

            return airports;
        }
    }
}
