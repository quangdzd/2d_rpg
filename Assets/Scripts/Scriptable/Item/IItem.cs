using UnityEngine;
public interface IItem
{
    Sprite image { get; }
    string itemName { get; }
    EItemType ItemType { get; }
    void Use();
    

}

public enum EItemType
{
    Equipment,
    Consumable,
}
