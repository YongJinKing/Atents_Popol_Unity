using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class BtnModeFuntion : MonoBehaviour
{
    public Sprite[] BtnImage;
    public GameObject BtnParents;
    public UnityEvent<int> ModeAction;
    int PrevIndex = -1;
    Button[] BtnFunctionList;
    private void Start() 
    {
        BtnFunctionList = BtnParents.GetComponentsInChildren<Button>();
        for(int i = 0; i < BtnFunctionList.Length; i++)
        {
            int index = i;
            BtnFunctionList[i].onClick.AddListener(() => ChooseBtn(index));
        }
    }
    public void ChooseBtn(int index)
    {
        if(PrevIndex == index)
        {
            CleanBtn(-1);
            ModeAction?.Invoke(0);
            return;
        }
        else
        {
            CleanBtn(index);
            BtnFunctionList[index].GetComponent<Image>().sprite = BtnImage[1];
            BtnFunctionList[index].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -5, 0);
            ModeAction?.Invoke(index + 1);
        }
    }
    void CleanBtn(int PrevIdxSetting)
    {
        
        for(int i = 0; i < BtnParents.transform.childCount; i++)
        {
            BtnFunctionList[i].GetComponent<Image>().sprite = BtnImage[0];
            BtnFunctionList[i].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 10, 0);
        }
        PrevIndex = PrevIdxSetting;
    }
    public void CleanBtn()
    {
        for(int i = 0; i < BtnParents.transform.childCount; i++)
        {
            BtnFunctionList[i].GetComponent<Image>().sprite = BtnImage[0];
            BtnFunctionList[i].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 10, 0);
        }
    }

}
