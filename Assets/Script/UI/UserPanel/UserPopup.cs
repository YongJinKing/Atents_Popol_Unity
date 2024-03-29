using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPopup : MonoBehaviour
{
    public bool LRcheck;
    Vector2 LeftDefalutVector = new Vector2(230, -185);
    public void getLRcheck(bool TorF)
    {
        LRcheck = TorF;
    }
    public void MoveInvenDescPopup(Vector2 SlotVector)
    {
        transform.Find("Inven").Find("DescPopup").GetComponent<RectTransform>().anchorMax =
        new Vector2(0,1);
        transform.Find("Inven").Find("DescPopup").GetComponent<RectTransform>().anchorMin =
        new Vector2(0,1);
        transform.Find("Inven").Find("DescPopup").GetComponent<RectTransform>().anchoredPosition = 
        SlotVector + LeftDefalutVector;   
    }
}
