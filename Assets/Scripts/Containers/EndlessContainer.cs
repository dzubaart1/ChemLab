using System;
using Interfaces;
using Substances;
using Tasks;
using UnityEngine;
using Zenject;

namespace Containers
{
    public class EndlessContainer : MonoBehaviour, IContainer
    {
        [SerializeField]
        private BaseContainer _baseContainer;

        [SerializeField]
        private SubstanceParams _substanceParams;
        private TasksCollection _tasksCollection;
        
        [Inject]
        public void Construct(TasksCollection tasksCollection)
        {
            _tasksCollection = tasksCollection;
        }

        private void Start()
        {
            _tasksCollection.Notify += CheckTasks;
        }

        public bool AddSubstance(Substance substance)
        {
            return false;
        }

        public bool RemoveSubstance()
        {
            if (_baseContainer.Substance == null) return false;
            _baseContainer.Substance = null;
            return true;
        }

        public void Awake()
        {
            _baseContainer.BaseFormPrefab.SetActive(true);
            _baseContainer.BaseFormPrefab.GetComponent<MeshRenderer>().material.color = _substanceParams.Color;
        }

        public void CheckTasks()
        {
            if (_tasksCollection.CurrentTask().SubstancesParams is null) return;
            if (_substanceParams.SubName.Equals(_tasksCollection.CurrentTask().SubstancesParams.SubName))
            {
                var substance = new Substance(_substanceParams, _tasksCollection.CurrentTask().Weight);
                _baseContainer.Substance = substance;
            }
        }
    }
}
