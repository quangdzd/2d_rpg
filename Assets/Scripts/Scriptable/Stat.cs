using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Stat", menuName = "ScriptableObject/Stat", order = 0)]
public class Stat : ScriptableObject
{
    [Header("Name")]
    public string Name; 
    [Header("MinDmg-MaxDmg")]
    public int[] baseattack = new int[2];

    [Header("BaseStat-Float")]
    public float speed;
    public float attack_threshold;

    [Header("BaseStat_Int")]
    public int defense_physic;
    public int defense_magic;

    public int Max_Hp;
    public int Max_Mp;

    [Header("Percent")]
    public int critical;
    public int critical_change;
    public int attack_speed;

    [Header("Recovery/5s")]
    public float Hp_recovery;
    public float Mp_recovery;
    
}

