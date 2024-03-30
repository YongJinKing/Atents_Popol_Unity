using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPopup : MonoBehaviour
{
    bool LRcheck;
    Vector2 LeftDefalutVector = new Vector2(290 , -240);
    Vector2 RightDefalutVector = new Vector2(-310 , -240);
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
}
