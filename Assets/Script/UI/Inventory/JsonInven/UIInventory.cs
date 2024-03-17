using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UIInventory : MonoBehaviour
{
    public GameObject[] arrEmpty;
    private List<UIItem> items;

    private GameObject prefab;
    
    public Transform empty;


    void Start()
    {
        this.prefab = Resources.Load<GameObject>("UI/UIItem/Empty");
        this.items = new List<UIItem>();

        this.AddItem(100);
        this.AddItem(101);
    }

    public void AddItem(int id) {
        var parent = this.arrEmpty[this.items.Count];
        var go = Instantiate<GameObject>(this.prefab, parent.transform) ;
        var uiItem = go.GetComponent<UIItem>();
        this.items.Add(uiItem);
        string spName = "";
        if (id == 100)
        {
            spName = "Leather_Lv1";
        }
        else if (id == 101)
        {
            spName = "Leather_Lv2";
        }
       
    
        Sprite sp = Resources.Load<Sprite>($"UI/UIItem/{spName}");
   
        uiItem.Init(id, sp, 1);
        
    }
}