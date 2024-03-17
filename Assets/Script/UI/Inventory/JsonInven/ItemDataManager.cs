using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager 
{
    private static ItemDataManager instance;
    public Dictionary<int, ItemData> dicItemDatas;

    private ItemDataManager()
    {
        
    }
}
