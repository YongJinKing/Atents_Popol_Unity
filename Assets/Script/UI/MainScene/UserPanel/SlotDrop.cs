

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;



public class SlotDrop : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent<string, int> EventDrop;
    public UnityEvent<int, bool> IsMouseInSlot;
    public UnityEvent<int, Sprite> DropEnd;
    Sprite TempSprite;
    bool DragStart = false;
    int TempSlotIndex;
    public GameObject DragImage;
    
    
    public void OnBeginDrag(PointerEventData eventData)//Drag Start
    {
        
        Image image = transform.GetComponent<Image>();
        if(image.sprite.name != "Grey")
        {
            
            TempSprite = image.sprite;
            TempSlotIndex = transform.GetSiblingIndex();
            DragImage.GetComponent<Image>().sprite = image.sprite;
            Color color = DragImage.GetComponent<Image>().color;
            color.a = 1.0f;
            DragImage.GetComponent<Image>().color = color;
            DragImage.GetComponent<Image>().raycastTarget = false;
            image.sprite = Resources.Load<Sprite>("UI/UserSkill/Grey");
            DragStart = true;
            
        }
        else
            DragStart = false;
        
        
        
    }

    public void OnDrag(PointerEventData eventData)//Drag Ing
    {
        if(DragStart)
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
        if(DragStart)
        {
            DragImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            Color color = DragImage.GetComponent<Image>().color;
            color.a = 0.0f;
            DragImage.GetComponent<Image>().color = color;
            DragImage.GetComponent<Image>().raycastTarget = true;
            DropEnd?.Invoke(TempSlotIndex, TempSprite);
        }
        
    }

    
    
    public void OnDrop(PointerEventData eventData)
    {
        if(!UserSkillSlot.CanDrag)
            EventDrop?.Invoke(DragImage.GetComponent<Image>().sprite.name, transform.GetSiblingIndex());       
        //transform.GetComponent<Image>().sprite = DragImage.GetComponent<Image>().sprite;
    }

}
