using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour
{

    [SerializeField] private ScriptableObject item;

    public IItem _Item => item as IItem;

    public string UniqueKey => GetInstanceID().ToString(); 

    public int size = 25;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        spriteRenderer.sprite = _Item.image;
        ScaleObjBySize();


    }
    
    public void ScaleObjBySize()
    {
        float imgW = _Item.image.rect.width;
        float imgH = _Item.image.rect.height;
        float scaleFactor = size / imgW;
        gameObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
        spriteRenderer.sprite.texture.filterMode = FilterMode.Point;

        boxCollider2D.size = spriteRenderer.sprite.bounds.size *2 ;
        boxCollider2D.offset = spriteRenderer.sprite.bounds.center;
        boxCollider2D.isTrigger = true;

    }

    public BoxCollider2D Collider()
    {
        return boxCollider2D;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HUD.Instance.PickupManager_Add(UniqueKey , this);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HUD.Instance.PickupManager_Remove(UniqueKey);
        }
    }   






}
