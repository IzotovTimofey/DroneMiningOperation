using TMPro;
using UnityEngine;

public class ResourceCounterUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text _counter;
    [SerializeField] private BaseDroneSpawningComponent _base;

    private void OnEnable()
    {
        _base.StoredValueChanged += UpdateResourceCounter;
    }

    private void OnDisable()
    {
        _base.StoredValueChanged -= UpdateResourceCounter;
    }

    private void UpdateResourceCounter(int value)
    {
        _counter.text = value.ToString();
    }
}
