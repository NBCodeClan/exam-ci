using CIA.Evaluation.Core.Domain;
using CIA.Evaluation.Core.Domain.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CIA.Evaluation.Infrastructure
{

    public class JsonAirportRepository : IAirportRepository
    {
        private readonly string _path;

        public JsonAirportRepository(string jsonPath)
        {
            _path = jsonPath;
        }

        public Airport GetAirportAt(int x, int y)
        {
            var airports = LoadAirports();
            return airports.FirstOrDefault(airport => airport.X == x && airport.Y == y);
        }

        public IEnumerable<Airport> GetAirports()
        {
            var airports = LoadAirports();
            return airports;
        }

        public bool AirportExists(Airport airport)
        {
            var airports = LoadAirports();
            return airports.Any(a =>
                a.X == airport.X &&
                a.Y == airport.Y &&
                a.Name == airport.Name
            );
        }

        private IEnumerable<Airport> LoadAirports()
        {
            string json = File.ReadAllText(_path);
            return JsonConvert.DeserializeObject<IEnumerable<Airport>>(json);
        }
    }
}
