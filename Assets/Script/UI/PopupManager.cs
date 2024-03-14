using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopupManager : MonoBehaviour
{
    void OnPopup()
    {
        transform.gameObject.SetActive(true);
    }
    void offPopup()
    {
        transform.gameObject.SetActive(false);
    }
    
}
