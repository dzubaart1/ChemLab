using Interfaces;
using Substances;
using UnityEngine;

namespace Containers
{
    public class MeasuringContainer : MonoBehaviour, IContainer
    {
        [SerializeField]
        private BaseContainer baseContainer;
        private const int MAXCOUNT = 3;

        public bool AddSubstance(SubstanceParams substanceParams)
        {
            if (baseContainer.SubParams is null)
            {
                baseContainer.SubParams = substanceParams;
                MeshRenderer[] meshRenderers = baseContainer.BaseFormPrefab.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mRend in meshRenderers)
                {
                    mRend.material.color = baseContainer.SubParams.color;
                }

                baseContainer.BaseFormPrefab.SetActive(true);
                return true;
            }
            else
            {
                if (baseContainer.BaseFormPrefab.transform.localScale.z < MAXCOUNT)
                {
                    baseContainer.BaseFormPrefab.transform.localScale += new Vector3(0, 0, 1);
                }
                return true;
            }
        }

        public bool RemoveSubstance()
        {
            if (baseContainer.SubParams is not null)
            {
                baseContainer.SubParams = null;
                baseContainer.BaseFormPrefab.transform.localScale = new Vector3(1, 1, 1);
                baseContainer.BaseFormPrefab.SetActive(false);
                return true;
            }
            return false;
        }
    }
}
