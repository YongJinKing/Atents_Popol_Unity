using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SmithUi : MonoBehaviour
{
    public GameObject GridLine;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ChooseSlot(int id)
    {
        int Slotindex = 0;
        
        for(int i = 0; i < GridLine.transform.childCount; i++)
        {
            
            if(GridLine.transform.GetChild(i).GetChild(0).GetComponent<UIItem>().id
            == id)
            {
                Slotindex = i;
                break;
            }
            Slotindex = -1;
        }
        var go = GridLine.transform.GetChild(Slotindex).GetChild(0);
        if(Slotindex == -1)
            return;
        if(go.GetComponent<UIItem>().isSelected) 
        {
            go.GetComponent<UIItem>().isSelected = false;
            go.GetChild(2).
            GetComponent<Image>().color =
            AlphaColorChange(0.0f, go.GetChild(2).
            GetComponent<Image>().color);
        }
        else
            go.GetComponent<UIItem>().isSelected = true;
        if(go.GetComponent<UIItem>().isSelected)
        {
            go.GetChild(2).
            GetComponent<Image>().color =
            AlphaColorChange(0.3f, go.GetChild(2).
            GetComponent<Image>().color);
        }
    }
    Color AlphaColorChange(float Value, Color Objcolor)
    {
        Color color = Objcolor;
        color.a = Value;
        Objcolor = color;
        return Objcolor;
    }
}
