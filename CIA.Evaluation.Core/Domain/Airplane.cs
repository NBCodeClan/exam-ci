namespace CIA.Evaluation.Core.Domain
{
    public class Airplane
    {
        public Airplane(double range, double costPerKilometer)
        {
            Range = range;
            CostPerKilometer = costPerKilometer;
        }

        public double Range { get; }

        public double CostPerKilometer { get; }

    }

}
