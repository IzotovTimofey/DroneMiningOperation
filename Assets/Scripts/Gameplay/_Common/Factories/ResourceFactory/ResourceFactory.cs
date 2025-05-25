using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

public class ResourceFactory : MonoBehaviour
{
    [SerializeField] private GameObject _resourcePrefab;
    [SerializeField] private Transform _resourcesPoolParent;
    [SerializeField] private int _resourcesPoolSize = 30;
    [SerializeField] private int _startResourceAmount;
    [SerializeField] private int _minResourceAmountGenerated = 1;
    [SerializeField] private int _maxResourceAmountGenerated = 3;
    [SerializeField] private float _resourceGenerationTimer = 5;

    [SerializeField] private Transform _boundary1;
    [SerializeField] private Transform _boundary2;

    private GenericPool<Resource> _resourcePool;
    private bool _isSpawning = true;
    private Vector3 _randomSpawningPosition;
    private List<Resource> _activeResrourcesList;

    private void Awake()
    {
        _resourcePool = new(_resourcePrefab, _resourcesPoolSize, _resourcesPoolParent);
    }

    private void Start()
    {
        for (int i = 0; i < _startResourceAmount; i++)
        {
            SpawnResource();
        }
        StartCoroutine(SpawnResourcesCourutine());
    }

    private Resource SpawnResource()
    {
        Resource resource = _resourcePool.GetObjectFromPool(true);
        Vector3 position = Vector3.zero;
        NavMeshHit hit;
        while (position == Vector3.zero)
        {
            var pos = GetSpawningPosition();
            if (NavMesh.SamplePosition(pos, out hit, 2, NavMesh.AllAreas))
            {
                position = hit.position;
            }
        }
        resource.transform.position = position;
        return resource;
    }

    private IEnumerator SpawnResourcesCourutine()
    {
        while (_isSpawning)
        {
            yield return new WaitForSeconds(_resourceGenerationTimer);
            int randomCount = Random.Range(_minResourceAmountGenerated, _maxResourceAmountGenerated);
            for (int i = 0; i < randomCount; i++)
            {
                SpawnResource();
            }
        }
    }

    private Vector3 GetSpawningPosition()
    {
        _randomSpawningPosition = new Vector3(
                Random.Range(_boundary1.position.x, _boundary2.position.x),
                0,
                Random.Range(_boundary1.position.z, _boundary2.position.z));
        return _randomSpawningPosition;
    }

    public Resource GetClosestAvailableResource(Vector3 basePosition)
    {
        Dictionary<Resource, float> resourcesDistance = new Dictionary<Resource, float>();
        var activeObjects = _resourcePool.GetActiveObjects();
        foreach (var obj in activeObjects)
        {
            if (!obj.IsAvailable)
                continue;
            resourcesDistance.Add(obj, (obj.transform.position - transform.position).magnitude);
        }
        if (resourcesDistance.Count != 0)
        {
            var closestObjPair = resourcesDistance.OrderBy(pair => pair.Value).First();
            return closestObjPair.Key;
        }
        else
            return SpawnResource();
    }
}
