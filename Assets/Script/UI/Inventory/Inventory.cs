using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    public Slot[] slots;
    public int TypeCount = 0;
    int SlotMode = 0;
    

#if UNITY_EDITOR
    private void OnValidate() {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
#endif

    void Awake() 
    {
        FreshSlot(0);
    }

    public void FreshSlot(int index)
    {
        SlotMode = index;
        if(SlotMode == 0)
        {
            int i = 0;
            TypeCount = 0;
            for (; i < items.Count && i < slots.Length; i++) 
            {
                slots[i].item = items[i];
                TypeCount++;
            }
            for (; i < slots.Length; i++) 
            {
                slots[i].item = null;
            }
        }
        if(SlotMode == 1)
        {
            TypeCount = 0;
            for (int i = 0; i < items.Count && i < slots.Length; i++) 
            {
                if(items[i].riggingType == RiggingType.Weapon)
                    {
                        slots[TypeCount].item = items[i];
                        TypeCount++;
                    }
            }
            for (int i = TypeCount; i < slots.Length; i++) 
            {
                slots[i].item = null;
            }
        }
        if(SlotMode == 2)
        {
            TypeCount = 0;
            for (int i = 0; i < items.Count && i < slots.Length; i++) 
            {
                if(items[i].riggingType == RiggingType.Armor)
                    {
                        slots[TypeCount].item = items[i];
                        TypeCount++;
                    }
            }
            for (int i = TypeCount; i < slots.Length; i++) 
            {
                slots[i].item = null;
            }
        }
    }

    public void AddItem(Item _item)
    {
        if (items.Count < slots.Length) 
        {
            items.Add(_item);
            FreshSlot(0);
        }
        else 
        {
            print("슬롯이 가득 차 있습니다.");
        }
    }

    
    
}

