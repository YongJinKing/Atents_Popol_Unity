using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image image;
    public float duration = 30.0f;

    private void Start()
    {
        StartCoroutine(ChangeFillAmountOverTime());
    }

    private IEnumerator ChangeFillAmountOverTime()
    {
        float currentTime = 0.0f;
        float startFillAmount = 0.0f;
        float endFillAmount = 1.0f;

        while (currentTime < duration)
        {
            float fillAmount = Mathf.Lerp(startFillAmount, endFillAmount, currentTime / duration);
            fillAmount = Mathf.Clamp01(fillAmount);

            image.fillAmount = fillAmount;
            currentTime += Time.deltaTime;

            yield return null;
        }

        image.fillAmount = endFillAmount;
    }
}
/*
 * public Slider myEnergySlider;
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
        ChangeEnergySlider();
    }

    public void ChangeEnergySlider()
    {
        myEnergySlider.value = pl.EnergyGage;
    }
*/
