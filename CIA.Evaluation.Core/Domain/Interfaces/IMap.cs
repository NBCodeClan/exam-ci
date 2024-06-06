using System.Collections.Generic;

namespace CIA.Evaluation.Core.Domain.Interfaces
{
    public interface IMap
    {
        int Size { get; }

        Airport GetAirportAt(int x, int y);

        bool IsAirportAt(int x, int y);

        double DistanceTo(Airport start, Airport destination);

        IEnumerable<Airport> GetAirportsInRange(Airport center, double radius);

    }

}
