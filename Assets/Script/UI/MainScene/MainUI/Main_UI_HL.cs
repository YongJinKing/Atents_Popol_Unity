using UnityEngine;
using UnityEngine.EventSystems;


public class Main_UI_HL : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(transform.Find("Logo1"))
            transform.Find("Logo1").gameObject.GetComponent<Animator>().SetBool("SelectButton",true);
        if(transform.Find("Logo2"))
            transform.Find("Logo2").gameObject.GetComponent<Animator>().SetBool("SelectButton",true);
        SoundManager.instance.PlaySfxMusic("Equip");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(transform.Find("Logo1"))
            transform.Find("Logo1").gameObject.GetComponent<Animator>().SetBool("SelectButton",false);
        if(transform.Find("Logo2"))
            transform.Find("Logo2").gameObject.GetComponent<Animator>().SetBool("SelectButton",false);
    }
   
}
