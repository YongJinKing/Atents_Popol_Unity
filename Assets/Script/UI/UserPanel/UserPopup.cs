using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPopup : MonoBehaviour
{
    bool LRcheck;
    public GameObject Popup;
    public GameObject DisInven;
    public GameObject InvenManager;
    public GameObject PlayerItem;
    int InvenItemId;
    int SlotNum;
    Vector2 LeftDefalutVector = new Vector2(290 , -240);
    Vector2 RightDefalutVector = new Vector2(-310 , -240);

    private void Start() 
    {
        ItemDataManager.GetInstance().InvenItemLoadDatas();
        PlayerItem.transform.Find("Weapon").GetComponent<UIItem>().Init(1000);
        PlayerItem.transform.Find("Armor").GetComponent<UIItem>().Init(1001);
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
        }
        if(index == 1)
        {
            PlayerItemId = PlayerItem.transform.Find("Armor").GetComponent<UIItem>().id;
            PlayerItem.transform.Find("Armor").GetComponent<UIItem>().Init(InvenItemId);
        }
        InvenManager.transform.Find("GridLine").GetChild(SlotNum).GetComponent<UIItem>().Init(PlayerItemId);
            
        
    }
}
