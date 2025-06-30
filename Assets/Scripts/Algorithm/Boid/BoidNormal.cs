using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidNormal : ABoidMovement
{
    protected override void Awake()
    {
        EnemyRank = EEnemyRank.Normal;
        radius = 3f;
        Velocity = Random.insideUnitCircle.normalized;
    }
}
