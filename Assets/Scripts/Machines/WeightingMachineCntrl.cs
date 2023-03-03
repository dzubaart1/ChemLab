using System.Globalization;
using BNG;
using Containers;
using Interfaces;
using Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Machines
{
    public class WeightingMachineCntrl : MonoBehaviour, IMachine
    {
        [SerializeField]
        private Text _weightText;
        [SerializeField]
        private SnapZone _snapZone;
        [SerializeField] 
        private BaseMachine _baseMachine;

        private TasksCollection _tasksCollection;
        private bool _isStart, _isEnter, _hasObject;
        [Inject]
        public void Construct(TasksCollection tasksCollection)
        {
            _tasksCollection = tasksCollection;
        }
        private void Awake()
        {
            OnResetValues();
        }

        private void Update()
        {
            if (_isEnter && _snapZone.HeldItem.GetComponent<BaseContainer>().Substance is not null && !_isStart)
            {
                OnStartWork();
            }
        }
        
        public void OnEnterObject()
        {
            if (_snapZone.HeldItem is null ||
                _snapZone.HeldItem.GetComponent<WeightableContainer>() is null)
            {
                return;
            }
            Debug.Log("IsEnter");
            _isEnter = true;
            _tasksCollection.CheckEnteringIntoMachine(_baseMachine.MachinesType,
                _snapZone.HeldItem.GetComponent<BaseContainer>().ContainersType);
        }

        public void OnDetachObject()
        {
            _isEnter = false;
            _isStart = false;
            OnResetValues();
        }
        
        public void OnResetValues()
        {
            _weightText.text = "0.0000g";
        }
        
        public void OnStartWork()
        {
            Debug.Log("IsStart");
            _isStart = true;
            var text = _snapZone.HeldItem.GetComponent<BaseContainer>().Substance.Weight.ToString("0.0000", CultureInfo.InvariantCulture);
            _weightText.text = text + "g";
            OnFinishWork();
        }

        public void OnFinishWork()
        {
            _tasksCollection.CheckFinishMachineWork(_baseMachine.MachinesType, _snapZone.HeldItem.GetComponent<BaseContainer>().Substance);
        }
    }
}
 