using CIA.Evaluation.Core.Domain;
using CIA.Evaluation.Core.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CIA.Evaluation.Core.Tests
{
    public class MapTests
    {
        /* Opdracht 2 
           2.1  "De berekende afstand tussen Airports wordt steeds afgerond op 2 decimalen"

            Vervolledig de act and assert fases.
        */
        [Theory]
        [InlineData(2, 2, 4, 4, 2.83)]
        [InlineData(0, 6, 5, 8, 5.39)]
        public void DistanceTo_RoundedTwoDecimals(int x1, int y1, int x2, int y2, double expectedDistance)
        {
            //arrange
            var origin = new Airport { X = x1, Y = y1 };
            var destination = new Airport { X = x2, Y = y2 };

            var mockRepository = new Mock<IAirportRepository>();
            var map = new Map(20, mockRepository.Object);

            //act
            var disctanceAirports = map.DistanceTo(origin, destination);
            //assert
            Assert.Equal(expectedDistance, disctanceAirports, 2);

        }






        [Fact]
        public void GetAirportAt_ShouldReturnCorrectAirport()
        {
            // Arrange
            var airport = new Airport { X = 1, Y = 2 };
            var airportRepositoryMock = new Mock<IAirportRepository>();
            airportRepositoryMock.Setup(repo => repo.GetAirportAt(1, 2)).Returns(airport);
            var map = new Map(5, airportRepositoryMock.Object);

            // Act
            var result = map.GetAirportAt(1, 2);

            // Assert
            Assert.Equal(airport, result);
        }

        [Fact]
        public void IsAirportAt_ShouldReturnTrueIfAirportExists()
        {
            // Arrange
            var airport = new Airport { X = 1, Y = 2 };
            var airportRepositoryMock = new Mock<IAirportRepository>();
            airportRepositoryMock.Setup(repo => repo.GetAirportAt(1, 2)).Returns(airport);
            var map = new Map(5, airportRepositoryMock.Object);

            // Act
            var result = map.IsAirportAt(1, 2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetAirportsInRange_ShouldReturnAirportsWithinRadius()
        {
            // Arrange
            var airport1 = new Airport { X = 1, Y = 2 };
            var airport2 = new Airport { X = 2, Y = 3 };
            var airportRepositoryMock = new Mock<IAirportRepository>();
            airportRepositoryMock.Setup(repo => repo.GetAirports()).Returns(new List<Airport> { airport1, airport2 });
            var map = new Map(5, airportRepositoryMock.Object);

            // Act
            var result = map.GetAirportsInRange(new Airport { X = 1, Y = 2 }, 2);

            // Assert
            Assert.Contains(airport1, result);
            Assert.Contains(airport2, result);
        }
    }
}
