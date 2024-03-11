using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Item
{
    public Item[] armors
    {
        get => Resources.LoadAll<Item>("UI/ItemModel/Armor");
        
    }

    public Item[] weapons
    {
        get => Resources.LoadAll<Item>("UI/ItemModel/WeaPon");
    }
}

public class ItemList : MonoBehaviour, I_Item
{
    // Start is called before the first frame update
    void Start()
    {
        I_Item itemss = GetComponent<I_Item>();

        foreach (var item in itemss.armors)
        {
            Debug.Log($"armors : {item.name}");
        }

        foreach (var item in itemss.weapons)
        {
            Debug.Log($"weapons : {item.name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
