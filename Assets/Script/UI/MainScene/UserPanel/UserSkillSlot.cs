using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;
using UnityEngine.Events;
using Unity.VisualScripting;
using TMPro;


public class UserSkillSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    

    GameObject DragImage;
    public int WeaponType;
    public int SkillLevel;
    public int SkillEnergy;
    public string DetailDesc;
    
    public static bool CanDrag = false;
    
    
    private void Start() 
    {
        DragImage = GameObject.Find("DragImage");
    }
    public void OnBeginDrag(PointerEventData eventData)//Drag Start
    {
        if(transform.GetChild(1).gameObject.activeSelf )
            CanDrag = true;
        else
            CanDrag = false;
        if(!CanDrag)
        {
            Image image = transform.Find("Paper").Find("Image").GetComponent<Image>();
            DragImage.GetComponent<Image>().sprite = image.sprite;
            Color color = DragImage.GetComponent<Image>().color;
            color.a = 1.0f;
            DragImage.GetComponent<Image>().color = color;
            DragImage.GetComponent<Image>().raycastTarget = false;
        }
        
    }
    public void OnDrag(PointerEventData eventData)//Drag Ing
    {
      
        if(!CanDrag)
        {
            //Debug.Log("드래그실행?");
            DragImage.GetComponent<RectTransform>().anchoredPosition = eventData.position;
        }
    }
    public void OnEndDrag(PointerEventData eventData)//Drag End
    {
        if(!CanDrag)
        {
            //Debug.Log("앤드실행?");
            DragImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            Color color = DragImage.GetComponent<Image>().color;
            color.a = 0.0f;
            DragImage.GetComponent<Image>().color = color;
            DragImage.GetComponent<Image>().raycastTarget = true;
        }
    }
    
}
