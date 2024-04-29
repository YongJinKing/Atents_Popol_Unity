using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRiggingType : MonoBehaviour
{
    int SlotNum;
    int InvenItem;
    public UnityEvent PlayerAbilityUpdate;
    void Start()
    {
        ItemDataManager.GetInstance().InvenItemLoadDatas();
        if(DataManager.instance.playerData.Rigging_Armor_Id == 0 && DataManager.instance.playerData.Rigging_Weapon_Id == 0)
        {
            transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().Init(1000);
            transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().Init(1001);
            DataManager.instance.playerData.Rigging_Weapon_Id = 1000;
            DataManager.instance.playerData.Rigging_Armor_Id = 1001;
        }
        else
        {
            transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().Init(DataManager.instance.playerData.Rigging_Weapon_Id);
            transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().Init(DataManager.instance.playerData.Rigging_Armor_Id);
            transform.GetChild(1).GetChild(0).GetComponent<UIItem>().ItemDuration = DataManager.instance.playerData.Rigging_Weapon_Duration;
            transform.GetChild(1).GetChild(1).GetComponent<UIItem>().ItemDuration = DataManager.instance.playerData.Rigging_Armor_Duration;
        }
    }

    public void ReceiveSlotNum(int index)
    {
        SlotNum = index;

    }
    public void TempItemId()
    {
        UIItem InvenItem = transform.GetComponent<Inventory>().ItemList[SlotNum];
        var ItemData = ItemDataManager.GetInstance().dicItemDatas[InvenItem.id];
        int ItemRigging = ItemData.Inven_riggingType;//0 : weapon, 1 :Armor
        ChangeItem(ItemRigging);
    }
    void ChangeItem(int index)
    {
        int PlayerItem = 0;
        int RiggingItemDur = 0;
        int InvenItemDur = 0;
        InvenItem = transform.GetComponent<Inventory>().ItemList[SlotNum].id;
        InvenItemDur = transform.Find("GridLine").GetChild(SlotNum).GetComponent<UIItem>().ItemDuration;
        if(index == 0)
        {
            PlayerItem = transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().id;
            RiggingItemDur = transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().ItemDuration;
            transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().Init(InvenItem);
            DataManager.instance.playerData.Rigging_Weapon_Id = InvenItem;
        }
        if(index == 1)
        {
            PlayerItem = transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().id;
            RiggingItemDur = transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().ItemDuration;
            transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().Init(InvenItem);
            DataManager.instance.playerData.Rigging_Armor_Id = InvenItem;
        }
        transform.Find("GridLine").GetChild(SlotNum).GetComponent<UIItem>().Init(PlayerItem);
        transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().ItemDuration = InvenItemDur;
        transform.Find("GridLine").GetChild(SlotNum).GetComponent<UIItem>().ItemDuration = RiggingItemDur;
        PlayerAbilityUpdate?.Invoke();
    }

    public void SaveRiggingItemData()
    {
        DataManager.instance.playerData.Rigging_Weapon_Ability = 
        transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().ItemValue;
        DataManager.instance.playerData.Rigging_Armor_Ability = 
        transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().ItemValue;

        DataManager.instance.playerData.Rigging_Weapon_Duration = 
        transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().ItemDuration;
        DataManager.instance.playerData.Rigging_Armor_Duration = 
        transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().ItemDuration;

        DataManager.instance.playerData.Rigging_Weapon_Type = 
        transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().WeaponType;
        DataManager.instance.playerData.Rigging_Armor_Type = 
        transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().WeaponType;
    }
}
