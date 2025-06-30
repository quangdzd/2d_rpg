using UnityEngine;

[System.Serializable]
public class BuffStats
{
    [Header("Base Stats")]
    public Vector2Int baseAttack;         // [min, max]
    public int percentAttack;

    public int bonus_criticalChance;            // %
    public float bonus_criticalMultiplier ;
    public float bonus_attackSpeed ;

    public float bonus_physicalDefenseFlat;
    public float bonus_magicalDefenseFlat;
    public float bonus_physicalDefensePercent;
    public float bonus_magicalDefensePercent;

    [Header("Core Attributes")]
    public int bonus_strength;
    public int bonus_endurance;
    public int bonus_intelligence;
    public int bonus_life;
    public int bonus_mana;

    [Header("Vitals")]
    public float bonus_maxHpFlat;
    public float bonus_maxMpFlat;
    public float bonus_maxHpPercent;
    public float bonus_maxMpPercent;
        
    public float bonus_hpRegen;
    public float bonus_mpRegen;
}
