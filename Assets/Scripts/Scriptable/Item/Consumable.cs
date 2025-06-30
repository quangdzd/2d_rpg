using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "ScriptableObject/Item/Consumable")]
public class Consumable : ScriptableObject, IItem
{
    public EItemType itemType = EItemType.Consumable;

    [Header("Basic Info")]
    public string consumableName;
    public Sprite icon;
    public string description;

    [Header("Effect")]
    public int flatRestoreHP;
    public int flatRestoreMP;
    public float percentRestoreHP;
    public float percentRestoreMP;
    public int buffStrength;
    public float duration;

    [Header("System")]
    public bool stackable;



    public Sprite image => icon;
    public string itemName => this.consumableName;
    public EItemType ItemType => itemType;
    public EConsumableType consumableType;


    public void Use()
    {
        AHpManager aHpManager = GameManager.Instance.playerHpManger;
        
        if (flatRestoreHP != 0)
        {
            aHpManager.FlatRestoreHP(flatRestoreMP);
        }
        if (percentRestoreHP != 0)
        {
            aHpManager.PercentRestoreHP(percentRestoreHP);
        }

        if (flatRestoreMP != 0)
        {
            aHpManager.FlatRestoreMP(flatRestoreMP);
        }
        if (percentRestoreMP != 0)
        {
            aHpManager.PercentRestoreMP(percentRestoreMP);
        }

    }
}

public enum EConsumableType
{
    Healing,
    Mana,
    Buff,
    Cure,
    Scroll,
    Food
}
