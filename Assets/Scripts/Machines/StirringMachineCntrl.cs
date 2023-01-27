using BNG;
using Containers;
using UnityEngine;

namespace Machines
{
    public class StirringMachineCntrl : MonoBehaviour
    {
        [SerializeField]
        private SnapZone _snapZone;
        public void StartStirring()
        {
            if(!CheckMixContainer() || !CheckAnchor())
            {
                return;
            }
            
            _snapZone.HeldItem.gameObject.GetComponent<MixContainer>().Anchor.gameObject.GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<Animator>().enabled = true;
        }

        private bool CheckMixContainer()
        {
            return _snapZone.HeldItem.gameObject.GetComponent<MixContainer>() is not null;
        }

        private bool CheckAnchor()
        {
            return _snapZone.HeldItem is not null && _snapZone.HeldItem.gameObject.GetComponent<MixContainer>().Anchor is not null;
        }
    }
}
