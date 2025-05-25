using UnityEngine;

public class DroneFactory : MonoBehaviour
{
    [SerializeField] private GameObject _dronePrefab;
    [SerializeField] private Transform _dronePoolParent;
    [SerializeField] private int _dronePoolSize = 10;

    private GenericPool<Drone> _dronePool;

    private void Awake()
    {
        _dronePool = new(_dronePrefab, _dronePoolSize, _dronePoolParent);
    }

    public Drone GetDrone()
    {
        return _dronePool.GetObjectFromPool(true);
    }
}
