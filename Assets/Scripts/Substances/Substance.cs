namespace Substances
{
    public class Substance
    {
        public SubstanceParams SubParams { get; }
        public float Weight { get; }
        public Substance(SubstanceParams substanceParams, float weight)
        {
            SubParams = substanceParams;
            Weight = weight;
        }
    }
}