using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public TMP_Text text;


    [HideInInspector] public IItem item;

    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    void Start()
    {

    }
    public void InitialiseItem(IItem item)
    {
        this.item = item;
        image.sprite = item.image;
        RefreshText();
    }
    public IItem GetItem()
    {
        return item;
    }

    public void RefreshText()
    {
        if (count > 1)
        {
            text.text = count.ToString();
        }
        else
        {
            text.text = string.Empty;
        }
    }
    public string GetItemName()
    {
        if (item.itemName != null) return item.itemName;
        return string.Empty;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        transform.localScale = parentAfterDrag.localScale;
    }
}
