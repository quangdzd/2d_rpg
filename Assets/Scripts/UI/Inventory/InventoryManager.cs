using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryItemPrefab;


    public InventorySlot[] mainInventorySlot;
    public InventorySlot[] consumablesSlot;
    int selectSlot = -1;
    public List<EquipmentSlotPair> equipmentSlotPairs;
    private Dictionary<EEquipment, InventorySlot> equipmentSlotDict;

    private void Awake()
    {
        // Tạo Dictionary từ danh sách equipmentSlotPairs
        equipmentSlotDict = new Dictionary<EEquipment, InventorySlot>();
        foreach (var pair in equipmentSlotPairs)
        {
            if (!equipmentSlotDict.ContainsKey(pair.equipmentType))
                equipmentSlotDict.Add(pair.equipmentType, pair.slot);
        }
    }

    private void Start() {
        ChangeSelectSlot(0);
    }
    private void Update()
    {
        CheckInput();
    }
    public void ChangeSelectSlot(int newvalue)
    {
        if (selectSlot >= 0)
        {
            consumablesSlot[selectSlot].DeSelect();
        }

        consumablesSlot[newvalue].Select();
        selectSlot = newvalue;
    }

    public void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSelectSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSelectSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSelectSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeSelectSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("su dung item");
            InventoryItem item = consumablesSlot[selectSlot].GetComponentInChildren<InventoryItem>();
            UseItem(item, consumablesSlot[selectSlot]);

            
        }
        
    }








    public void AddItem(IItem item)
    {
        if (item is Equipment equipment)
        {
            CheckEquipEquipment(equipment);
        }
        else if (item is Consumable consumable)
        {
            CheckEquipConsumable(consumable);
        }
    }


    public void SpawnNewItem(IItem item, InventorySlot slot)
    {
        GameObject newitem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newitem.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
    public void CheckEquipEquipment(Equipment equipment)
    {
        if (equipmentSlotDict.TryGetValue(equipment.eEquipment, out InventorySlot slot))
        {
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
            if (inventoryItem == null)
            {
                SpawnNewItem(equipment, slot);
                equipment.Use();
                return;
            }
        }
        MainInventoryAddNewItem(equipment);

    }
    public void CheckEquipConsumable(Consumable consumable)
    {
        if (consumable.stackable)
        {
            bool has_existed = CheckConsumableCanStack(consumable);

            if (has_existed)
            {
                return;
            }
        }
        for (int i = 0; i < consumablesSlot.Length; i++)
        {
            if (consumablesSlot[i].GetComponentInChildren<InventoryItem>() == null)
            {
                SpawnNewItem(consumable, consumablesSlot[i]);
                return;
            }
        }
        MainInventoryAddNewItem(consumable);

    }
    public bool CheckConsumableCanStack(Consumable consumable)
    {
        for (int i = 0; i < consumablesSlot.Length; i++)
        {
            InventoryItem item = consumablesSlot[i].GetComponentInChildren<InventoryItem>();
            if (item == null) continue;

            if (item.GetItemName() == consumable.itemName)
            {
                item.count += 1;
                item.RefreshText();
                return true;
            }
        }
        for (int i = 0; i < mainInventorySlot.Length; i++)
        {
            InventoryItem item = mainInventorySlot[i].GetComponentInChildren<InventoryItem>();
            if (item == null) continue;
            if (item.GetItemName() == consumable.itemName)
            {
                item.count += 1;
                item.RefreshText();
                return true;
            }
        }

        return false;
    }
    public void MainInventoryAddNewItem(IItem item)
    {
        for (int i = 0; i < mainInventorySlot.Length; i++)
        {
            InventoryItem inventoryItem = mainInventorySlot[i].GetComponentInChildren<InventoryItem>();
            if (inventoryItem == null)
            {
                SpawnNewItem(item, mainInventorySlot[i]);
                return;
            }
        }

        Debug.LogError("Inventory is full");
    }



    public void UseItem(InventoryItem item , InventorySlot slot)
    {

        IItem iitem = item.GetItem();
        Debug.Log(iitem);
        if (iitem is Equipment equipment)
        {
            CheckUseEquipment(equipment, slot);
            Debug.Log("quip");
        }
        else if (iitem is Consumable consumable)
        {
            CheckUseConsumable(item, slot);
        }
    }

    public void CheckUseEquipment(Equipment equipment , InventorySlot ISlot)
    {
        if (equipmentSlotDict.TryGetValue(equipment.eEquipment, out InventorySlot slot))
        {
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
            if (inventoryItem == null)
            {
                SpawnNewItem(equipment, slot);
                equipment.Use();
                return;
            }
            else
            {
                SwapSlots(ISlot, slot);
            }
        }

    }

    public void SwapSlots(InventorySlot slot1, InventorySlot slot2)
    {
        InventoryItem item1 = slot1.GetComponentInChildren<InventoryItem>();
        InventoryItem item2 = slot2.GetComponentInChildren<InventoryItem>();

        item1.gameObject.transform.SetParent(slot2.transform);
        item2.gameObject.transform.SetParent(slot1.transform);
    }
    public void CheckUseConsumable(InventoryItem inventoryItem, InventorySlot ISlot)
    {
        
        Consumable consumable = inventoryItem.GetItem() as Consumable;
        consumable.Use();

        inventoryItem.count -= 1;
        inventoryItem.RefreshText();
        if (inventoryItem.count <= 0)
        {
            Destroy(inventoryItem.gameObject);
        }


    }

}


[System.Serializable]
public class EquipmentSlotPair
{
    public EEquipment equipmentType;
    public InventorySlot slot;
}
