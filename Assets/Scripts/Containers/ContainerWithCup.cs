using BNG;
using UnityEngine;

namespace Containers
{
    public class ContainerWithCup : MonoBehaviour
    {
        [SerializeField]
        private CupCntrl _cup;
        [SerializeField]
        private SnapZone _snapZone;
        [SerializeField]
        private BaseContainer _baseContainer;
        private string _name;
        private void Awake()
        {
            _cup.gameObject.name += _baseContainer.GetIndex();
            _snapZone.OnlyAllowNames.Clear();
            _snapZone.OnlyAllowNames.Add(_cup.gameObject.name);
        }

        public bool isClosed()
        {
            return _snapZone.HeldItem is not null;
        }
    }
}
