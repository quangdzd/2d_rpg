using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectColor, notSelectColor;

    public void Select()
    {
        Color color = selectColor;
        color.a = 40f / 255f;
        image.color = color;
        
    }

    public void DeSelect()
    {
        Color color = selectColor;
        color.a = 0f / 255f;
        image.color = color;
    }
    void Awake()
    {
        DeSelect();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
