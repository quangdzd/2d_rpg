using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : Singleton<HUD>
{
    public PickupManager pickupManager;


    public void PickupManager_Add(string key, Item item)
    {
        pickupManager.Add(key, item);
    }
    public void PickupManager_Remove(string key)
    {
        pickupManager.Remove(key);
    }
    public Item PickupManager_Pickup()
    {
        return pickupManager.Pickup();
    }
    

    
}
