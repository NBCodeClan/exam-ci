using CIA.Evaluation.Core.Application;
using CIA.Evaluation.Core.Domain;
using CIA.Evaluation.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CIA.Evaluation.Wpf
{
    internal class WpfGamePresenter : IGamePresenter
    {
        private readonly Grid _mapGrid;
        private readonly Marker _marker;
        private readonly TextBlock _cash, _currentAirport;
        private readonly Action<int, int> _cellClickedCallback;

        private const float buttonSize = 55f;

        public WpfGamePresenter(
            Grid mapGrid, Marker marker,
            TextBlock cash, TextBlock currentAirport,
            Action<int, int> cellClickedCallback)
        {
            _mapGrid = mapGrid;
            _marker = marker;
            _cash = cash;
            _currentAirport = currentAirport;
            _cellClickedCallback = cellClickedCallback;
        }

        public void DrawMap(IMap map, IEnumerable<Airport> airports)
        {
            _mapGrid.Children.Clear();
            InitializeGrid(map.Size);

            for (int y = 0; y < map.Size; y++)
            {
                for (int x = 0; x < map.Size; x++)
                {
                    var airportAtLocation = airports.FirstOrDefault(a => a.X == x && a.Y == y);

                    var mapButton = new Button
                    {
                        Tag = airportAtLocation,
                        Width = buttonSize,
                        Height = buttonSize,
                        Opacity = .55f,
                        BorderBrush = new SolidColorBrush(Colors.LightSlateGray),
                        BorderThickness = new Thickness(1f),
                        Background = new SolidColorBrush(Color.FromArgb(75, 255, 255, 255))
                    };

                    mapButton.Click += MapButton_Click;

                    Grid.SetColumn(mapButton, x);
                    Grid.SetRow(mapButton, y);

                    _mapGrid.Children.Add(mapButton);
                }
            }
            _mapGrid.Children.Add(_marker);
        }
        public void Update(Traveller traveller)
        {
            Grid.SetRow(_marker, traveller.Location.Y);
            Grid.SetColumn(_marker, traveller.Location.X);

            _cash.Text = traveller.Wallet.Cash.ToString("C2");
            _currentAirport.Text = traveller.Location.Name.ToString();
        }

        public void RefreshAirportsInRange(IEnumerable<Airport> locationsInRange)
        {
            foreach (var child in _mapGrid.Children)
            {
                if(child is Button)
                {
                    var button = (Button)child;
                    var airport = (Airport)button.Tag;
                    if (airport != null)
                    {
                        var airportInRange = locationsInRange
                            .FirstOrDefault(a => a.X == airport.X && a.Y == airport.Y);
                        if (airportInRange != null)
                        {
                            button.Background = new SolidColorBrush(Colors.Green);
                            button.BorderBrush = new SolidColorBrush(Colors.Yellow);
                            button.BorderThickness = new Thickness(2f);
                        }
                        else
                        {
                            button.Background = new SolidColorBrush(Colors.DarkRed);
                            button.BorderBrush = new SolidColorBrush(Colors.Yellow);
                            button.BorderThickness = new Thickness(2f);
                        }
                    }
                }
            }
        }

        private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int x = Grid.GetColumn(button);
            int y = Grid.GetRow(button);
            _cellClickedCallback(x, y);
        }

        private void InitializeGrid(int gridResolution)
        {
            _mapGrid.Width = gridResolution * buttonSize;
            _mapGrid.Height = gridResolution * buttonSize;

            _mapGrid.RowDefinitions.Clear();
            _mapGrid.ColumnDefinitions.Clear();

            for (int x = 0; x < gridResolution; x++)
            {
                _mapGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });
                _mapGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = GridLength.Auto
                });
            }
        }

    }
}

