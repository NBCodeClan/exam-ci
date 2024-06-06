using CIA.Evaluation.Core.Domain;
using CIA.Evaluation.Core.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CIA.Evaluation.Core.Tests
{
    public class TravellerTests
    {

        /* Opdracht 3
           3.1 "Als er gereist wordt naar de huidige luchthaven, dan wordt er false geretourneerd" 

            Tips: 
             - Hou er rekening mee dat de Traveller minstens een start-Airport parameter 
               moet ontvangen in de constructor, zodat de huidige locatie wordt ingesteld
         */

        [Fact]
        public void Travel_ToCurrentAirport_ShouldReturnFalse()
        {
            // Arrange
            var mapMock = new Mock<IMap>();
            var walletMock = new Mock<IWallet>();
            var airplane = new Airplane(5, 1);
            var airport = new Airport { X = 5, Y = 2 };
            var traveller = new Traveller(mapMock.Object, walletMock.Object, airplane, airport);

            // Act
            var result = traveller.Travel(5, 2);

            // Assert
            Assert.False(result);
        }


        /* Opdracht 3
           3.2 "Reizen naar een plaats waar geen Airport is, resulteert in een exceptie"

            Tips: 
             - Hou er rekening mee dat de Traveller een IMap object, een IWallet, een Airplane en een start-Airport parameter 
               moet ontvangen in de constructor.
             - Het bepalen of er een Airport op een X en Y locatie is wordt gedaan met de IMap.IsAirportAt methode. 
               => Je mag de juiste werking van deze methoden niet meetesten, dus zal je deze moeten mocken.
         */

        [Fact]
        public void Travel_ToNonExistentAirport_ShouldThrowException()
        {
            // Arrange
            var mapMock = new Mock<IMap>();
            mapMock.Setup(m => m.IsAirportAt(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            var walletMock = new Mock<IWallet>();
            var airplane = new Airplane(5, 1);
            var airport = new Airport { X = 5, Y = 2 };
            var traveller = new Traveller(mapMock.Object, walletMock.Object, airplane, airport);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => traveller.Travel(0, 0));
            Assert.Equal(Traveller.NoAirportError, exception.Message);
        }

        /* Opdracht 3
           3.3 "Reizen naar een Airport buiten het bereik van het vliegtuig resulteert in een exceptie"

            Tips: 
            - Hou er rekening mee dat de Traveller een IMap object, een IWallet, een Airplane en een start-Airport parameter 
              moet ontvangen in de constructor.
            - Het bepalen of er een Airport op een X en Y locatie is wordt gedaan met de IMap.IsAirportAt methode. 
            - Het ophalen van een een Airport op een X en Y locatie is wordt gedaan met de IMap.GetAirportAt methode. 
            - De afstand tussen twee Airports wordt berekend met de IMap.DistanceTo methode. 
              => Je mag de juiste werking van deze methoden niet meetesten, dus zal je deze moeten mocken. 
         */
        [Fact]
        public void Travel_ToAirportOutOfRange_ShouldThrowException()
        {
            // Arrange
            var mapMock = new Mock<IMap>();
            mapMock.Setup(m => m.IsAirportAt(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            mapMock.Setup(m => m.GetAirportAt(It.IsAny<int>(), It.IsAny<int>())).Returns(new Airport { X = 10, Y = 10 });
            mapMock.Setup(m => m.DistanceTo(It.IsAny<Airport>(), It.IsAny<Airport>())).Returns(10);
            var walletMock = new Mock<IWallet>();
            var airplane = new Airplane(5, 1);
            var airport = new Airport { X = 5, Y = 2 };
            var traveller = new Traveller(mapMock.Object, walletMock.Object, airplane, airport);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => traveller.Travel(10, 10));
            Assert.Equal(Traveller.AirportOutOfRangeError, exception.Message);
        }

        /* Opdracht 3
           3.4 "Als er niet genoeg cash beschikbaar is voor een vlucht, dan wordt er een exception gegooid"

            Tips: 
            - Hou er rekening mee dat de Traveller een IMap object, een IWallet, een Airplane en een start-Airport parameter 
              moet ontvangen in de constructor.
            - Het bepalen of er een Airport op een X en Y locatie is wordt gedaan met de IMap.IsAirportAt methode. 
            - Het ophalen van een een Airport op een X en Y locatie is wordt gedaan met de IMap.GetAirportAt methode. 
            - De afstand tussen twee Airports wordt berekend met de IMap.DistanceTo methode. 
              => Je mag de juiste werking van deze methoden niet meetesten, dus zal je deze moeten mocken.
         */
        [Fact]
        public void Travel_WithoutSufficientCash_ShouldThrowException()
        {
            // Arrange
            var mapMock = new Mock<IMap>();
            mapMock.Setup(m => m.IsAirportAt(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            mapMock.Setup(m => m.GetAirportAt(It.IsAny<int>(), It.IsAny<int>())).Returns(new Airport { X = 6, Y = 2 });
            mapMock.Setup(m => m.DistanceTo(It.IsAny<Airport>(), It.IsAny<Airport>())).Returns(1);
            var walletMock = new Mock<IWallet>();
            walletMock.Setup(w => w.Pay(It.IsAny<double>())).Returns(false);
            var airplane = new Airplane(5, 1);
            var airport = new Airport { X = 5, Y = 2 };
            var traveller = new Traveller(mapMock.Object, walletMock.Object, airplane, airport);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => traveller.Travel(6, 2));
            Assert.Equal(Traveller.InsufficientCashError, exception.Message);
        }

        /* Opdracht 3
           3.5 "Een reiziger die met voldoende cash naar een Airport binnen bereik vliegt komt op de locatie van de bestemming aan."

            Tips: 
            - Hou er rekening mee dat de Traveller een IMap object, een IWallet, een Airplane en een start-Airport parameter 
              moet ontvangen in de constructor.
            - Het bepalen of er een Airport op een X en Y locatie is wordt gedaan met de IMap.IsAirportAt methode. 
            - Het ophalen van een een Airport op een X en Y locatie is wordt gedaan met de IMap.GetAirportAt methode. 
            - De afstand tussen twee Airports wordt berekend met de IMap.DistanceTo methode. 
              => Je mag de juiste werking van deze methoden niet meetesten, dus zal je deze moeten mocken.
         */

        [Fact]
        public void Travel_WithSufficientCash_ShouldArriveAtDestination()
        {
            // Arrange
            var destinationAirport = new Airport { X = 10, Y = 10 };
            var mapMock = new Mock<IMap>();
            mapMock.Setup(m => m.IsAirportAt(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            mapMock.Setup(m => m.GetAirportAt(It.IsAny<int>(), It.IsAny<int>())).Returns(destinationAirport);
            mapMock.Setup(m => m.DistanceTo(It.IsAny<Airport>(), It.IsAny<Airport>())).Returns(5);
            var walletMock = new Mock<IWallet>();
            walletMock.Setup(w => w.Pay(It.IsAny<double>())).Returns(true);
            var airplane = new Airplane(10, 1);
            var airport = new Airport { X = 5, Y = 2 };
            var traveller = new Traveller(mapMock.Object, walletMock.Object, airplane, airport);

            // Act
            var result = traveller.Travel(10, 10);

            // Assert
            Assert.True(result);
            Assert.Equal(10, traveller.Location.X);
            Assert.Equal(10, traveller.Location.Y);
        }

        /* Opdracht 3
           3.6 "De reiskost is gelijk aan het verbruik per kilometer plus de LandingFee van de bestemming" 

            Tips: 
             - Hou er rekening mee dat de Traveller minstens een start-Airport parameter 
               moet ontvangen in de constructor, zodat de huidige locatie wordt ingesteld
         */

        [Fact]
        public void CalculateTravelCosts_ShouldReturnCorrectAmount()
        {
            // Arrange
            var destinationAirport = new Airport { X = 10, Y = 10, LandingFee = 100 };
            var mapMock = new Mock<IMap>();
            mapMock.Setup(m => m.DistanceTo(It.IsAny<Airport>(), It.IsAny<Airport>())).Returns(10);
            var walletMock = new Mock<IWallet>();
            var airplane = new Airplane(10, 2);
            var airport = new Airport { X = 5, Y = 2 };
            var traveller = new Traveller(mapMock.Object, walletMock.Object, airplane, airport);

            // Act
            var travelCost = traveller.CalculateTravelCosts(destinationAirport);

            // Assert
            Assert.Equal(120, travelCost);
        }
    }
}
