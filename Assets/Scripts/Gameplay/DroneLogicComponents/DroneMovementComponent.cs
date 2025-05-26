using System;
using UnityEngine;
using UnityEngine.AI;

public class DroneMovementComponent : MonoBehaviour
{
    [SerializeField] private float _destinationReachingMagnitute = 0.5f;

    private NavMeshAgent _agent;
    private Transform _currentTarget;
    private Action _onDestinationReached;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_currentTarget == null)
            return;
        _agent.destination = _currentTarget.transform.position;
        if ((_currentTarget.transform.position - _agent.transform.position).magnitude <= _destinationReachingMagnitute)
        {
            _currentTarget = null;
            _onDestinationReached?.Invoke();
        }

    }

    public void SetDestination(Transform destination, Action onDestinationReached)
    {
        _onDestinationReached = onDestinationReached;
        _currentTarget = destination;
    }
}
