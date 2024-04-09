using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotDrop : MonoBehaviour, IDropHandler
{
    public GameObject DragImage;
    public void OnDrop(PointerEventData eventData)
    {
        transform.GetComponent<Image>().sprite = DragImage.GetComponent<Image>().sprite;
    }
}
