using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pool
{
    public string tag;
    public string rcode;
    public MonoBehaviour prefab;
    public int size;
}

public class PoolManager : Singleton<PoolManager>
{
    public List<Pool> Pools;
    public Dictionary<string, Queue<MonoBehaviour>> PoolDictionary;
    private Transform objectPoolParent;

    protected override void Awake()
    {
        isDontDestroyOnLoad = false;
        base.Awake();

        PoolDictionary = new Dictionary<string, Queue<MonoBehaviour>>();
        objectPoolParent = new GameObject("ObjectPool").transform;
    }

    private void Start()
    {
        foreach (var pool in Pools)
        {
            CreatePool(pool);
        }
    }

    public T SpawnFromPool<T>(string rcode) where T : MonoBehaviour
    {
        if (!PoolDictionary.ContainsKey(rcode)) return default;

        MonoBehaviour obj = PoolDictionary[rcode].Dequeue();
        PoolDictionary[rcode].Enqueue(obj);
        obj.gameObject.SetActive(true);
        return (T)obj;
    }

    private void CreatePool(Pool pool)
    {
        if (PoolDictionary.ContainsKey(pool.rcode)) return;

        Queue<MonoBehaviour> objectPool = new Queue<MonoBehaviour>();
        Transform rcodeParent = new GameObject(pool.rcode).transform;
        rcodeParent.SetParent(objectPoolParent);


        for (int i = 0; i < pool.size; i++)
        {
            MonoBehaviour obj = Instantiate(pool.prefab, rcodeParent);
            obj.GetComponent<PoolObject>().SetData(pool.rcode);
            obj.gameObject.SetActive(false);
            objectPool.Enqueue(obj);
        }

        PoolDictionary.Add(pool.rcode, objectPool);
    }
}
