using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{

    [Header("Base Stats")]
    public Vector2Int attack;
    public int criticalChance;
    public float criticalMultiplier;
    public float attackSpeed;

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

    public void CaculateStats(BaseStats baseStats, BuffStats buffStats)
    {
        // Base Stats
        attack = new Vector2Int(
            Mathf.FloorToInt(baseStats.baseAttack.x * (1 + buffStats.percentAttack / 100f) + buffStats.baseAttack.x),
            Mathf.FloorToInt(baseStats.baseAttack.y * (1 + buffStats.percentAttack / 100f) + buffStats.baseAttack.y)
        );

        criticalChance = baseStats.criticalChance + buffStats.bonus_criticalChance;
        criticalMultiplier = baseStats.criticalMultiplier + buffStats.bonus_criticalMultiplier;
        attackSpeed = baseStats.attackSpeed + buffStats.bonus_attackSpeed;

        physicalDefense = baseStats.physicalDefense
                        + buffStats.bonus_physicalDefenseFlat
                        + baseStats.physicalDefense * buffStats.bonus_physicalDefensePercent / 100f;

        magicalDefense = baseStats.magicalDefense
                    + buffStats.bonus_magicalDefenseFlat
                    + baseStats.magicalDefense * buffStats.bonus_magicalDefensePercent / 100f;

        // Core Attributes
        strength = baseStats.strength + buffStats.bonus_strength;
        endurance = baseStats.endurance + buffStats.bonus_endurance;
        intelligence = baseStats.intelligence + buffStats.bonus_intelligence;
        life = baseStats.life + buffStats.bonus_life;
        mana = baseStats.mana + buffStats.bonus_mana;

        // Vitals
        maxHp = baseStats.maxHp
            + buffStats.bonus_maxHpFlat
            + baseStats.maxHp * buffStats.bonus_maxHpPercent / 100f;

        maxMp = baseStats.maxMp
            + buffStats.bonus_maxMpFlat
            + baseStats.maxMp * buffStats.bonus_maxMpPercent / 100f;

        hpRegen = baseStats.hpRegen + buffStats.bonus_hpRegen;
        mpRegen = baseStats.mpRegen + buffStats.bonus_mpRegen;
    }
    public void CaculateAttack(BaseStats baseStats, BuffStats buffStats)
    {
        attack = new Vector2Int(
            Mathf.FloorToInt(baseStats.baseAttack.x * (1 + buffStats.percentAttack / 100f) + buffStats.baseAttack.x),
            Mathf.FloorToInt(baseStats.baseAttack.y * (1 + buffStats.percentAttack / 100f) + buffStats.baseAttack.y)
        );
    }
    public void CalculateCritical(BaseStats baseStats, BuffStats buffStats)
    {
        criticalChance = baseStats.criticalChance + buffStats.bonus_criticalChance;
        criticalMultiplier = baseStats.criticalMultiplier + buffStats.bonus_criticalMultiplier;
    }

    public void CalculateAttackSpeed(BaseStats baseStats, BuffStats buffStats)
    {
        attackSpeed = baseStats.attackSpeed + buffStats.bonus_attackSpeed;
    }

    public void CalculatePhysicalDefense(BaseStats baseStats, BuffStats buffStats)
    {
        physicalDefense = baseStats.physicalDefense
            + buffStats.bonus_physicalDefenseFlat
            + baseStats.physicalDefense * buffStats.bonus_physicalDefensePercent / 100f;
    }

    public void CalculateMagicalDefense(BaseStats baseStats, BuffStats buffStats)
    {
        magicalDefense = baseStats.magicalDefense
            + buffStats.bonus_magicalDefenseFlat
            + baseStats.magicalDefense * buffStats.bonus_magicalDefensePercent / 100f;
    }

    public void CalculateCoreAttributes(BaseStats baseStats, BuffStats buffStats)
    {
        strength = baseStats.strength + buffStats.bonus_strength;
        endurance = baseStats.endurance + buffStats.bonus_endurance;
        intelligence = baseStats.intelligence + buffStats.bonus_intelligence;
        life = baseStats.life + buffStats.bonus_life;
        mana = baseStats.mana + buffStats.bonus_mana;
    }

    public void CalculateMaxHp(BaseStats baseStats, BuffStats buffStats)
    {
        maxHp = baseStats.maxHp
            + buffStats.bonus_maxHpFlat
            + baseStats.maxHp * buffStats.bonus_maxHpPercent / 100f;
    }

    public void CalculateMaxMp(BaseStats baseStats, BuffStats buffStats)
    {
        maxMp = baseStats.maxMp
            + buffStats.bonus_maxMpFlat
            + baseStats.maxMp * buffStats.bonus_maxMpPercent / 100f;
    }

    public void CalculateHpRegen(BaseStats baseStats, BuffStats buffStats)
    {
        hpRegen = baseStats.hpRegen + buffStats.bonus_hpRegen;
    }

    public void CalculateMpRegen(BaseStats baseStats, BuffStats buffStats)
    {
        mpRegen = baseStats.mpRegen + buffStats.bonus_mpRegen;
    }

    
    

}
