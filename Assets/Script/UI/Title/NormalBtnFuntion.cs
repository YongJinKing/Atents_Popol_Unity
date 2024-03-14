using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class NormalBtnFuntion : MonoBehaviour
{
    public UnityEvent<int> BtnAct;
    // Start is called before the first frame update
    void Start()
    {
        Button[] BtnList = transform.GetComponentsInChildren<Button>();
        for(int i = 0; i < transform.childCount; i++)
        {
            int index = i;
            BtnList[i].onClick.AddListener(() => PressedBtn(index));
        }
    }

    void PressedBtn(int index)
    {
        Debug.Log(index);
        BtnAct?.Invoke(index);
    }
    
}
