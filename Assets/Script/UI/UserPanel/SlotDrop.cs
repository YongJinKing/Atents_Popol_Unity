using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.ComponentModel.Design;


public class SlotDrop : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent<string, int> EventDrop;
    public UnityEvent<int, bool> IsMouseInSlot;
    public UnityEvent<int, Image> DropEnd;
    Image TempImage;
    int TempSlotIndex;
    public GameObject DragImage;
    
    
    public void OnBeginDrag(PointerEventData eventData)//Drag Start
    {
        
        Image image = transform.GetComponent<Image>();
        if(image.sprite.name != "Grey")
        {
            TempImage = image;
            
            TempSlotIndex = transform.GetSiblingIndex();
            DragImage.GetComponent<Image>().sprite = image.sprite;
            Color color = DragImage.GetComponent<Image>().color;
            color.a = 1.0f;
            DragImage.GetComponent<Image>().color = color;
            DragImage.GetComponent<Image>().raycastTarget = false;
            image.sprite = Resources.Load<Sprite>("UI/UserSkill/Grey");
        }
        
        
    }

    public void OnDrag(PointerEventData eventData)//Drag Ing
    {
        DragImage.GetComponent<RectTransform>().anchoredPosition = eventData.position;   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsMouseInSlot?.Invoke(transform.GetSiblingIndex(), true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsMouseInSlot?.Invoke(transform.GetSiblingIndex(), false);
    }
    public void OnEndDrag(PointerEventData eventData)//Drag End
    {
        DragImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        Color color = DragImage.GetComponent<Image>().color;
        color.a = 0.0f;
        DragImage.GetComponent<Image>().color = color;
        DragImage.GetComponent<Image>().raycastTarget = true;
        DropEnd?.Invoke(TempSlotIndex, TempImage);
    }

    
    
    public void OnDrop(PointerEventData eventData)
    {
        EventDrop?.Invoke(DragImage.GetComponent<Image>().sprite.name, transform.GetSiblingIndex());       
        //transform.GetComponent<Image>().sprite = DragImage.GetComponent<Image>().sprite;
    }

}
