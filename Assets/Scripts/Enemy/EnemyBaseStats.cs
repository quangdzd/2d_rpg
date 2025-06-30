using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBaseStats : BaseStats
{
    [Header("Only Enemy")]
    public string EnemyName;
    public float attack_threshold;

}
