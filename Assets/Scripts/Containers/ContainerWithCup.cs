using BNG;
using UnityEngine;

namespace Containers
{
    public class ContainerWithCup : MonoBehaviour
    {
        [SerializeField]
        private CupCntrl[] _cup;
        [SerializeField]
        private SnapZone _snapZone;
        [SerializeField]
        private BaseContainer _baseContainer;
        private string _name;
        private void Awake()
        {
            _snapZone.OnlyAllowNames.Clear();
            int i = 1;
            foreach (CupCntrl cup in _cup)
            {
                cup.gameObject.name += _baseContainer.GetIndex() +"." + i.ToString();
                _snapZone.OnlyAllowNames.Add(cup.gameObject.name);
                i++;
            }
        }

        public bool isClosed()
        {
            return _snapZone.HeldItem is not null;
        }
    }
}
