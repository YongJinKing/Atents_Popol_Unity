
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowHL : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetComponent<Animator>().SetBool("OnBool",true);        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetComponent<Animator>().SetBool("OnBool",false);
    }
}
