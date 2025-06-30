using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public EPool pool_id;
    public GameObject prefab;
    public int size;
    public float z_order;

    public Pool(EPool pool_id, GameObject prefab, int size, int z_order = 0)
    {
        this.pool_id = pool_id;
        this.prefab = prefab;
        this.size = size;
        this.z_order = z_order;
    }
}



public class PoolManager : SingletonDestroy<PoolManager>
{
    public List<Pool> pools;
    private Dictionary<EPool, Queue<GameObject>> poolDict;
    protected override void Awake()
    {
        base.Awake();
        poolDict = new Dictionary<EPool, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            poolDict.Add(pool.pool_id, objectQueue);
        }
    }
    public GameObject GetFromPool(EPool tag, Vector3 position, Quaternion rotation, bool setpos = true, bool setZ_order = true)
    {

        if (poolDict.TryGetValue(tag, out var pool))
        {

            GameObject obj;
            var foundPool = pools.Find(x => x.pool_id == tag);

            if (pool.Count > 0)
            {
                obj = pool.Dequeue();

                obj.SetActive(true);

            }
            else
            {


                if (foundPool == null)
                {
                    Debug.LogWarning("Prefab with tag " + tag + " not found in pool list.");
                    return null;
                }
                obj = Instantiate(foundPool.prefab);
                obj.SetActive(true);

            }

            if (setpos)
            {
                obj.transform.position = position;

                if (setZ_order)
                {
                    foundPool.z_order += 0.01f;
                    obj.transform.position = new Vector3(position.x, position.y, -foundPool.z_order);
                }
                obj.transform.rotation = rotation;
            }


            return obj;
        }
        else
        {

            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }


    }

    public void AddToPool(GameObject obj, EPool pool_id)
    {
        obj.SetActive(false);
        if (poolDict.TryGetValue(pool_id, out var poolqueue))
        {
            Pool pool = pools.Find(x => x.pool_id == pool_id);
            if (poolqueue.Count == pool.size)
            {
                pool.z_order = 0;
                pool.size++;
            }
            if (pool.z_order <= -1)
            {
                pool.z_order = 0;
            }

            poolqueue.Enqueue(obj);
        }
        else
        {
            Pool newpool = new Pool(pool_id, obj, 1);
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            poolDict.Add(pool_id, objectQueue);
        }
    } 
    


    
}
