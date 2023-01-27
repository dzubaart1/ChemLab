using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Substances
{
    public class SubstancesCollection
    {
        private List<SubstanceParams> _substanceParamsList;

        public SubstancesCollection()
        {
            _substanceParamsList = new List<SubstanceParams>();
            _substanceParamsList = Resources.LoadAll<SubstanceParams>("Substances/").ToList();
        }

        private SubstanceParams GetBadSubstance() 
        {
            return _substanceParamsList[0];
        }
        public SubstanceParams GetMixSubstance(SubstanceParams oldParam, SubstanceParams addParam)
        {
            foreach (var substance in _substanceParamsList.Where(substance => substance.components.Contains(oldParam) && substance.components.Contains(addParam)))
            {
                return substance;
            }

            return GetBadSubstance();
        }
    }
}
