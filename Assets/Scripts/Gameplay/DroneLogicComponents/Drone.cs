using System;
using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private DroneMovementComponent _movementComponent;
    [SerializeField] private Renderer _renderer;

    private Resource _currentResource;
    private Action<int, Drone> _storeResourceValue;
    private Transform _baseWP;
    private int _currentResourceValue;

    public NavMeshAgent Agent => _agent;
    public Renderer Renderer => _renderer;

    public void ActivateDrone(Action<int, Drone> storeResourceValueCallback, Transform baseWP)
    {
        _storeResourceValue = storeResourceValueCallback;
        _baseWP = baseWP;
    }

    public void SendToResource(Resource resource)
    {
        _currentResource = resource;
        _movementComponent.SetDestination(_currentResource.gameObject.transform, CollectResource);
    }

    public void CollectResource()
    {
        _currentResource.StartCollection(OnResourceCollected);
    }

    private void OnResourceCollected(int value)
    {
        _currentResourceValue = value;
        _movementComponent.SetDestination(_baseWP, LoadResourceToBase);
    }

    private void LoadResourceToBase()
    {
        _storeResourceValue?.Invoke(_currentResourceValue, this);
        //TODO : Создать эффект выгрузки
    }
}
