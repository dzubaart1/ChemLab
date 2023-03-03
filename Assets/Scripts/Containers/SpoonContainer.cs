using Interfaces;
using Substances;
using UnityEngine;

namespace Containers
{
    public class SpoonContainer : MonoBehaviour, IContainer
    {
        [SerializeField]
        private BaseContainer _baseContainer;
        public bool AddSubstance(Substance substance)
        {
            if (_baseContainer.Substance is not null)
            {
                return false;
            }
            _baseContainer.Substance = substance;
            _baseContainer.BaseFormPrefab.GetComponent<MeshRenderer>().material.color = _baseContainer.Substance.SubParams.Color;
            _baseContainer.BaseFormPrefab.SetActive(true);
            return true;

        }

        public bool RemoveSubstance()
        {
            if (_baseContainer.Substance is null)
            {
                return false;
            }
            _baseContainer.Substance = null;
            _baseContainer.BaseFormPrefab.SetActive(false);
            return true;
        }
    }
}
