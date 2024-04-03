using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUi : MonoBehaviour
{
    public GameObject Contents;
    string ItemRiggingStr = "";
    int InstanceCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        ItemDataManager.GetInstance().InvenItemLoadDatas();
        
        foreach(KeyValuePair<int, ItemData> item in ItemDataManager.GetInstance().dicItemDatas)
        {   
            bool Found = ItemDataManager.GetInstance().dicResouseTable[item.Key].ImageResourceName.Contains("Begginer");
            
            if(!Found)
            {
                Instantiate(Resources.Load("UI/ShopUi/ItemSlot"),Contents.transform);
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().Init(item.Key);
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").Find("ItemName").GetComponent<TMP_Text>().text =
                "이름 :\n"+
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().ItemName;
                if(item.Value.Inven_riggingType == 0)
                    ItemRiggingStr = "공격력 : ";
                if(item.Value.Inven_riggingType == 1)
                    ItemRiggingStr = "체력 : ";
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").Find("ItemValue").GetComponent<TMP_Text>().text =
                ItemRiggingStr +
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().ItemValue.ToString();
            
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").Find("ItemType").GetComponent<TMP_Text>().text =
                "종류 :" +
                WeaponTypeToString(Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().WeaponType);
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").Find("Gold").Find("Text (TMP)").GetComponent<TMP_Text>().text =
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().ItemPrice.ToString();

                
                InstanceCount++;
            }
            
        }
    }

    public void PressedBuyBtn()
    {

    }
    public string WeaponTypeToString(int index)
    {
        string Rtstring = "";
        //0 : 한손 검, 1: 양손 검, 2 : 한손 둔기, 3 : 양손 둔기, 4 : 창, 5 : 단검, 6 : 투창용 창, 10 : 가죽, 11 : 경갑, 12 : 판금
        if(index == 0)
            Rtstring = "한손 검";
        if(index == 1)
            Rtstring = "양손 검";
        if(index == 2)
            Rtstring = "한손 둔기";
        if(index == 3)
            Rtstring = "양손 둔기";
        if(index == 4)
            Rtstring = "창";
        if(index == 5)
            Rtstring = "단검";
        if(index == 6)
            Rtstring = "투창용 창";
        if(index == 10)
            Rtstring = "가죽";
        if(index == 11)
            Rtstring = "경갑";
        if(index == 12)
            Rtstring = "판금";
        return Rtstring;

    }
}
