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
    public int btnChildCount;
    public UnityEvent<int> BtnAct;
    public UnityEvent PointerBtnAct;

    private void OnEnable() 
    {
        Button[] BtnList = transform.GetComponentsInChildren<Button>();
        btnChildCount = BtnList.Length;
        for(int i = 0; i < BtnList.Length; i++)
        {
            int index = i;
            BtnList[i].onClick.AddListener(() => PressedBtn(index));
            
        }    
    }
    public void ButtonPointerDown(GameObject gameObject)
    {
        gameObject.transform.GetComponent<Image>().sprite = ButtonSprite[1];
        gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -5, 0);
        PointerBtnAct?.Invoke();
        
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
