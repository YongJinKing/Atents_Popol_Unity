using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;


public class SlotDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    

    public GameObject DragImage;

    public void OnBeginDrag(PointerEventData eventData)//Drag Start
    {
        Image image = transform.Find("Paper").Find("Image").GetComponent<Image>();
        DragImage.GetComponent<Image>().sprite = image.sprite;
        DragImage.gameObject.SetActive(true);
        DragImage.GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)//Drag Ing
    {
        DragImage.GetComponent<RectTransform>().anchoredPosition = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)//Drag End
    {
        
        DragImage.gameObject.SetActive(false);
        DragImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        DragImage.GetComponent<Image>().raycastTarget = true;
    }
}
