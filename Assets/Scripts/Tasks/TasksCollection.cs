using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Containers;
using Machines;
using Substances;
using UnityEngine;

namespace Tasks
{
    public class TasksCollection
    {
        private List<TaskParams> _tasksParamsList;
        private int _taskCurrentId;
        public TasksCollection()
        {
            _taskCurrentId = 0;
            _tasksParamsList = new List<TaskParams>();
            _tasksParamsList = Resources.LoadAll<TaskParams>("Tasks/").ToList();
            _tasksParamsList.Sort(ComapreToTaskParams);
        }

        public delegate void TaskUpdateHandler();
        public event TaskUpdateHandler? Notify; 
        public int ComapreToTaskParams(TaskParams t1, TaskParams t2)
        {
            if (t1.StepId > t2.StepId)
            {
                return 1;
            }

            if (t1.StepId < t2.StepId)
            {
                return -1;
            }

            return 0;
        }

        public TaskParams GetTaskParamsById(int id)
        {
            return _tasksParamsList[id];
        }

        public void MoveToNext()
        {
            if (_taskCurrentId + 1 >= _tasksParamsList.Count) return;
            _taskCurrentId++;
            Notify?.Invoke();
        }

        public TaskParams CurrentTask()
        {
            return GetTaskParamsById(_taskCurrentId);
        }

        public void CheckTransferSubstance(BaseContainer firstContainer, BaseContainer secondContainer, Substance substance)
        {
            if (!CurrentTask().ContainersType.Contains(firstContainer.ContainersType) ||
                !CurrentTask().ContainersType.Contains(secondContainer.ContainersType) /*||
                !CurrentTask().ResultSubstance.SubName.Equals(substance.SubParams.SubName)*/) 
                return;
            Debug.Log($"{CurrentTask().StepId} is done");
            MoveToNext();
        }

        public void CheckEnteringIntoMachine(MachinesTypes machinesType, ContainersTypes enteringContainer)
        {
            if (!CurrentTask().MachinesType.Equals(machinesType) ||
                !CurrentTask().ContainersType.Contains(enteringContainer)) return;
            Debug.Log($"{CurrentTask().StepId} is done");
            MoveToNext();
        }
        public void CheckStartMachineWork(MachinesTypes machinesType)
        {
            if (CurrentTask().MachinesType.Equals(machinesType))
            {
                Debug.Log($"{CurrentTask().StepId} is done");
                MoveToNext();
            }
        }
        
        public void CheckFinishMachineWork(MachinesTypes machinesType, Substance substance)
        {
            if (CurrentTask().MachinesType.Equals(machinesType)
                && CurrentTask().ResultSubstance.Equals(substance.SubParams))
            {
                Debug.Log($"{CurrentTask().StepId} is done");
                MoveToNext();
            }
        }
    }
}