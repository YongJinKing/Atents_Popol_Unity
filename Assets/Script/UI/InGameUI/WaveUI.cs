using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public int nowWave = 1;
    bool iscine = false;
    public int TotalWave;

    public GameObject WaveTxt;
    public GameObject WaveNum;
    public GameObject StopWatchUI;

    public Vector2 txtPos;
    public Vector2 txtEndPos;

    public float txtScale;
    public float txtEndScale;

    public float moveSpeed;

    public GameObject BroadCastCore;
    public GameObject BossOpening;

    public Coroutine stopWatch;

    private void Start()
    {
        stopWatch = StartCoroutine("StopWatch");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0) && !iscine)
        {
            nextWave();
        }
    }

    public void nextWave()
    {
        if (nowWave! < TotalWave)
        {
            state = 0;
            StartCoroutine(WaveChange());
        }
    }

    public void IsBossCo()
    {
        StartCoroutine("IsBossStage");
    }
    IEnumerator IsBossStage()
    {
        yield return new WaitForEndOfFrame();
        BossOpening.SetActive(true);

    }

    int state = 0;
    IEnumerator WaveChange()
    {
        iscine = true;
        state = 0;
        ++nowWave;
        while (state == 0)      //Move to Center
        {
            txtPos.y = Mathf.Lerp(txtPos.y, txtEndPos.y, Time.deltaTime * moveSpeed);
            txtScale = Mathf.Lerp(txtScale, txtEndScale, Time.deltaTime * moveSpeed);
            UIMove();
            if (Mathf.Floor(Vector2.Distance(txtPos, txtEndPos)) == 0)
            {
                if(nowWave >= TotalWave)
                {
                    state = 10;
                    break;
                }
                ++state;
            }
            yield return null;
        }
        float alph = 1.0f;
        while (state == 1)      //Change num
        {
            alph -= Time.deltaTime*3;
            WaveNum.GetComponent<TMP_Text>().alpha = Mathf.Abs(alph);
            if (Mathf.Floor(alph*10) <= 0)
            {
                WaveNum.GetComponent<TMP_Text>().text = nowWave.ToString();
            }
            if(alph <= -1)
            {
                txtEndPos.y = 1025;
                txtEndScale = 0.44f;
                alph = 1.0f;
                ++state;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }

        while (state == 10)     //Boss
        {
            alph -= Time.deltaTime * 3;
            WaveTxt.GetComponent<TMP_Text>().alpha = Mathf.Abs(alph);
            WaveNum.GetComponent<TMP_Text>().alpha = Mathf.Abs(alph);
            if (Mathf.Floor(alph * 10) <= 0)
            {
                WaveNum.GetComponent<TMP_Text>().text = "";
                WaveTxt.GetComponent<TMP_Text>().text = "   Boss";
            }
            if (alph <= -1)
            {
                txtEndPos.y = 1025;
                txtEndScale = 0.44f;
                alph = 1.0f;
                state = 2;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }

        while (state == 2)      //Move back
        {
            txtPos.y = Mathf.Lerp(txtPos.y, txtEndPos.y, Time.deltaTime * moveSpeed);
            txtScale = Mathf.Lerp(txtScale, txtEndScale, Time.deltaTime * moveSpeed);
            UIMove();
            if (Mathf.Floor(Vector2.Distance(txtPos, txtEndPos)) == 0)
            {
                txtEndPos.y = 540;
                txtEndScale = 1.5f;
                ++state;
                iscine = false;
            }
            yield return null;
        }
    }

    private void UIMove()
    {
        WaveTxt.GetComponent<RectTransform>().position = txtPos;
        WaveTxt.GetComponent<RectTransform>().localScale = new Vector3(txtScale, txtScale, 1);
    }

    float playTime = 0;
    IEnumerator StopWatch()
    {
        while (true)
        {
            playTime += Time.deltaTime;
            StopWatchUI.GetComponent<TMP_Text>().text = "Time : " + Mathf.Floor(playTime).ToString()+"s";
            yield return null;
        }
    }

    public void saveTime()
    {
        if( stopWatch != null)
            StopCoroutine(stopWatch);
        DataManager.instance.playerData.PlayTime += (int)playTime;
    }



    public void OnLoadStageEvent(int totalStageWaves)
    {
        this.TotalWave = totalStageWaves;
    }

    public void OnWaveEndEvent()
    {
        ++nowWave;
    }

    public void OnStageEnd()
    {

    }

    public void OnBossWaveStartEvent()
    {

    }
}
