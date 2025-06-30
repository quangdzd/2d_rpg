using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public struct ItemDrop
{
    [SerializeField] public ScriptableObject scriptableObject;
    public IItem item => scriptableObject as IItem;
    public float drop_rate;
}
public class DropItem : MonoBehaviour
{

    [SerializeField] private AssetReference itemDropPrefab;
    public List<ItemDrop> itemdrops;


    public void DropAt(Vector3 pos)
    {
        foreach (var itemDrop in itemdrops)
        {
            if (Random.Range(0, 101) < itemDrop.drop_rate)
            {
                var hander = Addressables.LoadAssetAsync<GameObject>(itemDropPrefab);
                hander.Completed += (AsyncOperationHandle<GameObject> task) =>
                {
                    GameObject item = Instantiate(task.Result);
                    item.transform.position = pos += (Vector3)Random.insideUnitCircle.normalized*0.2f; 
                };
            }
        }
    }

}
