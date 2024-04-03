using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSlot : MonoBehaviour
{
    public int SibilingRtn()
    {
        return transform.GetSiblingIndex();
    }
}
