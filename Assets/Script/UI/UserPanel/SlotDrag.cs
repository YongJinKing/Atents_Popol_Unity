using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;
using UnityEngine.Events;
using Unity.VisualScripting;


public class SlotDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    

    GameObject DragImage;
    private void Start() 
    {
        DragImage = GameObject.Find("DragImage");
    }

    public void OnBeginDrag(PointerEventData eventData)//Drag Start
    {

        
        Image image = transform.Find("Paper").Find("Image").GetComponent<Image>();
        DragImage.GetComponent<Image>().sprite = image.sprite;
        Color color = DragImage.GetComponent<Image>().color;
        color.a = 1.0f;
        DragImage.GetComponent<Image>().color = color;
        DragImage.GetComponent<Image>().raycastTarget = false;
        
    }
    public void OnDrag(PointerEventData eventData)//Drag Ing
    {
        DragImage.GetComponent<RectTransform>().anchoredPosition = eventData.position;
        
    }
    public void OnEndDrag(PointerEventData eventData)//Drag End
    {
        DragImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        Color color = DragImage.GetComponent<Image>().color;
        color.a = 0.0f;
        DragImage.GetComponent<Image>().color = color;
        DragImage.GetComponent<Image>().raycastTarget = true;
    }
}
