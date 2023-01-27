using Interfaces;
using Substances;
using UnityEngine;

namespace Containers
{
    public class EndlessContainer : MonoBehaviour, IContainer
    {
        [SerializeField]
        private BaseContainer baseContainer;

        public bool AddSubstance(SubstanceParams substanceParams)
        {
            return false;
        }

        public bool RemoveSubstance()
        {
            return false;
        }

        public void Awake()
        {
            baseContainer.BaseFormPrefab.SetActive(true);
            baseContainer.BaseFormPrefab.GetComponent<MeshRenderer>().material.color = baseContainer.SubParams.color;
        }
    }
}
