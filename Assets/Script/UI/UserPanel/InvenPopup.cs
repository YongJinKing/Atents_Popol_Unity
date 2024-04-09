using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InvenPopup : MonoBehaviour
{

    public GameObject UserGridLine;
    public GameObject PlayerItem;
    public GameObject DisInven;
    public GameObject Inven;
    


    public GameObject RiggingPopup;
    public GameObject InvenManager;
    public GameObject PlayerDetailAbility;
    
    public UnityEvent EventChangeItem;
    public UnityEvent<int> EventSlotNum;
    



    Coroutine CorRiggingItem;
    bool LRcheck;
    int InvenItemId;
    int SlotNum;
    Vector2 SlotVector2;
    Vector2 LeftDefalutVector = new Vector2(290 , -240);
    Vector2 RightDefalutVector = new Vector2(-310 , -240);

    
    public void OnRiggingItemHighLight(int index, bool OnCheck)
    {
        if(OnCheck)
        {
            PlayerItem.transform.GetChild(index).Find("Button").gameObject.SetActive(OnCheck);
            SlotVector2 = PlayerItem.transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition;
            StartCoroutine(OnPopupControll(1, index));
        }
        else
        {
            StopAllCoroutines();
            PlayerItem.transform.GetChild(0).gameObject.SetActive(true);
            Inven.transform.Find("DescPopup").gameObject.SetActive(false);
            PlayerItem.transform.GetChild(index).Find("Button").gameObject.SetActive(OnCheck);
        }
        
    }
    public void OnSlotHighLight(int index, bool OnCheck)
    {
        if(OnCheck)
        {
            if(DisInven.GetComponent<DisplayInven>().items.Count > index)
            {
                UserGridLine.transform.GetChild(index).Find("Button").gameObject.SetActive(OnCheck);
                if(UserGridLine.transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition.x >= 300)
                    LRcheck = true;
                else
                    LRcheck = false;
                SlotNum = index;
                SlotVector2 = UserGridLine.transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition;
                StartCoroutine(OnPopupControll(0, index));
                StartCoroutine(OnMouseControll());

            } 
        }
        else
        {
            if(Inven.transform.Find("PlayerAbility").Find("Inventory").GetComponent<DisplayInven>().items.Count > index)
            {
                StopAllCoroutines();
                Inven.transform.Find("DescPopup").gameObject.SetActive(false);
                UserGridLine.transform.GetChild(index).Find("Button").gameObject.SetActive(OnCheck);
            }
        }
          
    }
     IEnumerator OnMouseControll()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(1))
            {
                PopupControll(SlotNum);
            }
            yield return null;
        }   
    }
    
    IEnumerator OnPopupControll(int type, int index)//0 : gridline 1 : 
    {
        yield return new WaitForSeconds(0.8f);
        UIItem go = gameObject.AddComponent<UIItem>();
        if(type == 0)
        {
            go = UserGridLine.transform.GetChild(index).GetComponent<UIItem>();
        }
        if(type == 1)
        {
            go = PlayerItem.transform.GetChild(index).GetComponent<UIItem>();
            PlayerItem.transform.GetChild(0).gameObject.SetActive(false);
        }
        Inven.transform.Find("DescPopup").gameObject.SetActive(true);
        Inven.transform.Find("DescPopup").Find("Paper").Find("ImgBg").Find("ItemImage").GetComponent<Image>().sprite
        = go.icon.sprite;
        Inven.transform.Find("DescPopup").Find("Paper").Find("ItemName").GetComponent<TMP_Text>().text
        = go.ItemName;
        Inven.transform.Find("DescPopup").Find("Paper").Find("ItemType").GetComponent<TMP_Text>().text
        = WeaponTypeToString(go.WeaponType);
        Inven.transform.Find("DescPopup").Find("Paper").Find("ItemValue").GetComponent<TMP_Text>().text
        = RiggingTypeToString(go.ItemRigging) + go.ItemValue.ToString();
        Inven.transform.Find("DescPopup").Find("Paper").Find("ItemDesc").GetComponent<TMP_Text>().text
        = go.ItemDesc;
        Inven.transform.Find("DescPopup").Find("Paper").Find("DurationDesc").Find("Value").GetComponent<TMP_Text>().text
        = go.ItemDuration.ToString();

        MoveInvenDescPopup(type);
    }
   
    public void MoveInvenDescPopup(int type)
    {
        Vector2 vector2 = new Vector2();
        if(type == 0)
        {
            if(!LRcheck)
                vector2 = LeftDefalutVector;
            else
                vector2 = RightDefalutVector;
        }
        if(type == 1)
        {
            vector2 = new Vector2(1515-755, -655);
        }
        transform.Find("DescPopup").GetComponent<RectTransform>().anchoredPosition = 
        SlotVector2 + vector2;   
    }
    
    
    public void PopupControll(int index)
    {
        RiggingPopup.gameObject.SetActive(true);
        EventSlotNum?.Invoke(index);
    }

    public void InvenPopupYesOrNo(int index)
    {
        if(index == 0)
        {
            
            EventChangeItem?.Invoke();
            PlayerAbilityUpdate();
        }
        RiggingPopup.gameObject.SetActive(false);
    }
    
    public void PlayerAbilityUpdate()
    {   
        PlayerItem.transform.Find("Armor").GetComponent<UIItem>()
        .Init(InvenManager.transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().id);

        PlayerItem.transform.Find("Weapon").GetComponent<UIItem>()
        .Init(InvenManager.transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().id);

        PlayerDetailAbility.transform.Find("Level").GetComponent<TMP_Text>().text
        = "레벨 : " + DataManager.instance.playerData.Character_CurrentLevel.ToString();

        PlayerDetailAbility.transform.Find("Hp").GetComponent<TMP_Text>().text
        = "체력 : " + (DataManager.instance.playerData.Character_Hp + 
        InvenManager.transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().ItemValue).ToString();

        PlayerDetailAbility.transform.Find("AttackPower").GetComponent<TMP_Text>().text
        = "공격력 : " + (DataManager.instance.playerData.Character_AttackPower + 
        InvenManager.transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().ItemValue).ToString();

        PlayerDetailAbility.transform.Find("AttackType").GetComponent<TMP_Text>().text 
        = "무기 종류 : \n" + WeaponTypeToString(PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().WeaponType);

        PlayerDetailAbility.transform.Find("ArmorType").GetComponent<TMP_Text>().text 
        = "방어구 종류 : \n" + WeaponTypeToString(PlayerItem.transform.Find("Armor").GetComponent<UIItem>().WeaponType);

        DataManager.instance.playerData.Weapon_Ability = PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().ItemValue;
        DataManager.instance.playerData.Armor_Ability = PlayerItem.transform.Find("Armor").GetComponent<UIItem>().ItemValue;
        DataManager.instance.playerData.WeaponType = PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().WeaponType;
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
    public string RiggingTypeToString(int index)
    {
        string Rtstring = "";
        //0 : 무기 1 : 방어구
        if(index == 0)
            Rtstring = "공격력 : ";
        if(index == 1)
            Rtstring = "체력 : ";
       
        return Rtstring;
    }
    
}
