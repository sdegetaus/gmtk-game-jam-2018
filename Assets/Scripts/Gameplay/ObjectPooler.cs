﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public PoolTypes tag;
    public GameObject objectPrefab;
    public int size;


    public Pool(GameObject obj, int amt)
    {
        objectPrefab = obj;
        size = Mathf.Max(amt, 2);
    }
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
    public static int numberOnObject
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.objectPrefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag.ToString(), objectPool);
        }
    }
    

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool doesnt exist");
            return null;
        }

        GameObject objectToSpawn =  poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        if(pooledObj != null)
        {
            if (tag.Equals(PoolTypes.Enviroment.ToString()))
                pooledObj.OnObjectSpawn(new Vector3(40f, 0, 0));
            else
                pooledObj.OnObjectSpawn(new Vector3(15f, 0, 0));
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }


}
