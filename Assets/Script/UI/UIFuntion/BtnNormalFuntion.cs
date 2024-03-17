using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;


public class BtnNormalFuntion : MonoBehaviour
{
    public Sprite[] ButtonSprite;
    public UnityEvent<int> BtnAct;

    void Start()
    {
        Button[] BtnList = transform.GetComponentsInChildren<Button>();
        for(int i = 0; i < BtnList.Length; i++)
        {
            Debug.Log(BtnList[i].name);
        }
        /* for(int i = 0; i < transform.childCount; i++)
        {
            int index = i;
            Debug.Log(BtnList[index].name);
            BtnList[i].onClick.AddListener(() => PressedBtn(index));
        } */
    }
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
    void PressedBtn(int index)
    {
        BtnAct?.Invoke(index);
    }
}
