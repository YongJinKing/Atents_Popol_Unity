using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmithEventManager : MonoBehaviour
{
    private enum SmithPopupType
    {
        None,
        Repair,
        ThrowAway,
    }
    
    public Sprite[] ButtonImgSprite;

   
    public GameObject SmithUI;
    public GameObject SmithInventory;//
    public GameObject SmithModeBtn;
    public GameObject SmithItemAbility;
    public GameObject SmithPairAndAwayBtnManager;
    public GameObject SmithPopupManager;
    public GameObject SmithMainImage;
    public GameObject ShopEventManager;
    public GameObject ShopInven;
    public GameObject DontDestroyManager;
    

    
    private List<InvenSlot> InvenSlotList = new List<InvenSlot>();//
   

    SmithPopupType popupType = SmithPopupType.None;
    int ChooseSlotIndex = 0;
    //private Button moveUI;


    public class InvenSlot
    {
        public GameObject gameObject;
        public bool ChooseSlot;
    }
    

    private void Start() 
    {
     

        #region SmithInventoryInit
        for(int i = 0; i < SmithInventory.GetComponent<Inventory>().slots.Length; i++)
        {
            int index = i;
            InvenSlot temp = new InvenSlot();
            temp.gameObject = SmithInventory.transform.GetChild(1).GetChild(0).GetChild(i).GetChild(1).gameObject;
            temp.ChooseSlot = false;
            InvenSlotList.Add(temp);
            InvenSlotList[i].gameObject.GetComponent<Button>().onClick.AddListener(() => InvenSlotBtnChoise(index));
        }

        

        Button[] SmithPairAndAwayBtnList = SmithPairAndAwayBtnManager.GetComponentsInChildren<UnityEngine.UI.Button>();
        for(int i = 0; i < SmithPairAndAwayBtnManager.transform.childCount; i++)
        {
         
            int index = i;
            SmithPairAndAwayBtnList[i].onClick.AddListener(() => RepairAndAwayPopup(index));
        }

        

        #endregion
         
        #region SmithPopupInit
        Button[] SmithPopupBtnList = SmithPopupManager.GetComponentsInChildren<UnityEngine.UI.Button>();
        for(int i = 0; i < SmithPopupBtnList.Length; i++)
        {
            int index = i;
            SmithPopupBtnList[i].onClick.AddListener(() => PopupUiControll(index));
        }
        #endregion

        #region UIInit
        
        ItemDetailShow(false);
        #endregion
        
        //Debug.Log(SmithInventory.transform.GetChild(1).GetChild(0).GetChild(0).gameObject);
        //Debug.Log(GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).gameObject);
        //Debug.Log(GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject);
        //smith UI / Main Panel / BuyAndsell / itemDetail / ItemAbility / ItemAbility / Image
        
    }
    
    #region SmithSystem
    
    Color AlphaColorChange(float Value, Color Objcolor)
    {
        Color color = Objcolor;
        color.a = Value;
        Objcolor = color;
        return Objcolor;
    }
    public void CleanSlots()
    {
        for(int i = 0; i < SmithInventory.GetComponent<Inventory>().slots.Length; i++)
        {
            InvenSlotList[i].gameObject.GetComponent<Image>().color =
            AlphaColorChange(0.0f, InvenSlotList[i].gameObject.GetComponent<Image>().color);
            InvenSlotList[i].ChooseSlot = false;//
        }
        ItemDetailShow(false);
    }
    void InvenSlotBtnChoise(int index)
    {
        Slot[] SlotList = SmithInventory.GetComponent<Inventory>().slots;
        Image ItemInSlot = SmithInventory.transform.GetChild(1).GetChild(0).GetChild(index).gameObject.GetComponent<Image>();
        if(index >= SmithInventory.transform.GetComponent<Inventory>().TypeCount)
            return;
        if(SlotList[index])
        {
            ItemDetailShow(false);
            if(InvenSlotList[index].ChooseSlot) 
            {
                CleanSlots();
                return;
            }
            else
            {
                CleanSlots();
                InvenSlotList[index].ChooseSlot = true;   
            }
        
            if(InvenSlotList[index].ChooseSlot)
            {
                InvenSlotList[index].gameObject.GetComponent<Image>().color =
                AlphaColorChange(0.3f, InvenSlotList[index].gameObject.GetComponent<Image>().color);
                ItemDetailShow(true);
                ItemDetailChange(SlotList[index], index);
            }
        }
        else return;
    }
   
    void ItemDetailShow(bool isShow)
    {
        GameObject ItemAbility = SmithMainImage.transform.GetChild(0).gameObject;
        GameObject NpcTalkBollum = SmithMainImage.transform.GetChild(1).GetChild(0).gameObject;
        GameObject NpcText = SmithMainImage.transform.GetChild(1).GetChild(1).gameObject;
        ItemAbility.SetActive(isShow);
        NpcTalkBollum.SetActive(isShow);
        NpcText.SetActive(isShow);
    }
    
    void ItemDetailChange(Slot slot, int index)//슬롯의 정보를 받아서
    {

        ChooseSlotIndex = SmithInventory.GetComponent<Inventory>().itemsIndex[index];
        Image ItemAbilityImage = SmithItemAbility.transform.GetChild(0).GetComponent<Image>();
        TMP_Text ItemAbilityText = GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<TMP_Text>();
        Image NpcTalkBollum = GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<Image>();
        TMP_Text NpcText = GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(1).gameObject.GetComponent<TMP_Text>();
        ItemAbilityImage.sprite = SmithInventory.transform.GetChild(1).GetChild(0).GetChild(index).GetChild(0).gameObject.GetComponent<Image>().sprite;
        string EqirType(RiggingType Type)
        {
            string Typename = "";
            if(Type == RiggingType.Weapon)
                Typename = "공격력 : ";
            if(Type == RiggingType.Armor)
                Typename = "체력 : ";
            return Typename;
        }
         ItemAbilityText.text = 
        "이름 : " + slot.InvenDetailName + "\n\n" + EqirType(slot.InvenDetailRiggingType)
         + slot.InvenDetailValue + "\n\n" + "내구도 : " + slot.InvenDetailDurAbility;
        NpcText.text = slot.InvenDetailSmithTalk;
    }
    void RepairAndAwayPopup(int index)
    {
        bool isSelected = false;
        foreach(InvenSlot i in InvenSlotList)
        {
            if(i.ChooseSlot)
            {
                isSelected = true;
                SmithUI.transform.GetChild(1).gameObject.SetActive(true);// 팝업 on
            }
        }

        if(!isSelected)
            {
                return;
            }
        if(index == 0)
        {
            SmithPopupManager.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>().text = 
            "아이템을 수리하시겠습니까?";
            popupType = SmithPopupType.Repair;
        }
        if(index == 1)
        {
            SmithPopupManager.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>().text = 
            "아이템을 폐기하시겠습니까?";
            popupType = SmithPopupType.ThrowAway;
        }
    }
    #endregion 
    
    #region PopupControll
    void PopupUiControll(int index)
    {

        if(index == 0)//yes
        {
            List<bool> SmithModeBtnList = SmithModeBtn.GetComponent<BtnModeFuntion>().BtnModeCheck;
            if(popupType == SmithPopupType.Repair)
            {
                DontDestroyManager.GetComponent<DataManager>().HaveInventory[ChooseSlotIndex].durAbility = 100;
                CleanSlots();
                PopupClose();
                int BoolIndex = SmithModeBtnList.IndexOf(true);
                if(BoolIndex != -1)
                {
                    SmithInventory.GetComponent<Inventory>().FreshSlot(BoolIndex + 1);
                    ShopInven.GetComponent<Inventory>().FreshSlot(BoolIndex + 1);
                }
                else
                {
                    SmithInventory.GetComponent<Inventory>().FreshSlot(0);
                    ShopInven.GetComponent<Inventory>().FreshSlot(0);
                }
            }
            if(popupType == SmithPopupType.ThrowAway)
            {
                DontDestroyManager.GetComponent<DataManager>().HaveInventory.RemoveAt(ChooseSlotIndex);

                CleanSlots();
                PopupClose();
                int BoolIndex = SmithModeBtnList.IndexOf(true);
                if(BoolIndex != -1)
                {
                    SmithInventory.GetComponent<Inventory>().FreshSlot(BoolIndex + 1);
                    ShopInven.GetComponent<Inventory>().FreshSlot(BoolIndex + 1);
                }
                else
                {
                    SmithInventory.GetComponent<Inventory>().FreshSlot(0);
                    ShopInven.GetComponent<Inventory>().FreshSlot(0);
                }
                ShopEventManager.GetComponent<ShopEvenvtManager>().InvenCheck();
            }
        }
        if(index == 1)//no
        {
           
            PopupClose();
        }
    }

    void PopupClose()
    {
        popupType = SmithPopupType.None;
        SmithUI.transform.GetChild(1).gameObject.SetActive(false);// 팝업 off
    }

    #endregion
}


