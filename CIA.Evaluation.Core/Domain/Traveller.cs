using CIA.Evaluation.Core.Domain.Interfaces;
using System;

namespace CIA.Evaluation.Core.Domain
{
    public class Traveller
    {
        private IMap _map;
        public const string NoAirportError = "Can't travel here, there is no airport.";
        public const string AirportOutOfRangeError = "This destination is too far away.";
        public const string InsufficientCashError = "Not enough cash to travel here.";

        public Traveller(IMap map, IWallet wallet, Airplane airplane, Airport startLocation)
        {
            _map = map;
            Wallet = wallet;
            Airplane = airplane;
            Location = startLocation;
        }

        public Airport Location { get; private set; }
        public Airplane Airplane { get; private set; }
        public IWallet Wallet { get; private set; }

        public bool Travel(int x, int y)
        {
            if(Location?.X == x && Location?.Y == y)
            {
                return false;
            }

            if(!_map.IsAirportAt(x, y))
            {
                throw new InvalidOperationException(NoAirportError);
            }

            var destination = _map.GetAirportAt(x, y);

            if (_map.DistanceTo(Location, destination) > Airplane.Range)
            {
                throw new InvalidOperationException(AirportOutOfRangeError);
            }
            else
            {
                double costs = CalculateTravelCosts(destination);
                if (!Wallet.Pay(costs))
                { 
                    throw new InvalidOperationException(InsufficientCashError);
                }
                Location = destination;
            }

            return true;
        }

        public double CalculateTravelCosts(Airport destination)
        {
            double distance = _map.DistanceTo(Location, destination);
            double flyingCost = Airplane.CostPerKilometer * distance;
            return destination.LandingFee + flyingCost;
        }
    }
}
