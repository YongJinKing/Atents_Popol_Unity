using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopEvenvtManager : MonoBehaviour, I_Item
{
   
    public List<Item> SellingItems;
    public GameObject ItemSlot;
    public GameObject Contents;
    public GameObject DontDestroyManager;

    // Start is called before the first frame update
    void Start()
    {
        int ItemCount = 0;
       
        I_Item InterFaceItems = GetComponent<I_Item>();

        foreach (var item in InterFaceItems.armors)
        {
            SellingItems.Add(item);
            Instantiate(ItemSlot, Contents.transform);
            ItemSlotDetail(ItemCount);
            ItemCount++;
        }
        foreach (var item in InterFaceItems.weapons)
        {
            SellingItems.Add(item);
            Instantiate(ItemSlot, Contents.transform);
            ItemSlotDetail(ItemCount);
            ItemCount++;
        }
        InvenCheck();
        Button[] BuySellingItemsBtnList = Contents.GetComponentsInChildren<Button>();
        for(int i = 0; i < BuySellingItemsBtnList.Length; i++)
        {         
            int index = i;
            BuySellingItemsBtnList[i].onClick.AddListener(() => BuySellingItems(index));
        }
       
    }
    void ItemSlotDetail(int index)
    {
        String WeaponTypeName = "";
        String itemValueTypeName = "";
        Contents.transform.GetChild(index).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite
        = SellingItems[index].itemImage;
        Contents.transform.GetChild(index).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text
        = SellingItems[index].itemName;
        Contents.transform.GetChild(index).GetChild(1).GetChild(1).GetComponent<TMP_Text>().text
        = SellingItems[index].SellingPrice.ToString();
        
        if(SellingItems[index].riggingType.Equals(RiggingType.Armor))
            itemValueTypeName = "체력";
        if(SellingItems[index].riggingType.Equals(RiggingType.Weapon))
            itemValueTypeName = "공격력";
         Contents.transform.GetChild(index).GetChild(0).GetChild(2).GetComponent<TMP_Text>().text
        = itemValueTypeName + " : " + SellingItems[index].itemValue.ToString();
        if(SellingItems[index].weaponType.Equals(WeaponType.Leather))
            WeaponTypeName = "가죽";
        else if (SellingItems[index].weaponType.Equals(WeaponType.Solid))
            WeaponTypeName = "경갑";
        else if (SellingItems[index].weaponType.Equals(WeaponType.Plate))
            WeaponTypeName = "판금";
        else if (SellingItems[index].weaponType.Equals(WeaponType.Sword))
            WeaponTypeName = "검";
        else if (SellingItems[index].weaponType.Equals(WeaponType.Club))
            WeaponTypeName = "둔기";
        else if (SellingItems[index].weaponType.Equals(WeaponType.Spear))
            WeaponTypeName = "창";
        Contents.transform.GetChild(index).GetChild(0).GetChild(3).GetComponent<TMP_Text>().text
        = "종류" + " : " + WeaponTypeName;
        
    }
    public void InvenCheck()
    {
        List<Item> items = DontDestroyManager.GetComponent<DataManager>().HaveInventory;
        for(int i = 0; i < SellingItems.Count; i++)
        {
            for(int j = 0; j < items.Count; j++)
            {
                if(SellingItems[i] == items[j])
                {
                    Contents.transform.GetChild(i).GetChild(0).GetChild(5).gameObject.SetActive(true);
                    break;
                }
                Contents.transform.GetChild(i).GetChild(0).GetChild(5).gameObject.SetActive(false);
            }
        }
    }
    void BuySellingItems(int index)
    {
        
    }

   
}
