using CIA.Evaluation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CIA.Evaluation.Core.Tests
{
    public class WalletTests
    {
        /* Opdracht 2
           2.2  "Betalen met voldoende cash retourneert true"

            Tips: 
                - Maak een Theory om verschillende waarden te testen
        */

        [Theory]
        [InlineData(10, 5)]
        [InlineData(6, 6)]
        [InlineData(2, 1)]
        public void Pay_WithSufficientCash_ShouldReturnTrue(double initialCash, double amountToPay)
        {
            // Arrange
            var wallet = new Wallet(initialCash);

            // Act
            var result = wallet.Pay(amountToPay);

            // Assert
            Assert.True(result);
        }

        /* Opdracht 2 
           2.3  "Betalen zonder voldoende cash retourneert false"

            Tips: 
                - Maak een Theory om verschillende waarden te testen
        */
        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(0, 1)]
        public void Pay_WithInsufficientCash_ShouldReturnFalse(double initialCash, double amountToPay)
        {
            // Arrange
            var wallet = new Wallet(initialCash);

            // Act
            var result = wallet.Pay(amountToPay);

            // Assert
            Assert.False(result);
        }

        /* Opdracht 2 
           2.4  "Een succesvolle betaling trekt het betaalde bedrag af van de Cash uit de Wallet"

            Tips: 
                - Maak een Theory om verschillende waarden te testen
        */

        [Theory]
        [InlineData(10, 5, 5)]
        [InlineData(6, 6, 0)]
        [InlineData(8, 3, 5)]
        public void Pay_WithSufficientCash_ShouldDeductAmount(double initialCash, double amountToPay, double expectedCash)
        {
            // Arrange
            var wallet = new Wallet(initialCash);

            // Act
            var result = wallet.Pay(amountToPay);

            // Assert
            Assert.True(result);
            Assert.Equal(expectedCash, wallet.Cash);
        }

    }
}
