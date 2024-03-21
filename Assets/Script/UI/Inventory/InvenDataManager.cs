using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvenDataManager : MonoBehaviour
{
    public static InvenDataManager instance;
    public List<UIItem> ItemList;
    public List<int> BackUpIdList;
    public UnityEvent CleanSlot;
    int InvenMode = 0;
    private void Awake()    //싱글톤
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
    }
    void Start()
    {
        ItemDataManager.GetInstance().InvenItemLoadDatas();
        this.ItemList = new List<UIItem>();
        BackUpIdList = new List<int>();
        FreshSlot(0);
        this.AddItem(1000);
        this.AddItem(1001);
        this.AddItem(1100);
    }
    public void AddItem(int id) 
    {
        for(int i = 0; i < transform.GetChild(0).transform.childCount; i++)
        {
            if(transform.GetChild(0).transform.GetChild(i).GetComponent<UIItem>().id == 0)
            {
                this.ItemList.Add(transform.GetChild(0).transform.GetChild(i).GetComponent<UIItem>());
                transform.GetChild(0).transform.GetChild(i).GetComponent<UIItem>().Init(id);
                break;
            }
        }
    }

    public void DurationCharge(int index)
    {
        
        
        
    }
    public void RemoveItem(int index) 
    { 
        int id;
        id = ItemList[index].id;
        
        for(int i = 0; i < ItemList.Count; i++)
        {
            if(ItemList[i].id == id)
            {
                ItemList.RemoveAt(i);
                break;
            }
        }
        
        for(int i = 0; i < BackUpIdList.Count; i++)
        {
            if(BackUpIdList[i] == id)
            {
                BackUpIdList.RemoveAt(i);
                break;
            }
        }
        ChangeSlot(InvenMode);   
    }
    public void ChangeSlot(int index)
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
        for(int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            transform.GetChild(0).transform.GetChild(i).GetComponent<UIItem>().InitAll();
        }
        this.ItemList = new List<UIItem>();
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
            for(int i = 0; i < ItemList.Count; i++)
                IdList.Add(ItemList[i].id);
        }
        else if(Mode == 1)
        {
            for(int i = 0; i < ItemList.Count; i++)
            {
                BackUpIdList.Add(ItemList[i].id);
                if(ItemList[i].ItemRigging == 0)
                {
                    IdList.Add(ItemList[i].id);
                }
            }
        }
        else if(Mode == 2)
        {
            for(int i = 0; i < ItemList.Count; i++)
            {
                BackUpIdList.Add(ItemList[i].id);
                if(ItemList[i].ItemRigging == 1)
                {
                    IdList.Add(ItemList[i].id);
                }
            }
        }
        for(int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            transform.GetChild(0).transform.GetChild(i).GetComponent<UIItem>().InitAll();
        }
        this.ItemList = new List<UIItem>();
        
        for(int i = 0; i < IdList.Count; i++)
        {
            AddItem(IdList[i]);
            if(Mode == 0)
                BackUpIdList = new List<int>();
        }
        
    }
   
}
