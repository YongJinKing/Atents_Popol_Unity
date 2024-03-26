using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tempEnergy : MonoBehaviour
{
    public Slider myEnergySlider;
    GameObject Player;
    Player pl;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        pl = Player.GetComponent<Player>();
        pl.EnergyGage = pl.MaxEnergyGage;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeEnergySlider();
    }

    public void ChangeEnergySlider()
    {
        myEnergySlider.value = pl.EnergyGage / pl.MaxEnergyGage;
    }
}
