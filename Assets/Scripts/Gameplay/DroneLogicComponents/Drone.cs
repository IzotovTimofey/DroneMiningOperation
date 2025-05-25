using System;
using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private Resource _currentTarget;
    private bool _isActive;
    private Func<Resource> _getNewTargetCallback;

    public NavMeshAgent Agent => _agent;
    public bool IsActive => _isActive;
    public Resource CurrentTarget => _currentTarget;

    public void ActivateDrone(Func<Resource> getNewTargetcallback)
    {
        _getNewTargetCallback = getNewTargetcallback;
        _isActive = true;
    }

    public void GetNewTarget()
    {
        _currentTarget = _getNewTargetCallback?.Invoke();
    }

}
