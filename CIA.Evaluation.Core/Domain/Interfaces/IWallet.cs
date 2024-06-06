namespace CIA.Evaluation.Core.Domain.Interfaces
{
    public interface IWallet
    {
        /// <summary>
        /// The current amount of cash in the wallet
        /// </summary>
        double Cash { get; }

        /// <summary>
        /// Pays a certain amount using the Cash.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>true if there is enough Cash to pay</returns>
        bool Pay(double amount);
    }
}