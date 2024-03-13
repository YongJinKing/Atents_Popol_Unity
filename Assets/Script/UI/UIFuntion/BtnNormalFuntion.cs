using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BtnNormalFuntion : MonoBehaviour
{
    public Sprite[] ButtonSprite;
    
    public void ButtonPointerDown(GameObject gameObject)
    {
        gameObject.transform.GetComponent<Image>().sprite = ButtonSprite[1];
        gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -5, 0);
    }
    public void ButtonPointerUp(GameObject gameObject)
    {
        gameObject.transform.GetComponent<Image>().sprite = ButtonSprite[0];
        gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 10, 0);
    }
}
