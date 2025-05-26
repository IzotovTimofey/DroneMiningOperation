using System;
using System.Collections;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private float _collectionTime = 2f;
    [SerializeField] private int _resourceValue = 10;

    private bool _isAvailable = true;
    private Action<int> _onCollection;

    public bool IsAvailable => _isAvailable;

    private void OnEnable()
    {
        _isAvailable = true;
    }

    public void LockResource()
    {
        _isAvailable = false;
    }

    public void StartCollection(Action<int> onCollect)
    {
        _onCollection = onCollect;
        StartCoroutine(CollectionCourutine());
    }

    private IEnumerator CollectionCourutine()
    {
        yield return new WaitForSeconds(_collectionTime);
        _onCollection?.Invoke(_resourceValue);
        gameObject.SetActive(false);
    }

}
