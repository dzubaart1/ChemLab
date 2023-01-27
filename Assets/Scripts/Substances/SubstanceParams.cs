using System.Collections.Generic;
using UnityEngine;

namespace Substances
{
    [CreateAssetMenu(fileName = "SubstanceParams", menuName = "Substance/Substance Params", order = 1)]
    public class SubstanceParams : ScriptableObject
    {
        public string subName;
        public Color color;
        public float weight;
        public List<SubstanceParams> components;
    }
}