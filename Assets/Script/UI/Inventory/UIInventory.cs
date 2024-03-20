using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.EventSystems;
using System.Data.Common;

public class UIInventory : MonoBehaviour
{
    public GameObject smithUi;
    public List<UIItem> items;
    public GameObject GridLine;
    public List<int> BackUpIdList;
    private GameObject prefab;
    int Slotindex;
    int InvenMode = 0;


    void Start()
    {
        ItemDataManager.GetInstance().InvenItemLoadDatas();
        this.prefab = Resources.Load<GameObject>("UI/UIItem/Empty");
        this.items = new List<UIItem>();
        BackUpIdList = new List<int>();
        this.AddItem(1000);
        this.AddItem(1001);
        this.AddItem(1100);
        
      
    }

    public void AddItem(int id) 
    {
        
        var ItemData = ItemDataManager.GetInstance().dicItemDatas[id];
        var SpriteData = ItemDataManager.GetInstance().dicResouseTable[ItemData.Inven_spriteName];
        
        string spName = SpriteData.ImageResourceName;
        
        
        Sprite sp = Resources.Load<Sprite>($"UI/UIItem/{spName}");
        
        for(int i = 0; i < GridLine.transform.childCount; i++)
        {
            if(GridLine.transform.GetChild(i).GetComponent<UIItem>().id == 0)
            {
                Slotindex = i;
                this.items.Add(GridLine.transform.GetChild(Slotindex).GetComponent<UIItem>());
                GridLine.transform.GetChild(Slotindex).GetComponent<UIItem>().Init(id, sp, 1);
                break;
            }
        }
    }
    
    public void RemoveItem(int id) 
    { 
        var data = ItemDataManager.GetInstance().dicItemDatas[id];
        
        for(int i = 0; i < GridLine.transform.childCount; i++)
        {
            if(GridLine.transform.GetChild(i).GetComponent<UIItem>().id == id)
            {
                Slotindex = i;
                this.items.RemoveAt(Slotindex);
                FreshSlot(InvenMode);
                break; 
            }
        }
    }

    public void ChangeSlot(int index)
    {
        InvenMode = index;
        FreshSlot(InvenMode);
    }
    public void FreshSlot(int Mode)
    {
        List<int> IdList = BackUpIdList;
        if(Mode == 0)
        {
            for(int i = 0; i < items.Count; i++)
                IdList.Add(items[i].id);
        }
        else if(Mode == 1)
        {
            for(int i = 0; i < items.Count; i++)
            {
                BackUpIdList.Add(items[i].id);
                if(items[i].ItemRigging == 0)
                {
                    IdList.Add(items[i].id);
                }
            }
        }
        else if(Mode == 2)
        {
            for(int i = 0; i < items.Count; i++)
            {
                BackUpIdList.Add(items[i].id);
                if(items[i].ItemRigging == 1)
                {
                    IdList.Add(items[i].id);
                }
            }
        }
        
        for(int i = 0; i < GridLine.transform.childCount; i++)
        {
            GridLine.transform.GetChild(i).GetComponent<UIItem>().InitAll();
        }
        this.items = new List<UIItem>();
        
        for(int i = 0; i < IdList.Count; i++)
        {
            AddItem(IdList[i]);
            if(Mode == 0)
                BackUpIdList = new List<int>();
        }
        
        
        
    }

    

}

