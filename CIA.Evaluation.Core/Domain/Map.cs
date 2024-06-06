using CIA.Evaluation.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace CIA.Evaluation.Core.Domain
{

    public class Map : IMap
    {
        public Map(int mapSize, IAirportRepository airportRepository)
        {
            this.airportRepository = airportRepository;
            Size = mapSize;
        }

        private IAirportRepository airportRepository;
        public int Size { get; private set; }

        public Airport GetAirportAt(int x, int y)
        {
            return airportRepository.GetAirportAt(x, y);
        }
        public bool IsAirportAt(int x, int y)
        {
            var location = GetAirportAt(x, y);
            return location != null;
        }

        public double DistanceTo(Airport origin, Airport destination)
        {
            var distance = Math.Sqrt(Math.Pow(destination.X - origin.X, 2) + Math.Pow(destination.Y - origin.Y, 2));
            return Math.Round(distance, 2);
        }

        public IEnumerable<Airport> GetAirportsInRange(Airport center, double radius)
        {
            List<Airport> locationsAround = new List<Airport>();

            foreach(var airport in airportRepository.GetAirports())
            {
                if(DistanceTo(center, airport) <= radius)
                {
                    locationsAround.Add(airport);
                }
            }

            return locationsAround;
        }

    }
}
