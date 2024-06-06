using System.Collections.Generic;

namespace CIA.Evaluation.Core.Domain.Interfaces
{
    public interface IAirportRepository
    {
        Airport GetAirportAt(int x, int y);
        IEnumerable<Airport> GetAirports();
        bool AirportExists(Airport airport);
    }

}
