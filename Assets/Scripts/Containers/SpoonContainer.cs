using Interfaces;
using Substances;
using UnityEngine;

namespace Containers
{
    public class SpoonContainer : MonoBehaviour, IContainer
    {
        [SerializeField]
        private BaseContainer baseContainer;
        public bool AddSubstance(SubstanceParams substanceParams)
        {
            if (baseContainer.SubParams is not null) return false;
            baseContainer.SubParams = substanceParams;
            baseContainer.BaseFormPrefab.GetComponent<MeshRenderer>().material.color = baseContainer.SubParams.color;
            baseContainer.BaseFormPrefab.SetActive(true);
            return true;

        }

        public bool RemoveSubstance()
        {
            if (baseContainer.SubParams is null) return false;
            baseContainer.SubParams = null;
            baseContainer.BaseFormPrefab.SetActive(false);
            return true;
        }
    }
}
