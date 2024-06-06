using CIA.Evaluation.Core.Domain;
using CIA.Evaluation.Core.Domain.Interfaces;
using System.Linq;

namespace CIA.Evaluation.Core.Application
{
    public class GameController
    {
        private readonly IGamePresenter _presenter;
        private readonly IAirportRepository _airportRepository;
        private readonly IMap _map;

        private int _mapSize;
        private Traveller _traveller;

        public GameController(
            int mapSize,
            IGamePresenter presenter,
            IAirportRepository airportRepository)
        {
            _presenter = presenter;
            _airportRepository = airportRepository;
            _mapSize = mapSize;

            _map = new Map(mapSize, _airportRepository);
        }

        public void Initialize(float airplaneRange, double airplaneConsumption, double startAmount, int startingAirportIndex)
        {
            var airports = _airportRepository.GetAirports().ToArray();

            var plane = new Airplane(airplaneRange, airplaneConsumption);
            var wallet = new Wallet(startAmount);

            _traveller = new Traveller(_map, wallet, plane, airports[startingAirportIndex]);

            _presenter.DrawMap(_map, airports);
            Refresh();
        }

        public void TravelTo(int x, int y)
        {
            _traveller.Travel(x, y);
            Refresh();
        }

        public void Refresh()
        {
            _presenter.Update(_traveller);
            _presenter.RefreshAirportsInRange(_map.GetAirportsInRange(_traveller.Location, _traveller.Airplane.Range));
        }
    }
}
