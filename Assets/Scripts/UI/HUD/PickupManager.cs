using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public Transform pickupContent;
    public Dictionary<string, GameObject> PU_Dicts;
    public List<Item> PU_items;

    public GameObject pickupPrefab;

    private void Awake()
    {
        PU_Dicts = new Dictionary<string, GameObject>();
        PU_items = new List<Item>();
    }
    public void Add(string key, Item item)
    {
        PU_items.Add(item);
        GameObject obj = Instantiate(pickupPrefab, pickupContent);

        PickupItem pickupItem = obj.GetComponent<PickupItem>();
        pickupItem.Item = item._Item;
        pickupItem.SetPickupItem();

        PU_Dicts.Add(key, obj);


    }
    public void Remove(string key)
    {

        if (PU_Dicts.TryGetValue(key, out GameObject obj))
        {

            PU_items.RemoveAll(i => i.UniqueKey == key);

            Destroy(obj);
            PU_Dicts.Remove(key);
        }


    }


    public Item Pickup()
    {
        Item item = PU_items.FirstOrDefault();

        if (item != null)
        {
            Destroy(PU_Dicts[item.UniqueKey]);
            PU_items.Remove(item);
            PU_Dicts.Remove(item.UniqueKey);

            return item;
        }
        return null;
    }

    
}
