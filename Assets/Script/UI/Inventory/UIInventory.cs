using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.EventSystems;
using System.Data.Common;
using UnityEngine.Events;

public class UIInventory : MonoBehaviour
{
    public GameObject smithUi;
    public List<UIItem> items;
    public GameObject GridLine;
    public List<int> BackUpIdList;
    public UnityEvent CleanSlot;
    
    int InvenMode = 0;
    


    void Start()
    {
        ItemDataManager.GetInstance().InvenItemLoadDatas();
        this.items = new List<UIItem>();
        BackUpIdList = new List<int>();
        FreshSlot(0);
        this.AddItem(1000);
        this.AddItem(1001);
        this.AddItem(1100);
        
      
    }

    public void AddItem(int id) 
    {
        for(int i = 0; i < GridLine.transform.childCount; i++)
        {
            if(GridLine.transform.GetChild(i).GetComponent<UIItem>().id == 0)
            {
                this.items.Add(GridLine.transform.GetChild(i).GetComponent<UIItem>());
                GridLine.transform.GetChild(i).GetComponent<UIItem>().Init(id);
                break;
            }
        }
    }
    
    public void RemoveItem(int index) 
    { 
        int id;
        id = items[index].id;
        items.RemoveAt(index);
        for(int i = 0; i < BackUpIdList.Count; i++)
        {
            if(BackUpIdList[i] == i)
            {
                BackUpIdList.RemoveAt(i);
                break;
            }
        }
        ChangeMode(InvenMode);   
    }

    public void ChangeMode(int index)
    {
        InvenMode = index;
        CleanSlot?.Invoke();
        if(BackUpIdList.Count > 0)
            BackUpItem();
        else
            FreshSlot(InvenMode);
    }

    void BackUpItem()
    {
        for(int i = 0; i < GridLine.transform.childCount; i++)
        {
            GridLine.transform.GetChild(i).GetComponent<UIItem>().InitAll();
        }
        this.items = new List<UIItem>();
        for(int i = 0; i < BackUpIdList.Count; i++)
        {
            AddItem(BackUpIdList[i]);
        }
        this.BackUpIdList = new List<int>();
        FreshSlot(InvenMode);
    }

    public void FreshSlot(int Mode)
    {
        List<int> IdList = new List<int>();
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

