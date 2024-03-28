using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowHL : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetComponent<Animator>().SetBool("OnBool",true);
        Debug.Log("들어옴?");
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetComponent<Animator>().SetBool("OnBool",false);
    }
}
