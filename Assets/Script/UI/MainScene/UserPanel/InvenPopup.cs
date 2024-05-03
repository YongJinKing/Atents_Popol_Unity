using System.Collections;

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
    public GameObject SkillPopup;
    


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
        = ItemTypeIntToString.IntToStringUIDesc(go.WeaponType);
        Inven.transform.Find("DescPopup").Find("Paper").Find("ItemValue").GetComponent<TMP_Text>().text
        = ItemTypeIntToString.IntToStringRiggingType(go.ItemRigging) + go.ItemValue.ToString();
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
            SkillPopup.transform.GetComponent<SkillPopup>().WeaponChange();
        }
        RiggingPopup.gameObject.SetActive(false);
    }
    
    public void PlayerAbilityUpdate()
    {   
        var inst = DataManager.instance.playerData;
        PlayerDataManager.instance.LoadPlayerData();
        PlayerItem.transform.Find("Armor").GetComponent<UIItem>()
        .Init(InvenManager.transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().id);

        PlayerItem.transform.Find("Weapon").GetComponent<UIItem>()
        .Init(InvenManager.transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().id);

        PlayerDetailAbility.transform.Find("Level").GetComponent<TMP_Text>().text
        = "레벨 : " + inst.Character_CurrentLevel.ToString();

        PlayerDetailAbility.transform.Find("Hp").GetComponent<TMP_Text>().text
        = "체력 : " + (inst.Character_Hp + 
        InvenManager.transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().ItemValue).ToString();

        PlayerDetailAbility.transform.Find("AttackPower").GetComponent<TMP_Text>().text
        = "공격력 : " + (inst.Character_AttackPower + 
        InvenManager.transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().ItemValue).ToString();

        PlayerDetailAbility.transform.Find("AttackType").GetComponent<TMP_Text>().text 
        = "무기 종류 : \n" + ItemTypeIntToString.IntToStringUIDesc(PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().WeaponType);

        PlayerDetailAbility.transform.Find("ArmorType").GetComponent<TMP_Text>().text 
        = "방어구 종류 : \n" + ItemTypeIntToString.IntToStringUIDesc(PlayerItem.transform.Find("Armor").GetComponent<UIItem>().WeaponType);
        
        

        var CurrentData = PlayerDataManager.instance.dicPlayerLevelData[inst.Character_CurrentLevel];
        var NextData = PlayerDataManager.instance.dicPlayerLevelData[inst.Character_CurrentLevel + 1];
        int CurrnetExp = inst.Character_CurrentExp - CurrentData.Total_Exp;
        PlayerDetailAbility.transform.GetChild(6).GetChild(0).GetComponent<Image>().fillAmount =
        (float)CurrnetExp / (float)NextData.Exp;
        PlayerDetailAbility.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text
        = $"Exp : {Mathf.Floor(((float)CurrnetExp / (float)NextData.Exp) * 100.0f)}%";

        inst.Rigging_Weapon_Ability = PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().ItemValue;
        inst.Rigging_Armor_Ability = PlayerItem.transform.Find("Armor").GetComponent<UIItem>().ItemValue;
        inst.Rigging_Weapon_Type = PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().WeaponType;
    }

    
    
    
}
