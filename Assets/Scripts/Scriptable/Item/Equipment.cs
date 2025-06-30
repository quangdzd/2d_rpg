using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObject/Item/Equipment")]
public class Equipment : ScriptableObject, IItem
{
    public EItemType itemType = EItemType.Equipment;
    [Header("Base Info")]
    public string EquipmentName;
    public EEquipment eEquipment;


    public Vector2Int baseattack;
    public int critical_change;
    public float criticalMultiplier;
    public float attack_speed;
    public float defense_physic;
    public float defense_magic;

    public int Strength;
    public int Endurance;
    public int Intelligence;
    public int Life;
    public int Mana;


    public float Max_Hp;
    public float Max_Mp;
    public float Hp_recovery;
    public float Mp_recovery;




    [Header("System")] public List<Requirement> requirements;

    [Header("Both")] public Sprite icon;
    public Sprite image => icon;
    public string itemName => EquipmentName;
    public EItemType ItemType => itemType;

    public void Use()
    {
        PlayerStats playerStats = GameManager.Instance.playerStats;

        playerStats.buffStats.baseAttack += baseattack;
        playerStats.buffStats.bonus_criticalChance += critical_change;
        playerStats.buffStats.bonus_criticalMultiplier += criticalMultiplier;
        playerStats.buffStats.bonus_attackSpeed += attack_speed;

        playerStats.buffStats.bonus_magicalDefenseFlat += defense_magic;
        playerStats.buffStats.bonus_physicalDefenseFlat += defense_physic;

        playerStats.buffStats.bonus_strength += Strength;
        playerStats.buffStats.bonus_endurance += Endurance;
        playerStats.buffStats.bonus_intelligence += Intelligence;
        playerStats.buffStats.bonus_life += Life;
        playerStats.buffStats.bonus_mana += Mana;

        playerStats.buffStats.bonus_maxHpFlat += Max_Hp;
        playerStats.buffStats.bonus_maxMpFlat += Max_Mp;
        playerStats.buffStats.bonus_hpRegen += Hp_recovery;
        playerStats.buffStats.bonus_mpRegen += Mp_recovery;


        playerStats.CaculateStats();

    }

    public void UnEquip()
    {
        PlayerStats playerStats = GameManager.Instance.playerStats;

        playerStats.buffStats.baseAttack -= baseattack;
        playerStats.buffStats.bonus_criticalChance -= critical_change;
        playerStats.buffStats.bonus_criticalMultiplier -= criticalMultiplier;
        playerStats.buffStats.bonus_attackSpeed -= attack_speed;

        playerStats.buffStats.bonus_magicalDefenseFlat -= defense_magic;
        playerStats.buffStats.bonus_physicalDefenseFlat -= defense_physic;

        playerStats.buffStats.bonus_strength -= Strength;
        playerStats.buffStats.bonus_endurance -= Endurance;
        playerStats.buffStats.bonus_intelligence -= Intelligence;
        playerStats.buffStats.bonus_life -= Life;
        playerStats.buffStats.bonus_mana -= Mana;

        playerStats.buffStats.bonus_maxHpFlat -= Max_Hp;
        playerStats.buffStats.bonus_maxMpFlat -= Max_Mp;
        playerStats.buffStats.bonus_hpRegen -= Hp_recovery;
        playerStats.buffStats.bonus_mpRegen -= Mp_recovery;


        playerStats.CaculateStats();
    }
}

[System.Serializable]
public class Requirement
{
    public EStatType statName;
    public int value;
}

public enum EEquipment
{
    Helmet,
    Sword,
    Shield,
    Armor,
    Shoes,
    Pants,
    Gloves,
    Ring1,
    Ring2,
    Necklace,

}
