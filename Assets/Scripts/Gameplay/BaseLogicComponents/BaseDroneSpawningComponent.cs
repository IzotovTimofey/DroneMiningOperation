using UnityEngine;

public class BaseDroneSpawningComponent : MonoBehaviour
{
    [SerializeField] private int _startingDronesAmount = 1;
    [SerializeField] private DroneFactory _droneFactory;
    [SerializeField] private ResourceFactory _resourceFactory;

    [SerializeField] private Transform[] _startingPositionsWPs;

    private void Start()
    {
        SpawnDrone(_startingDronesAmount);
    }

    private void SpawnDrone(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var drone = _droneFactory.GetDrone();
            drone.Agent.Warp(_startingPositionsWPs[i].position);
            drone.ActivateDrone(GetClosestResource);
        }
    }

    private Resource GetClosestResource()
    {
        return _resourceFactory.GetClosestAvailableResource(transform.position);
    }
}
