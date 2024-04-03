using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UserPopup : MonoBehaviour
{
    bool LRcheck;
    public GameObject Popup;
    public GameObject DisInven;
    public GameObject InvenManager;
    public GameObject PlayerItem;
    public GameObject PlayerDetailAbility;
    
    int InvenItemId;
    int SlotNum;
    Vector2 LeftDefalutVector = new Vector2(290 , -240);
    Vector2 RightDefalutVector = new Vector2(-310 , -240);

    private void Start() 
    {
        ItemDataManager.GetInstance().InvenItemLoadDatas();
        if(DataManager.instance.playerData.Armor_Id == 0 && DataManager.instance.playerData.Weapon_Id == 0)
        {
            PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().Init(1000);
            PlayerItem.transform.Find("Armor").GetComponent<UIItem>().Init(1001);
            DataManager.instance.playerData.Weapon_Id = 1000;
            DataManager.instance.playerData.Armor_Id = 1001;
        }
        else
        {
            PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().Init(DataManager.instance.playerData.Weapon_Id);
            PlayerItem.transform.Find("Armor").GetComponent<UIItem>().Init(DataManager.instance.playerData.Armor_Id);
        }
        
    }

    public void getLRcheck(bool TorF)
    {
        LRcheck = TorF;
    }
    public void MoveInvenDescPopup(Vector2 SlotVector)
    {
        Vector2 vector2 = new Vector2();
        transform.Find("Inven").Find("DescPopup").GetComponent<RectTransform>().anchorMax =
        new Vector2(0,1);
        transform.Find("Inven").Find("DescPopup").GetComponent<RectTransform>().anchorMin =
        new Vector2(0,1);
        if(!LRcheck)
            vector2 = LeftDefalutVector;
        else
            vector2 = RightDefalutVector;
        transform.Find("Inven").Find("DescPopup").GetComponent<RectTransform>().anchoredPosition = 
        SlotVector + vector2;   
    }
    
    
    public void PopupControll(int index)
    {
        Popup.gameObject.SetActive(true);
        SlotNum = index;
    }

    public void InvenPopupYesOrNo(int index)
    {
        if(index == 0)
        {
            InvenItemId = DisInven.GetComponent<DisplayInven>().items[SlotNum].id;
            var ItemData = ItemDataManager.GetInstance().dicItemDatas[InvenItemId];
            int ItemRigging = ItemData.Inven_riggingType;//0 : weapon, 1 :Armor
            ChangeItem(ItemRigging);


        }
        Popup.gameObject.SetActive(false);
    }
    void ChangeItem(int index)
    {
        
        int PlayerItemId = 0;
        
        if(index == 0)
        {
            PlayerItemId = PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().id;
            PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().Init(InvenItemId);
            DataManager.instance.playerData.Weapon_Id = InvenItemId;
        }
        if(index == 1)
        {
            PlayerItemId = PlayerItem.transform.Find("Armor").GetComponent<UIItem>().id;
            PlayerItem.transform.Find("Armor").GetComponent<UIItem>().Init(InvenItemId);
            DataManager.instance.playerData.Armor_Id = InvenItemId;
            
        }
        InvenManager.transform.Find("GridLine").GetChild(SlotNum).GetComponent<UIItem>().Init(PlayerItemId);
        PlayerAbilityUpdate();
    }
    public void PlayerAbilityUpdate()
    {
        PlayerDetailAbility.transform.Find("Level").GetComponent<TMP_Text>().text
        = "레벨 : " + DataManager.instance.playerData.Character_CurrentLevel.ToString();

        PlayerDetailAbility.transform.Find("Hp").GetComponent<TMP_Text>().text
        = "체력 : " + (DataManager.instance.playerData.Character_Hp + 
        PlayerItem.transform.Find("Armor").GetComponent<UIItem>().ItemValue).ToString();

        PlayerDetailAbility.transform.Find("AttackPower").GetComponent<TMP_Text>().text
        = "공격력 : " + (DataManager.instance.playerData.Character_AttackPower + 
        PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().ItemValue).ToString();

        PlayerDetailAbility.transform.Find("AttackType").GetComponent<TMP_Text>().text 
        = "무기 종류 : \n" + WeaponTypeToString(PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().WeaponType);

        PlayerDetailAbility.transform.Find("ArmorType").GetComponent<TMP_Text>().text 
        = "방어구 종류 : \n" + WeaponTypeToString(PlayerItem.transform.Find("Armor").GetComponent<UIItem>().WeaponType);

        DataManager.instance.playerData.Weapon_Ability = PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().ItemValue;
        DataManager.instance.playerData.Armor_Ability = PlayerItem.transform.Find("Armor").GetComponent<UIItem>().ItemValue;
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
