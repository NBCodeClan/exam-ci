using CIA.Evaluation.Core.Domain;
using CIA.Evaluation.Core.Domain.Interfaces;
using System.Collections.Generic;

namespace CIA.Evaluation.Core.Application
{
    public interface IGamePresenter
    {
        void DrawMap(IMap map, IEnumerable<Airport> airports);

        void Update(Traveller traveller);

        void RefreshAirportsInRange(IEnumerable<Airport> airportsInRange);
    }
}
