using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    int nowWave = 1;
    public int TotalWave;
    public GameObject WaveTxt;
    public GameObject WaveNum;

    Vector2 txtPos;
    public Vector2 txtEndPos;

    float txtScale = 0.44f;
    public float txtEndScale;

    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            StartCoroutine(WaveChange());
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            WaveNum.GetComponent<TMP_Text>().text = "1";
        }
    }

    int state = 0;
    IEnumerator WaveChange()
    {
        while (state == 0)      //Move to Center
        {
            txtPos.y = Mathf.Lerp(txtPos.y, txtEndPos.y, Time.deltaTime * moveSpeed);
            txtScale = Mathf.Lerp(txtScale, txtEndScale, Time.deltaTime * moveSpeed);
            UIMove();
            if (Mathf.Floor(Vector2.Distance(txtPos, txtEndPos)) == 0)
            {
                ++state;
            }
            yield return null;
        }
        float alph = 1.0f;
        while (state == 1)      //Change num
        {
            alph -= Time.deltaTime * moveSpeed;
            WaveTxt.GetComponent<TMP_Text>().alpha = Mathf.Abs(alph);
            if (alph >=0)
            {
                yield return null;
            }

        }
    }

    private void UIMove()
    {
        WaveTxt.GetComponent<RectTransform>().position = txtPos;
        WaveTxt.GetComponent<RectTransform>().localScale = new Vector3(txtScale, txtScale, 1);
    }
}
