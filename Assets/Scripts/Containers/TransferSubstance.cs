using UnityEngine;
using BNG;
using System.Collections.Generic;
using Containers;
using Interfaces;
using Tasks;
using Zenject;
//using System.Diagnostics;
//using System.ComponentModel;
//using System.Diagnostics;

public class TransferSubstance : MonoBehaviour
{
    [SerializeField]
    private BaseContainer _baseContainer;

    private bool _isAgain = false;
    private Grabber _leftGrabber, _rightGrabber;
    private IContainer _container;
    private TasksCollection _tasksCollection;

    [Inject]
    public void Construct(List<Grabber> grabbers, TasksCollection tasksCollection)
    {
        foreach (var grabber in grabbers)
        {
            if(grabber.HandSide == ControllerHand.Left)
            {
                _leftGrabber = grabber;
            }
            else
            {
                _rightGrabber = grabber;
            }
        }
        _tasksCollection = tasksCollection;
    }
    private void Awake()
    {
        _container = GetComponent<IContainer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isAgain)
        {
            return;
        }
        if(other.GetComponent<BaseContainer>() is null || other.GetComponent<TransferSubstance>() is null)
        {
            return;
        }

        if (!OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && !OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            return;
        }

        Grabber grabber = null;

        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            grabber = _rightGrabber;
        }
        else
        {
            grabber = _leftGrabber;
        }

        if (grabber.HeldGrabbable is null || grabber.HeldGrabbable.gameObject != gameObject)
        {
            return;
        }
        
        Transfer(other.gameObject);
        _isAgain = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isAgain = false;
    }

    private void Transfer(GameObject triggerGameObject)
    {
        if (triggerGameObject.GetComponent<IContainer>() is null)
        {
            return;
        }
        var temp = false;

        if (_baseContainer.Substance is null)
        {
            //если это ложка
            if (!GetComponent<SpoonContainer>())
            {
                return;
            }
            if (triggerGameObject.GetComponent<BaseContainer>().Substance is null)
            { 
                return;
            }

            temp = _container.AddSubstance(triggerGameObject.GetComponent<BaseContainer>().Substance);
            if (!temp) return;
            triggerGameObject.GetComponent<IContainer>().RemoveSubstance();
            _tasksCollection.CheckTransferSubstance(_baseContainer, triggerGameObject.GetComponent<BaseContainer>(), _baseContainer.Substance);
            return;
        }
        
        temp = triggerGameObject.GetComponent<IContainer>().AddSubstance(_baseContainer.Substance);
        if (!temp) return;
        _container.RemoveSubstance();
        _tasksCollection.CheckTransferSubstance(_baseContainer, triggerGameObject.GetComponent<BaseContainer>(), triggerGameObject.GetComponent<BaseContainer>().Substance);
    }
}
