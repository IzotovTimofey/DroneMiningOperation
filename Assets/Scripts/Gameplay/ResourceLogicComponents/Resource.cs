using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private float _collectionTime = 2f;
    [SerializeField] private int _resourceValue = 10;

    private bool _isAvailable = true;

    public bool IsAvailable => _isAvailable;
    public float CollectionTime => _collectionTime;

    private void OnEnable()
    {
        _isAvailable = true;
    }

    public void LockResource()
    {
        _isAvailable = false;
    }

    public void SetFree()
    {
        _isAvailable = true;
    }

    public int Collect()
    {
        gameObject.SetActive(false);
        return _resourceValue;
    }
}
