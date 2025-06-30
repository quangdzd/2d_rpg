using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : SingletonDestroy<BoidManager>
{



    public SpatialHashing<ABoidMovement> spatialHashing;
    public List<ABoidMovement> listBoids;

    protected override void Awake()
    {
        base.Awake();

        spatialHashing = new SpatialHashing<ABoidMovement>(0.66f);
        listBoids = new List<ABoidMovement>();

    }
    void Start()
    {
    }


    void Update()
    {
        foreach (var enemy in listBoids)
        {
            spatialHashing.UpdateUnit(enemy);

        }
       
    }

    public void ResisterMembers(ABoidMovement aBoid)
    {
        spatialHashing.Add(aBoid);
        listBoids.Add(aBoid);
        Debug.Log(listBoids.Count);

    }
    public void UnResisterMembers(ABoidMovement aBoid)
    {
        spatialHashing.Remove(aBoid);
        listBoids.Remove(aBoid);
    }
    public List<ABoidMovement> GetEnemiesNerest(Vector2 pos, int radiusInCells = 1)
    {
        return spatialHashing.QueryNearby(pos, radiusInCells);
    }

}
