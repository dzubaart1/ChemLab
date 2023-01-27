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
        private BaseContainer baseContainer;
        private SubstancesCollection _substancesCollection;
        private AnchorCntrl _anchor;
        public bool AddSubstance(SubstanceParams substanceParams)
        {
            var res = substanceParams;
            if (baseContainer.SubParams is not null)
            {
                res = _substancesCollection.GetMixSubstance(baseContainer.SubParams, substanceParams);
            }
            baseContainer.SubParams = res;
            baseContainer.BaseFormPrefab.GetComponent<MeshRenderer>().material.color = res.color;
            baseContainer.BaseFormPrefab.SetActive(true);
            return true;
        }

        public AnchorCntrl Anchor => _anchor;
        public bool RemoveSubstance()
        {
            if (baseContainer.SubParams is null) return false;
            baseContainer.SubParams = null;
            baseContainer.BaseFormPrefab.SetActive(false);
            return true;
        }

        public void AddAnchor(AnchorCntrl anchor)
        {
            _anchor = anchor;
        }

        [Inject]
        public void Construct(SubstancesCollection substancesCollection)
        {
            _substancesCollection = substancesCollection;
        }
    }
}
