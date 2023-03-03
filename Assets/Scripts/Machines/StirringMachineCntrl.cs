using System;
using BNG;
using Containers;
using Interfaces;
using Substances;
using Tasks;
using UnityEngine;
using Zenject;

namespace Machines
{
    public class StirringMachineCntrl : MonoBehaviour, IMachine
    {
        [SerializeField]
        private BaseMachine _baseMachine;
        [SerializeField]
        private SnapZone _snapZone;

        private TasksCollection _tasksCollection;
        public bool IsEnter;
        public bool IsStart;
        [Inject]
        public void Construct(TasksCollection tasksCollection)
        {
            _tasksCollection = tasksCollection;
        }

        private void Update()
        {
            if (_snapZone.HeldItem is null ||
                _snapZone.HeldItem.gameObject.GetComponent<MixContainer>() is null ||
                _snapZone.HeldItem.gameObject.GetComponent<MixContainer>().Anchor is null)
            {
                IsEnter = false;
                return;
            }

            if (!IsEnter)
            {
                OnEnterObject();
            }
        }

        public void OnEnterObject()
        {
            Debug.Log("Enter" + _snapZone.HeldItem.gameObject.GetComponent<BaseContainer>().Substance.SubParams.SubName);
            _tasksCollection.CheckEnteringIntoMachine(_baseMachine.MachinesType,
                _snapZone.HeldItem.gameObject.GetComponent<BaseContainer>().ContainersType);
            IsEnter = true;
        }
        
        public void OnStartWork()
        {
            if (!IsEnter)
            {
                return;
            }
            IsStart = true;
            Debug.Log( "Start " +_snapZone.HeldItem.gameObject.GetComponent<BaseContainer>().Substance.SubParams.SubName);
            _snapZone.HeldItem.gameObject.GetComponent<MixContainer>().StirringSubstance();
            StartStirringAnimation();
            _tasksCollection.CheckStartMachineWork(_baseMachine.MachinesType);
        }
        public void OnFinishWork()
        {
            IsStart = false;
            StopStirringAnimation();
            _tasksCollection.CheckFinishMachineWork(_baseMachine.MachinesType, _snapZone.HeldItem.gameObject.GetComponent<BaseContainer>().Substance);
        }

        private void StartStirringAnimation()
        {
            _snapZone.HeldItem.gameObject.GetComponent<MixContainer>().Anchor.gameObject.GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<Animator>().enabled = true;
        }
        
        private void StopStirringAnimation()
        {
            _snapZone.HeldItem.gameObject.GetComponent<MixContainer>().Anchor.gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }
}
