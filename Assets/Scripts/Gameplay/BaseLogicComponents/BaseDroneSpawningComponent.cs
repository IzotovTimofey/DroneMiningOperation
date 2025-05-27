using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.Events;

public class BaseDroneSpawningComponent : MonoBehaviour
{
    [SerializeField] private TeamAssigner _teamAssigner;
    [Space(15)]
    [Header("Setup")]
    [SerializeField] private DroneFactory _droneFactory;
    [SerializeField] private ResourceFactory _resourceFactory;
    [SerializeField] private int _startDroneCount;

    [SerializeField] private List<DroneSpot> _droneSpots;
    [SerializeField] private Renderer _renderer;

    private List<Drone> _activeDronesList = new();
    private int _storedValue;
    private Color _teamColor;

    public event UnityAction<int> StoredValueChanged;

    private void Start()
    {
        _teamColor = _teamAssigner.GetTeamColor();
        SetTeamColor(_renderer);
        SpawnDrone(_startDroneCount);
    }

    private void SpawnDrone(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var drone = _droneFactory.GetDrone();
            _activeDronesList.Add(drone);
            var freeSpot = _droneSpots.Where(s => s.IsFree).First();
            freeSpot.LockSpot();

            SetTeamColor(drone.Renderer);
            drone.ActivateDrone(GetResource, freeSpot);
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

    private void ReleaseDrone(int count)
    {
        if (count > _activeDronesList.Count)
            count = _activeDronesList.Count;

        for (int i = 0; i < count; i++)
        {
            var drone = _activeDronesList[i];
            drone.Release();
            _activeDronesList.Remove(drone);
        }
    }

    public void SetDronesCount(int value)
    {
        int droneCountDifference = value - _activeDronesList.Count;
        if (droneCountDifference < 0)
        {
            ReleaseDrone(Mathf.Abs(droneCountDifference));
        }
        else
        {
            SpawnDrone(droneCountDifference);
        }
    }
}
