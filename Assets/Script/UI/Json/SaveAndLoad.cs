using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    
}

[System.Serializable]
public class ItemLoad// 아이템의 아이디와 아이템이 들어가 있는 슬롯의 인덱스를 식별하기 위해 ItemLoad라는 클래스를 만든다.
{
    public int id, amount, slotIndex;
    public ItemLoad(int ID, int AMOUNT, int SLOTINDEX)
    {
        id = ID;
        amount = AMOUNT;
        slotIndex = SLOTINDEX;
    }
}