
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;



public class SlotHL : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    public UnityEvent<int, bool> OnHighLite;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("들어옴?");
        OnHighLite?.Invoke(transform.GetSiblingIndex(),true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {   
        OnHighLite?.Invoke(transform.GetSiblingIndex(),false);
    }
}
