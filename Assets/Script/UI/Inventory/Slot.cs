using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] 
    public Image image;
    public string InvenDetailName;
    public RiggingType InvenDetailRiggingType;
    public WeaponType InvenDetailWeaponType;
    public int InvenDetailValue;
    public string InvenDetailSmithTalk;
    public int InvenDetailDurAbility;
    public int InvenDetailIndex;
    public string InvenDetailDesc;
    public int InvenDetailSellingPrice;

    private Item _item;
    public Item item 
    {
        get { return _item; }
        set {
            _item = value;
            if (_item != null) 
            {
                image.sprite = item.itemImage;
                InvenDetailName = item.itemName;
                InvenDetailValue = item.itemValue;
                InvenDetailDurAbility = item.durAbility;
                InvenDetailSmithTalk = item.smithTalk;
                InvenDetailDesc = item.itemDesc;
                InvenDetailSellingPrice = item.SellingPrice;
                InvenDetailWeaponType = item.weaponType;
                InvenDetailRiggingType = item.riggingType;

                image.color = new Color(1, 1, 1, 1);
            } else 
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
