using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider myEnergySlider;
    GameObject Player;
    Player pl;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        pl = Player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeEnergySlider()
    {
        myEnergySlider.value = pl.EnergyGage;

    }
}
