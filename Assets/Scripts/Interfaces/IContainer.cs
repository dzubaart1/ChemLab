using Substances;

namespace Interfaces
{
    public interface IContainer
    {
        public bool RemoveSubstance();
        public bool AddSubstance(SubstanceParams substanceParams);
    }
}
