using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public enum RiggingType
{
    Weapon,
    Armor
}

public enum WeaponType
{
    Sword = 1,
    Club,
    Spear,
    Leather = 11,
    Solid,
    Plate,
}
[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemValue;
    public string itemDesc;
    public string smithTalk;
    public int durAbility;
    public WeaponType weaponType;
    public RiggingType riggingType;
    
}


