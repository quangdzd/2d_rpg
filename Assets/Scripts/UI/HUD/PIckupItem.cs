using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public IItem Item { get; set; }

    public Image image;
    public TMP_Text itemName;


    public void SetPickupItem()
    {
        image.sprite = Item.image;
        itemName.text = Item.itemName;
    }
    
    

}

