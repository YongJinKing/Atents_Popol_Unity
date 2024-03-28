using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider myHpSlider;
    GameObject Player;
    Player pl;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        pl = Player.GetComponent<Player>();
        pl.HP = pl.MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHpSlider();
        Debug.Log("Ã¼·ÂÀº," + myHpSlider.value);
    }

    public void ChangeHpSlider()
    {
        myHpSlider.value = pl.HP / pl.MaxHP;
        //Debug.Log(myHpSlider.value);
    }

}
