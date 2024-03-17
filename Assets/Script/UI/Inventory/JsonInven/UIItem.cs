using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Image icon;
    public TMP_Text txtCount;
    public Button btn;
    public int id;

    public void Init(int id, Sprite icon, int amount)
    {
        this.id = id;
        this.icon.sprite = icon;
        this.txtCount.text = amount.ToString();

    }
}
