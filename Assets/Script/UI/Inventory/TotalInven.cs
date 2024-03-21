using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TotalInven : MonoBehaviour
{
    public GameObject smithUi;
    public List<UIItem> ItemList;
    public UIItem InstItem;
    public int InvenLength = 25;
    public List<int> BackUpIdList;
    public UnityEvent CleanSlot;
    
    int InvenMode = 0;

    void Start()
    {
        ItemDataManager.GetInstance().InvenItemLoadDatas();
        this.ItemList = new List<UIItem>();
        BackUpIdList = new List<int>();
        
        this.AddItem(1000);
        this.AddItem(1001);
        this.AddItem(1100); 
        
      
    }
    public void AddItem(int id) 
    {
        var ItemData = ItemDataManager.GetInstance().dicItemDatas[id];
        InstItem = new UIItem();
        this.ItemList.Add(InstItem);
        ItemList[ItemList.Count - 1].Init(id);
    }
}
