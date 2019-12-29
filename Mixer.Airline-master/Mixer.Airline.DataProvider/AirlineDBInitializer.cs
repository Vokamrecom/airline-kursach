using Airline.Common;
using Airline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airline.DataProvider
{
    public static class AirlineDBInitializer
    {
        public static void Initialize(AirlineContext context)
        {
            IList<Airport> defaultAirports = new List<Airport>();
            IList<Aircraft> defaultAircrafts = new List<Aircraft>();
            IList<Flight> defaultFlights = new List<Flight>();
            IList<Passenger> defaultUsers = new List<Passenger>();

            if (!context.Airports.Any())
            {
                defaultAirports.Add(new Airport() { AirportCode = "DBI", Name = "Dubai international airport", City = "Dubai" });
                defaultAirports.Add(new Airport() { AirportCode = "JKT", Name = "Soekarno-Hatta", City = "Jakarta" });
                defaultAirports.Add(new Airport() { AirportCode = "DLS", Name = "International airport Dallas", City = "Dallas" });
                defaultAirports.Add(new Airport() { AirportCode = "PRS", Name = "International airport Charles de Gaulle", City = "Paris" });
                defaultAirports.Add(new Airport() { AirportCode = "LSA", Name = "International airport Los Angeles", City = "Los Angeles" });
                defaultAirports.Add(new Airport() { AirportCode = "TKO", Name = "Haneda airport", City = "Tokyo" });
                defaultAirports.Add(new Airport() { AirportCode = "LND", Name = "Heathrow Airport", City = "London" });
                defaultAirports.Add(new Airport() { AirportCode = "CHG", Name = "O'hare airport", City = "Chicago" });
                defaultAirports.Add(new Airport() { AirportCode = "BJN", Name = "Beijing capital international airport", City = "Beijing" });
                defaultAirports.Add(new Airport() { AirportCode = "ATN", Name = "Hartsfield-Jackson", City = "Atlanta" });

                context.Airports.AddRange(defaultAirports);
            }

            if (!context.Aircrafts.Any())
            {
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AB321", Model = "Airbus-А320", TotalPlaces = 180 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AB322", Model = "Airbus-А320", TotalPlaces = 180 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AB323", Model = "Airbus-А320", TotalPlaces = 180 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AB341", Model = "Airbus-A340", TotalPlaces = 700 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AB342", Model = "Airbus-A340", TotalPlaces = 700 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AB343", Model = "Airbus-A340", TotalPlaces = 700 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AB381", Model = "Airbus-А380", TotalPlaces = 1000 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AB382", Model = "Airbus-А380", TotalPlaces = 1000 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AB383", Model = "Airbus-А380", TotalPlaces = 1000 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "IL621", Model = "Il-62", TotalPlaces = 190 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "IL622", Model = "Il-62", TotalPlaces = 190 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "IL623", Model = "Il-62", TotalPlaces = 190 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "SSJ01", Model = "SSJ-100", TotalPlaces = 250 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "SSJ02", Model = "SSJ-100", TotalPlaces = 250 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "SSJ03", Model = "SSJ-100", TotalPlaces = 250 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "BG571", Model = "Boeing-757", TotalPlaces = 250 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "BG572", Model = "Boeing-757", TotalPlaces = 250 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "BG573", Model = "Boeing-757", TotalPlaces = 250 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "T1451", Model = "Tu-154", TotalPlaces = 150 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "T1452", Model = "Tu-154", TotalPlaces = 150 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "T1453", Model = "Tu-154", TotalPlaces = 150 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "LHGX1", Model = "Lockheed-Galaxy", TotalPlaces = 270 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "LHGX2", Model = "Lockheed-Galaxy", TotalPlaces = 270 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "LHGX3", Model = "Lockheed-Galaxy", TotalPlaces = 270 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AN241", Model = "An-124", TotalPlaces = 320 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AN242", Model = "An-124", TotalPlaces = 320 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AN243", Model = "An-124", TotalPlaces = 320 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AN551", Model = "An-255", TotalPlaces = 220 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AN552", Model = "An-255", TotalPlaces = 220 });
                defaultAircrafts.Add(new Aircraft() { AircraftCode = "AN553", Model = "An-255", TotalPlaces = 220 });

                context.Aircrafts.AddRange(defaultAircrafts);
            }

            string[] airlineCompanies = { "AA", "AC", "AZ", "SB", "NZ", "AI", "AM", "NH", "TN", "OS", "FJ", "LD", "SU", "PX", "EL", "OZ", "CA" };
            int[] minutes = { 0, 15, 30, 45 };
            int counter = 1001;
            Random random = new Random();

            if (!context.Flights.Any())
            {
                for (int j = 0; j < defaultAircrafts.Count(); j++)
                {
                    for (int i = 0; i < defaultAirports.Count(); i++)
                    {
                        int month = random.Next(6, 10);
                        int depDay = random.Next(1, 29);
                        int flightTime = random.Next(5, 12);
                        int price = flightTime * 800 / 5;
                        int price2 = flightTime * 800 / 7;
                        int arrDay;
                        int depHour = random.Next(0, 23);
                        int arrHour = depHour + flightTime;
                        int m = i + 1;
                        if (arrHour > 23)
                        {
                            arrHour = arrHour - 23;
                            arrDay = depDay + 1;
                        }
                        else
                            arrDay = depDay;

                        if (i == defaultAirports.Count() - 1)
                            m = 0;
                        int depMinute = random.Next(1, 3);
                        int arrMinute = random.Next(1, 3);

                        defaultFlights.Add(new Flight()
                        {
                            FlightNumber = airlineCompanies[random.Next(1, airlineCompanies.Count())] + Convert.ToString(counter).Substring(1),
                            DepartureDateTime = new DateTime(2019, month, depDay, depHour, minutes[depMinute], 00),
                            DepartureAirport = defaultAirports[i].AirportCode,
                            AircraftCode = defaultAircrafts[j].AircraftCode,
                            TotalPlaces = defaultAircrafts[j].TotalPlaces,
                            ArrivalDateTime = new DateTime(2019, month, arrDay, arrHour, minutes[arrMinute], 00),
                            ArrivalAirport = defaultAirports[m].AirportCode,
                            Price = price
                        });
                        defaultFlights.Add(new Flight()
                        {
                            FlightNumber = airlineCompanies[random.Next(1, airlineCompanies.Count())] + Convert.ToString(counter + 1).Substring(1),
                            DepartureDateTime = new DateTime(2019, month, depDay + 1, depHour / 2 + random.Next(depHour / 4, depHour / 2), minutes[depMinute], 00),
                            DepartureAirport = defaultAirports[m].AirportCode,
                            AircraftCode = defaultAircrafts[j].AircraftCode,
                            TotalPlaces = defaultAircrafts[j].TotalPlaces,
                            ArrivalDateTime = new DateTime(2019, month, arrDay + 1, arrHour / 2 + random.Next(arrHour / 4, arrHour / 2), minutes[arrMinute], 00),
                            ArrivalAirport = defaultAirports[i].AirportCode,
                            Price = price2
                        });
                        counter++;
                    }
                }
                context.Flights.AddRange(defaultFlights);
            }

            if (!context.Passengers.Any())
            {
                defaultUsers.Add(new Passenger() { Name = "admin", Surname = "admin", Email = "admin@admin.com", Password = Crypto.Sha256("admin" + "admin@admin.com"), Role = "Admin" });
                context.Passengers.AddRange(defaultUsers);
            }
            context.SaveChanges();
        }
    }
}