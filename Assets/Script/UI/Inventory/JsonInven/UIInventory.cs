using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.EventSystems;

public class UIInventory : MonoBehaviour
{
    public GameObject[] arrEmpty;
    public GameObject smithUi;
    private List<UIItem> items;

    private GameObject prefab;
    
    public Transform empty;


    void Start()
    {
        ItemDataManager.GetInstance().LoadDatas();
      
        this.prefab = Resources.Load<GameObject>("UI/UIItem/Empty");
        this.items = new List<UIItem>();

        this.AddItem(100);
        this.AddItem(1000);
    }

    public void AddItem(int id) 
    {
        var parent = this.arrEmpty[this.items.Count];
        var go = Instantiate<GameObject>(this.prefab, parent.transform) ;
        var uiItem = go.GetComponent<UIItem>();
        uiItem.btn.onClick.AddListener(() => 
        {
            smithUi.GetComponent<SmithUi>().ChooseSlot(uiItem.id);
        });
        this.items.Add(uiItem);
        var data = ItemDataManager.GetInstance().dicItemDatas[id];
        string spName = data.Inven_spriteName;
        Debug.Log(spName);
        Sprite sp = Resources.Load<Sprite>($"UI/UIItem/{spName}");
   
        uiItem.Init(id, sp, 1);
        
    }
}