using Airline.DataProvider.Repositories;
using Airline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airline.Logic.Services
{
    public interface IAircraftService
    {
        IEnumerable<AircraftViewModel> GetAll();
    }
    public class AircraftService : IAircraftService
    {
        private readonly IAircraftRepository _aircraftRepository;

        public AircraftService(IAircraftRepository aircraftRepository)
        {
            _aircraftRepository = aircraftRepository;
        }

        public IEnumerable<AircraftViewModel> GetAll()
        {
            var list = _aircraftRepository.GetAll();

            var aircrafts = list.Select(a => new AircraftViewModel()
            {
                AircraftCode = a.AircraftCode,
                Model = a.Model,
                TotalPlaces = a.TotalPlaces
            }).ToList();

            return aircrafts;
        }
    }
}
