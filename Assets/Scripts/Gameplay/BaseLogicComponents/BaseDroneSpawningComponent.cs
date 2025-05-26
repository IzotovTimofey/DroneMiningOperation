using UnityEngine;
using UnityEngine.Events;

public class BaseDroneSpawningComponent : MonoBehaviour
{
    [SerializeField] private TeamAssigner _teamAssigner;
    [Space(15)]
    [Header("Setup")]
    [SerializeField] private int _startingDronesAmount = 1;
    [SerializeField] private DroneFactory _droneFactory;
    [SerializeField] private ResourceFactory _resourceFactory;

    [SerializeField] private Transform[] _dronesWPs;
    [SerializeField] private Renderer _renderer;

    private int _storedValue;
    private Color _teamColor;

    public event UnityAction<int> StoredValueChanged;

    private void Start()
    {
        _teamColor = _teamAssigner.GetTeamColor();
        SetTeamColor(_renderer);
        SpawnDrone(_startingDronesAmount);
    }

    private void SpawnDrone(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var drone = _droneFactory.GetDrone();
            drone.Agent.Warp(_dronesWPs[i].position);
            SetTeamColor(drone.Renderer);
            drone.ActivateDrone(GetResource, _dronesWPs[i]);
            drone.SendToResource(GetClosestResource());
        }
    }

    private void GetResource(int value, Drone drone)
    {
        _storedValue += value;
        StoredValueChanged?.Invoke(_storedValue);
        drone.SendToResource(GetClosestResource());
    }

    private Resource GetClosestResource()
    {
        var closestResource = _resourceFactory.GetClosestAvailableResource(transform.position);
        closestResource.LockResource();
        return closestResource;
    }

    private void SetTeamColor(Renderer renderer)
    {
        renderer.material.SetColor("_Color", _teamColor);
    }
}
