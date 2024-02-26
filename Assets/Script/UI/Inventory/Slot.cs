using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image image;

    private Item _item;
    public Item item {
        get { return _item; }
        set {
            _item = value;
            if (_item != null) {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);
            } else {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
