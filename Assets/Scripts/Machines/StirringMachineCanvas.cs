using System.Collections;
using System.Collections.Generic;
using Machines;
using UnityEngine;

public class StirringMachineCanvas : MonoBehaviour
{
    private bool _isHeating = false;
    private bool _isStirring = false;

    [SerializeField] private Material _heatingButtonMaterial, _stirringButtonMaterial;
    [SerializeField] private Texture _onTexture, _offTexture;
    [SerializeField] StirringMachineCntrl _heatMachineCntrl;
    private void Start()
    {
        _heatingButtonMaterial.mainTexture = _offTexture;
        _stirringButtonMaterial.mainTexture = _offTexture;
    }
    public void ClickHeatingBtn()
    {
        _isHeating = !_isHeating;
        if (_isHeating)
            _heatingButtonMaterial.mainTexture = _onTexture;
        else
            _heatingButtonMaterial.mainTexture = _offTexture;
        TryStart();
    }
    public void ClickStirringBtn()
    {
        _isStirring = !_isStirring;
        if (_isStirring)
            _stirringButtonMaterial.mainTexture = _onTexture;
        else
            _stirringButtonMaterial.mainTexture = _offTexture;
        TryStart();
    }

    public void TryStart()
    {
        Debug.Log($"buttons: heating - {_isHeating}; stirring - {_isStirring}");
        if (_isHeating && _isStirring)
        {
            _heatMachineCntrl.StartStirring();
        }
    }
}
