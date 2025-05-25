using UnityEngine;
using UnityEngine.AI;

public class DroneMovementComponent : MonoBehaviour
{
    [SerializeField] private Drone _drone;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!_drone.IsActive)
            return;
        if (_drone.CurrentTarget != null)
            _agent.destination = _drone.CurrentTarget.transform.position;
        else
            _drone.GetNewTarget();
    }
}
