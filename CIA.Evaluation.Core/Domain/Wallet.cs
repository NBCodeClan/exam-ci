using CIA.Evaluation.Core.Domain.Interfaces;
using System;

namespace CIA.Evaluation.Core.Domain
{
    public class Wallet : IWallet
    {
        public Wallet(double startAmount)
        {
            Cash = startAmount;
        }

        public double Cash { get; private set; }

        public bool Pay(double amount)
        {
            if (amount <= Cash)
            {
                Cash = Math.Round(Cash - amount, 2);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
