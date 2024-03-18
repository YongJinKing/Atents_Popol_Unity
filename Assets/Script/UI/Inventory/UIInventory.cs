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
    public List<UIItem> items;

    private GameObject prefab;
    int Slotindex;
    public Transform empty;


    void Start()
    {
        ItemDataManager.GetInstance().LoadDatas();
      
        this.prefab = Resources.Load<GameObject>("UI/UIItem/Empty");
        this.items = new List<UIItem>();

        this.AddItem(100);
        this.AddItem(1000);
        this.AddItem(1001);

        this.RemoveItem(1000);
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

        Sprite sp = Resources.Load<Sprite>($"UI/UIItem/{spName}");
   
        uiItem.Init(id, sp, 1);
        
    }
    private void RemoveItem(int id) 
    { 
        var uiItem = this.items.Find(x => x.id == id);
        if (uiItem != null) {
            for (int i = 0; i < this.arrEmpty.Length; i++) 
            {
                
                var go = this.arrEmpty[i];
                if (go.transform.childCount > 0) 
                {
                    var child = go.transform.GetChild(0);
                    if (child != null) 
                    {
                        var target = child.GetComponent<UIItem>();
                        if (target.id == id) 
                        {
                            Destroy(target.gameObject);
                            this.items.RemoveAt(i);
                            ChangePararnts(i);
                            break;
                        }
                    }
                }
            }
        }
    }

    void ChangePararnts(int index)
    {

    }

}

