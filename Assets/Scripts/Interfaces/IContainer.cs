using Containers;
using Substances;

namespace Interfaces
{
    public interface IContainer
    {
        public bool RemoveSubstance();
        public bool AddSubstance(Substance substance);
    }
}
