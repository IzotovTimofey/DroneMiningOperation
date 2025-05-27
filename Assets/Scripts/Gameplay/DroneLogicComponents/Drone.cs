using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private DroneMovementComponent _movementComponent;
    [SerializeField] private Renderer _renderer;

    private Resource _currentResource;
    private Action<int, Drone> _storeResourceValue;
    private DroneSpot _droneSpot;
    private int _currentResourceValue;

    public Renderer Renderer => _renderer;

    public void ActivateDrone(Action<int, Drone> storeResourceValueCallback, DroneSpot droneSpot)
    {
        _storeResourceValue = storeResourceValueCallback;
        _droneSpot = droneSpot;
        _agent.Warp(droneSpot.DroneWP.position);
    }

    public void SendToResource(Resource resource)
    {
        _currentResource = resource;
        _movementComponent.SetDestination(_currentResource.gameObject.transform, CollectResource);
    }

    public void CollectResource()
    {
        StartCoroutine(CollectionCourutine());
    }

    private IEnumerator CollectionCourutine()
    {
        yield return new WaitForSeconds(_currentResource.CollectionTime);
        if (gameObject.activeSelf)
            OnResourceCollected(_currentResource.Collect());
    }

    private void OnResourceCollected(int value)
    {
        _currentResourceValue = value;
        _movementComponent.SetDestination(_droneSpot.DroneWP, LoadResourceToBase);
    }

    private void LoadResourceToBase()
    {
        _storeResourceValue?.Invoke(_currentResourceValue, this);
        //TODO : Создать эффект выгрузки
    }

    public void Release()
    {
        if (_currentResource != null)
            _currentResource.SetFree();
        _droneSpot.ReleaseSpot();
        gameObject.SetActive(false);
    }
}
