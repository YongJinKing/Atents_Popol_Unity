using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    
    public List<UIItem> ItemList;
    public List<int> BackUpIdList;
    public List<int> BackUpDurList;
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
   
    public void testtest()
    {
        transform.GetChild(0).GetChild(1).GetComponent<UIItem>().ItemDuration -= 10;
    }
    
    void Start()
    {
        Debug.Log("startCheckInInventory");
        
        ItemDataManager.GetInstance().InvenItemLoadDatas();
        this.ItemList = new List<UIItem>();
        BackUpIdList = new List<int>();
        FreshSlot(0);
        for(int i = 0; i < DataManager.instance.playerData.PlayerInven.Count; i++)
        {
            AddItem(DataManager.instance.playerData.PlayerInven[i]);
            ItemList[i].ItemDuration = DataManager.instance.playerData.PlayerInvenDuraion[i];
        }
        
        Debug.Log($"Armor Duration : {DataManager.instance.playerData.Armor_Duration}, {transform.GetChild(1).GetChild(1).GetComponent<UIItem>().ItemDuration}");

        
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
    public void InvenItemRepair(int index)
    {
        transform.GetChild(0).GetChild(index).GetComponent<UIItem>().ItemDuration = 100;
    }

    public void RiggingItemRepair(int index)
    {
        transform.GetChild(1).GetChild(index).GetComponent<UIItem>().ItemDuration = 100;
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
                BackUpDurList.RemoveAt(i);
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
            ItemList[i].ItemDuration = BackUpDurList[i];
        }
        this.BackUpIdList = new List<int>();
        this.BackUpDurList = new List<int>();
        FreshSlot(InvenMode);
    }
    public void FreshSlot(int Mode)
    {
        List<int> IdList = new List<int>();
        List<int> DurationList = new List<int>();
        if(Mode == 0)
        {
            for(int i = 0; i < ItemList.Count; i++)
            {
                IdList.Add(ItemList[i].id);
                DurationList.Add(ItemList[i].ItemDuration);
            }
                
        }
        else if(Mode == 1)
        {
            for(int i = 0; i < ItemList.Count; i++)
            {
                BackUpIdList.Add(ItemList[i].id);
                BackUpDurList.Add(ItemList[i].ItemDuration);
                if(ItemList[i].ItemRigging == 0)
                {
                    IdList.Add(ItemList[i].id);
                    DurationList.Add(ItemList[i].ItemDuration);
                }
            }
        }
        else if(Mode == 2)
        {
            for(int i = 0; i < ItemList.Count; i++)
            {
                BackUpIdList.Add(ItemList[i].id);
                BackUpDurList.Add(ItemList[i].ItemDuration);
                if(ItemList[i].ItemRigging == 1)
                {
                    IdList.Add(ItemList[i].id);
                    DurationList.Add(ItemList[i].ItemDuration);
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
            ItemList[i].ItemDuration = DurationList[i];
            if(Mode == 0)
            {
                BackUpIdList = new List<int>();
                BackUpDurList = new List<int>();
            }
                
        }
           
    }
    public void SaveInvenData()
    {
        DataManager.instance.playerData.PlayerInven = new List<int>();
        DataManager.instance.playerData.PlayerInvenDuraion = new List<int>(); 
        
        for(int i = 0; i < ItemList.Count; i++)
        {
            DataManager.instance.playerData.PlayerInven.Add(ItemList[i].id);
            DataManager.instance.playerData.PlayerInvenDuraion.Add(ItemList[i].ItemDuration);
        }
        DataManager.instance.playerData.Weapon_Duration = transform.GetChild(1).GetChild(0).GetComponent<UIItem>().ItemDuration;
        DataManager.instance.playerData.Armor_Duration = transform.GetChild(1).GetChild(1).GetComponent<UIItem>().ItemDuration;
    }
    
   
}
