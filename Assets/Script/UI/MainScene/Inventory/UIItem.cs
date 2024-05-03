using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Image icon;
    public TMP_Text txtCount;
    
    public int id;
    public string ItemName;
    public string ItemDesc;
    public string ItemSmith;
    public int ItemRigging;
    public int ItemValue;
    public int ItemDuration;
    public string spName;
    public int WeaponType;
    public int ItemPrice;
    
    
    public void Init(int id)
    {
        this.id = id;
        var ItemData = ItemDataManager.GetInstance().dicItemDatas[id];
        var SpriteData = ItemDataManager.GetInstance().dicResouseTable[ItemData.Inven_spriteName];
        var NameData = ItemDataManager.GetInstance().dicStringTable[ItemData.Inven_itemName];
        var DescData = ItemDataManager.GetInstance().dicStringTable[ItemData.Inven_itemDesc];
        var SmithData = ItemDataManager.GetInstance().dicStringTable[ItemData.Inven_smithTalk];
        
        this.WeaponType = ItemData.Inven_weaponType;
        //0 : 한손 검, 1: 양손 검, 2 : 한손 둔기, 3 : 양손 둔기, 4 : 창, 5 : 단검, 6 : 투창용 창, 10 : 가죽, 11 : 경갑, 12 : 판금
        this.spName = SpriteData.ImageResourceName;
        this.ItemName = NameData.String_Desc;
        this.ItemDesc = DescData.String_Desc;
        this.ItemSmith = SmithData.String_Desc;
        this.ItemRigging = ItemData.Inven_riggingType;
        this.ItemValue = ItemData.Inven_itemValue;
        this.ItemDuration = ItemData.Inven_durAbility;
        this.ItemPrice = ItemData.Inven_ItemPrice;
        Sprite sp = Resources.Load<Sprite>($"UI/UIItem/{spName}");
        this.icon.sprite = sp;
        this.txtCount.text = "1";
        
        ColorChage(1.0f);
    }
    public void InitAll()
    {
        this.id = 0;
        this.ItemName = "";
        this.ItemDesc = "";
        this.ItemSmith = "";
        this.ItemRigging = 0;
        this.ItemValue = 0;
        this.ItemDuration = 0;
        ColorChage(0.0f);
    }
    
    public void Attached(UIItem uIItem)
    {   
        var ItemData = ItemDataManager.GetInstance().dicItemDatas[uIItem.id];
        var SpriteData = ItemDataManager.GetInstance().dicResouseTable[ItemData.Inven_spriteName];
        var NameData = ItemDataManager.GetInstance().dicStringTable[ItemData.Inven_itemName];
        var DescData = ItemDataManager.GetInstance().dicStringTable[ItemData.Inven_itemDesc];
        var SmithData = ItemDataManager.GetInstance().dicStringTable[ItemData.Inven_smithTalk];
        this.id = uIItem.id;
        this.WeaponType = ItemData.Inven_weaponType;
        //0 : 한손 검, 1: 양손 검, 2 : 한손 둔기, 3 : 양손 둔기, 4 : 창, 5 : 단검, 6 : 투창용 창, 10 : 가죽, 11 : 경갑, 12 : 판금
        this.spName = SpriteData.ImageResourceName;
        this.ItemName = NameData.String_Desc;
        this.ItemDesc = DescData.String_Desc;
        this.ItemSmith = SmithData.String_Desc;
        this.ItemRigging = ItemData.Inven_riggingType;
        this.ItemValue = ItemData.Inven_itemValue;
        this.ItemDuration = uIItem.ItemDuration;
        this.ItemPrice = ItemData.Inven_ItemPrice;
        Sprite sp = Resources.Load<Sprite>($"UI/UIItem/{spName}");
        this.icon.sprite = sp;
        this.txtCount.text = "1";
        
        ColorChage(1.0f);
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
