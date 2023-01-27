using System.Globalization;
using BNG;
using Containers;
using UnityEngine;
using UnityEngine.UI;

namespace Machines
{
    public class WeightingMachineCntrl : MonoBehaviour
    {
        [SerializeField]
        private Text _weightText;
        [SerializeField]
        private SnapZone _snapZone;

        private void Awake()
        {
            _weightText.text = "0.0000g";
        }

        void Update()
        {
            if (_snapZone.HeldItem is null || _snapZone.HeldItem.GetComponent<BaseContainer>().SubParams is null || !_snapZone.HeldItem.GetComponent<WeightableContainer>())
            {
                _weightText.text = "0.0000g";
                return;
            }

            var text = _snapZone.HeldItem.GetComponent<BaseContainer>().SubParams.weight.ToString("0.0000", CultureInfo.InvariantCulture);
        
            _weightText.text = text + "g";
        }
    }
}
