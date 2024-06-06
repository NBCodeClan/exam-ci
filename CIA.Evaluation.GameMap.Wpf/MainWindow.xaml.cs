using CIA.Evaluation.Core.Application;
using CIA.Evaluation.Infrastructure;
using System;
using System.IO;
using System.Windows;

namespace CIA.Evaluation.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Marker marker;
        private GameController game;
        private IGamePresenter presenter;

        public MainWindow()
        {
            InitializeComponent();

            marker = new Marker();
            marker.Width = 55f;
            marker.Height = 55f;

            string dataPath = Path.Combine(Environment.CurrentDirectory, "Content", "airport-locations.json");
            var repository = new JsonAirportRepository(dataPath);
            int mapSize = 15;

            presenter = new WpfGamePresenter(
                mapGrid, marker,
                Cash, CurrentAirport,
                CellClicked);


            game = new GameController(mapSize, presenter, repository);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            float playerRange = 4f;
            double airplaneConsumption = 3;
            double startingCash = 1000;
            int startPositionIndex = 0;
            game.Initialize(playerRange, airplaneConsumption, startingCash, startPositionIndex);
        }

        private void CellClicked(int x, int y)
        {
            try
            {
                game.TravelTo(x, y);
            }
            catch(InvalidOperationException exception)
            {
                MessageBox.Show(exception.Message, "Can't travel here", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

    }
}
