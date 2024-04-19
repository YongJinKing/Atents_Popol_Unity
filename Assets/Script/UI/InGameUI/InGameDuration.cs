using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class InGameDuration : MonoBehaviour
{
    Coroutine Cor_Armor;
    Coroutine Cor_Weapon;
    float StartTime = 0.0f;
    Color ArmorColor = new Color();
    private void Start() 
    {   
        DisPlayDuration();
    }
    public void DisPlayDuration()
    {   
        if(Cor_Armor != null)
        {
            StopCoroutine(Cor_Armor);
            Cor_Armor = null;
        }
        if(Cor_Weapon != null)
        {
            StopCoroutine(Cor_Weapon);
            Cor_Weapon = null;
        }
        Cor_Armor = StartCoroutine(DurationDisplay(transform.GetChild(0).GetChild(0).GetChild(0)));
        Cor_Weapon = StartCoroutine(DurationDisplay(transform.GetChild(0).GetChild(0).GetChild(1)));
        
    }
    
    
    IEnumerator DurationDisplay(Transform go)
    {
        int Duration = 0;
        if(go.name == "Armor")
            Duration = DataManager.instance.playerData.Armor_Duration;
        else
            Duration = DataManager.instance.playerData.Weapon_Duration;

       
        go.GetChild(0).gameObject.SetActive(false);

        bool DurationDead = false;
        
        float TimeLimit = 2.0f;
        while(Duration <= 50)
        {
            if(Duration <= 50)
            {
                ArmorColor = new Color(255/255, 255/255, 0/255);
                if(Duration <= 25)
                {
                    ArmorColor = new Color(255/255, 0/255, 0/255);
                    if(Duration <= 0)
                    {
                        DurationDead = true;
                        ArmorColor = new Color(255/255, 255/255, 255/255);
                    }
                }
            }
            
            StartTime += Time.deltaTime;
            if(StartTime <= 1.0)
            {
                go.GetComponent<Image>().color = ArmorColor;
                if(DurationDead)
                    go.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                go.GetComponent<Image>().color = new Color(255/255, 255/255, 255/255);
                go.GetChild(0).gameObject.SetActive(false);
            }   
            if(StartTime >= TimeLimit)
                StartTime = 0.0f;

            yield return null;
        }
        go.GetComponent<Image>().color = new Color(255/255, 255/255, 255/255);

    }
    
}
