using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private PooledObject[] _availablePrefabs;
    public static ObjectPoolManager Instance { get; private set; }

    private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, List<GameObject>> _pooledObjects = new Dictionary<string, List<GameObject>>(); 
    private Dictionary<string, List<GameObject>> _activeObjects = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this);
            }
        }
    }

    private void Start()
    {
        foreach(PooledObject o  in _availablePrefabs)
        {
            if (_pooledObjects.ContainsKey(o.Name)) continue;
            _pooledObjects[o.Name] = new List<GameObject>();
            _activeObjects[o.Name] = new List<GameObject>();
            if (_prefabs.ContainsKey(o.Name)) continue;
            _prefabs[o.Name] = o.Prefab;
        }
    }

    public GameObject GetPrefab(string name)
    {
        if (!_prefabs.ContainsKey(name)) return null;
        GameObject newObject;
        if (_pooledObjects[name].Count <= 0)
        {
            newObject = Instantiate(_prefabs[name]);
            _activeObjects[name].Add(newObject);
        }
        else
        {
            newObject = _pooledObjects[name].First();
            _pooledObjects[name].Remove(newObject);
            newObject.SetActive(true);
            _activeObjects[name].Add(newObject);
        }
        return newObject;
    }

    public void Uninstantiate(string name, GameObject obj)
    {
        if (!_activeObjects.ContainsKey(name)) return;
        obj.SetActive(false);
        _activeObjects[name].Remove(obj);
        _pooledObjects[name].Add(obj);
    }
}

[Serializable]
public class PooledObject
{
    [field: SerializeField] public GameObject Prefab;
    [field:SerializeField] public string Name;
}

