using System;
using Interfaces;
using Substances;
using UnityEngine;

namespace Containers
{
    public class MeasuringContainer : MonoBehaviour, IContainer
    {
        [SerializeField]
        private BaseContainer _baseContainer;
        private const int MAXCOUNT = 3;

        public bool AddSubstance(Substance substance)
        {
            if (_baseContainer.Substance is not null) return false;
            _baseContainer.Substance = substance;
            _baseContainer.BaseFormPrefab.transform.localScale = new Vector3(1, 1, substance.Weight / 10);
            _baseContainer.BaseFormPrefab.GetComponent<MeshRenderer>().material.color = substance.SubParams.Color;
            _baseContainer.BaseFormPrefab.SetActive(true);
            return true;

        }

        public bool RemoveSubstance()
        {
            if (_baseContainer.Substance is null) return false;
            _baseContainer.Substance = null;
            _baseContainer.BaseFormPrefab.transform.localScale = new Vector3(1, 1, 10);
            _baseContainer.BaseFormPrefab.SetActive(false);
            return true;
        }
    }
}
