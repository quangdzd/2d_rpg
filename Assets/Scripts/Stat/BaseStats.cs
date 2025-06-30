using UnityEngine;

[System.Serializable]
public class BaseStats
{
    [Header("Base Stats")]
    public Vector2Int baseAttack;         // [min, max]
    public int criticalChance;            // %
    public float criticalMultiplier = 1.5f;
    public float attackSpeed = 1f;

    public float physicalDefense;
    public float magicalDefense;

    [Header("Core Attributes")]
    public int strength;
    public int endurance;
    public int intelligence;
    public int life;
    public int mana;

    [Header("Vitals")]
    public float maxHp;
    public float maxMp;
    public float hpRegen;
    public float mpRegen;
}
