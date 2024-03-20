using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Image icon;
    public TMP_Text txtCount;
    public Button btn;
    public int id;
    

    public string ItemName;
    public string ItemDesc;
    public int ItemRigging;

    public void Init(int id, Sprite icon, int amount)
    {
        this.id = id;
        this.icon.sprite = icon;
        this.txtCount.text = amount.ToString();
        var ItemData = ItemDataManager.GetInstance().dicItemDatas[id];
        var NameData = ItemDataManager.GetInstance().dicStringTable[ItemData.Inven_itemName];
        var DescData = ItemDataManager.GetInstance().dicStringTable[ItemData.Inven_itemDesc];
        
        this.ItemName = NameData.String_Desc;
        this.ItemDesc = DescData.String_Desc;
        this.ItemRigging = ItemData.Inven_riggingType;
        
        ColorChage(1.0f);
    }
    public void InitAll()
    {
        this.id = 0;
        this.ItemName = "";
        this.ItemDesc = "";
        this.ItemRigging = 0;
        ColorChage(0.0f);
    }
    void ColorChage(float Value)
    {
        Color ImgObj = this.icon.color;
        ImgObj.a = Value;
        this.icon.color = ImgObj;
        Color TxtObj = this.txtCount.color;
        TxtObj.a = Value;
        this.txtCount.color = TxtObj;
    }
}
