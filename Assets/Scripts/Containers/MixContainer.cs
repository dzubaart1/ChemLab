using AnchorCntrls;
using Interfaces;
using Substances;
using UnityEngine;
using Zenject;

namespace Containers
{
    public class MixContainer : MonoBehaviour, IContainer
    {
        [SerializeField]
        private BaseContainer _baseContainer;
        private SubstancesParamsCollection _substancesCollection;
        private AnchorCntrl _anchor;
        
        [Inject]
        public void Construct(SubstancesParamsCollection substancesCollection)
        {
            _substancesCollection = substancesCollection;
        }
        public bool AddSubstance(Substance substance)
        {
            var res = substance.SubParams;
            var weight = substance.Weight;
            if (_baseContainer.Substance is not null)
            {
                res = _substancesCollection.GetMixSubstance(_baseContainer.Substance.SubParams, substance.SubParams);
                weight += _baseContainer.Substance.Weight;
            }
            
            _baseContainer.Substance = new Substance(res, weight);
            _baseContainer.BaseFormPrefab.GetComponent<MeshRenderer>().material.color = res.Color;
            _baseContainer.BaseFormPrefab.SetActive(true);
            return true;
        }
        public bool RemoveSubstance()
        {
            if (_baseContainer.Substance is null) return false;
            _baseContainer.Substance = null;
            _baseContainer.BaseFormPrefab.SetActive(false);
            return true;
        }

        public void StirringSubstance()
        {
            if (_baseContainer.Substance is null)
            {
                return;
            }
            var res = _substancesCollection.GetStirringSubstance(_baseContainer.Substance.SubParams);
            _baseContainer.Substance = new Substance(res, _baseContainer.Substance.Weight);
            _baseContainer.BaseFormPrefab.GetComponent<MeshRenderer>().material.color = res.Color;
            _baseContainer.BaseFormPrefab.SetActive(true);
        }
        public AnchorCntrl Anchor => _anchor;

        public void AddAnchor(AnchorCntrl anchor)
        {
            _anchor = anchor;
        }
    }
}
