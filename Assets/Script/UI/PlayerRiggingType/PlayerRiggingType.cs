using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRiggingType : MonoBehaviour
{
    int SlotNum;
    int InvenItemId;
    public UnityEvent PlayerAbilityUpdate;
    void Start()
    {
        ItemDataManager.GetInstance().InvenItemLoadDatas();
        if(DataManager.instance.playerData.Armor_Id == 0 && DataManager.instance.playerData.Weapon_Id == 0)
        {
            transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().Init(1000);
            transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().Init(1001);
            DataManager.instance.playerData.Weapon_Id = 1000;
            DataManager.instance.playerData.Armor_Id = 1001;
        }
        else
        {
            transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().Init(DataManager.instance.playerData.Weapon_Id);
            transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().Init(DataManager.instance.playerData.Armor_Id);
        }
    }

    public void ReceiveSlotNum(int index)
    {
        SlotNum = index;

    }
    public void TempItemId()
    {
        InvenItemId = transform.GetComponent<Inventory>().ItemList[SlotNum].id;
        var ItemData = ItemDataManager.GetInstance().dicItemDatas[InvenItemId];
        int ItemRigging = ItemData.Inven_riggingType;//0 : weapon, 1 :Armor
        ChangeItem(ItemRigging);
    }
    void ChangeItem(int index)
    {
        int PlayerItemId = 0;
        
        if(index == 0)
        {
            PlayerItemId = transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().id;
            transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().Init(InvenItemId);
            DataManager.instance.playerData.Weapon_Id = InvenItemId;
        }
        if(index == 1)
        {
            PlayerItemId = transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().id;
            transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().Init(InvenItemId);
            DataManager.instance.playerData.Armor_Id = InvenItemId;
        }
        transform.Find("GridLine").GetChild(SlotNum).GetComponent<UIItem>().Init(PlayerItemId);
        PlayerAbilityUpdate?.Invoke();
        
    }
}
