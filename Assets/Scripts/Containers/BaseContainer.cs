using Generators;
using Substances;
using UnityEngine;
using Zenject;

namespace Containers
{
    public class BaseContainer : MonoBehaviour
    {
        public Substance Substance;
        public GameObject BaseFormPrefab;
        public ContainersTypes ContainersType;
        private int _index;

        [Inject]
        public void Construct(IdGenerator generator)
        {
            _index = IdGenerator.GetContainerId();
        }
        public int GetIndex()
        {
            return _index;
        }
    }
}
