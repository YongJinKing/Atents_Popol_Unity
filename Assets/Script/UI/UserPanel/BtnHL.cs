using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnHL : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetComponent<Animator>().SetBool("SelectButton",true);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetComponent<Animator>().SetBool("SelectButton",false);
    }
   
}
