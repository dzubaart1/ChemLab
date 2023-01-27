using UnityEngine;

namespace Containers
{
    public class CupCntrl : MonoBehaviour
    {
        private string _name;

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }
    }
}
