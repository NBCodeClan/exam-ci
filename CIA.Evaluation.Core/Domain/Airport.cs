namespace CIA.Evaluation.Core.Domain
{

    public class Airport
    {
        public Airport()
        {
        }

        public Airport(int x, int y)
        {
            X = x;
            Y = y;
        }

        public string Name { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public double LandingFee { get; set; }

    }

}
