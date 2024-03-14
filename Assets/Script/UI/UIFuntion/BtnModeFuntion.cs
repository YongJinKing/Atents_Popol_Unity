using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BtnModeFuntion : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] ButtonSprite;
    public List<bool> BtnModeCheck = new List<bool>();
    public UnityEvent <int> FreshSlot;
    public UnityEvent CleanSlot;
    public GameObject Inven;
    public GameObject SmithEventManager;
    void Start() 
    {
        Button[] ModeChildButton = transform.GetComponentsInChildren<Button>();
        for(int i = 0; i < ModeChildButton.Length; i++)
        {         
            int index = i;
            BtnModeCheck.Add(false);
            ModeChildButton[i].onClick.AddListener(() => PressedBtnMode(index));
        }
    }

    

    void PressedBtnMode(int index)
    {
        if(BtnModeCheck[index])
        {
            BtnModeCheck[index] = false;
            OffModeBtn(index); 
        }
        else
        {
            ClearBtn();
            BtnModeCheck[index] = true;
            OnModeBtn(index);
        }
    }

    void ClearBtn()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            BtnModeCheck[i] = false;
            OffModeBtn(i);    
        }
    }

    void OnModeBtn(int index)
    {
        gameObject.transform.GetChild(index).GetComponent<Image>().sprite = ButtonSprite[1];
        gameObject.transform.GetChild(index).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -5, 0);
        FreshSlot?.Invoke(index+1);
        CleanSlot?.Invoke();
        /* if(Inven)
            Inven.GetComponent<Inventory>().FreshSlot(index+1);
        if(SmithEventManager)
            SmithEventManager.GetComponent<SmithEventManager>().CleanSlots(); */
    }
    void OffModeBtn(int index)
    {
        gameObject.transform.GetChild(index).GetComponent<Image>().sprite = ButtonSprite[0];
        gameObject.transform.GetChild(index).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 10, 0);
        FreshSlot?.Invoke(0);
        CleanSlot?.Invoke();
        /* if(Inven)
            Inven.GetComponent<Inventory>().FreshSlot(0);
        if(SmithEventManager)
            SmithEventManager.GetComponent<SmithEventManager>().CleanSlots(); */

    }
}
