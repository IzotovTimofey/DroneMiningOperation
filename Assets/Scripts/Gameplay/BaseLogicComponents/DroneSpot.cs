using UnityEngine;

public class DroneSpot : MonoBehaviour
{
    [SerializeField] private Transform _droneWP;
    private bool _isFree = true;

    public Transform DroneWP => _droneWP;
    public bool IsFree => _isFree;

    public void LockSpot()
    {
        _isFree = false;
    }

    public void ReleaseSpot()
    {
        _isFree = true;
    }
}
