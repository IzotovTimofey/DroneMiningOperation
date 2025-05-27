using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<BaseController> _bases;
    [SerializeField] private Slider _droneCountChanger;

    private int _maxDroneCount = 5;

    private void OnEnable()
    {
        _droneCountChanger.onValueChanged.AddListener(OnDroneCountChanged);
    }

    public void OnDroneSpeedChanged(float value)
    {

    }

    public void OnDroneCountChanged(float value)
    {
        foreach (var bas in _bases)
        {
            bas.SetDronesCount((int)value);
        }
    }

    public void OnShowPathToggled(bool state)
    {

    }

    public void OnInput(int value)
    {

    }

    public void OnButtonPressed()
    {

    }

}
