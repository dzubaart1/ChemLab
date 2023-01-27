using UnityEngine;
using BNG;
using System.Collections.Generic;
using Containers;
using Interfaces;
using Zenject;

public class TransferSubstance : MonoBehaviour
{
    [SerializeField]
    private BaseContainer _baseContainer;

    private bool _isAgain = false;
    private Grabber _leftGrabber, _rightGrabber;
    private IContainer _container;

    [Inject]
    public void Construct(List<Grabber> grabbers)
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

        if (grabber?.HeldGrabbable is null || grabber.HeldGrabbable.gameObject != gameObject)
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
        var temp = false;
        if ( (triggerGameObject.GetComponent<ContainerWithCup>() is not null && triggerGameObject.GetComponent<ContainerWithCup>().isClosed())
            || (GetComponent<ContainerWithCup>() is not null && GetComponent<ContainerWithCup>().isClosed()))
        {
            return;
        }

        if (_baseContainer.SubParams is null)
        {
            if (!GetComponent<SpoonContainer>())
            {
                return;
            }
            if (triggerGameObject.GetComponent<BaseContainer>().SubParams is null)
            { 
                return;
            }

            temp = _container.AddSubstance(triggerGameObject.GetComponent<BaseContainer>().SubParams);
            if (temp)
            {
                triggerGameObject.GetComponent<IContainer>().RemoveSubstance();
            }
            return;
        }
        
        temp = triggerGameObject.GetComponent<IContainer>().AddSubstance(_baseContainer.SubParams);
        if (temp)
        {
            _container.RemoveSubstance();
        }
    }
}
