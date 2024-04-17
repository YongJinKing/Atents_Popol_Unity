using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    public int nowWave = 0;
    bool iscine = false;
    public int TotalWave;

    public TMP_Text WaveTxt;
    public TMP_Text WaveNum;
    public TMP_Text StopWatchUI;
    public Image Warnning;

    public Vector2 txtEndPos;   //ScreenCenter Pos
    public float txtEndScale;   //Make UI How Much Bigger

    public float moveSpeed;

    public GameObject BossOpening;
    public GameObject BossTiara;
    public GameObject BossSpawnRing;
    public UnityEngine.Events.UnityEvent WaveRound;

    private void Start()
    {
        StartCoroutine("WaveStart");
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
            StartCoroutine("NextWave");
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
        Instantiate(BossTiara);
        Instantiate(BossSpawnRing,Vector3.zero,Quaternion.identity);
    }

    int state = 0;
    float alph = 1.0f;
    IEnumerator WaveStart()
    {
        WaveTxt.alignment = TextAlignmentOptions.Center;
        MoveT();
        ChangeT(0, "Ready");
        txtEndScale = txtEndScale * 2;
        yield return new WaitForSeconds(2);
        while (state == 0)
        {
            ChangeT(0, "Start!");
            txtEndScale = Mathf.Lerp(txtEndScale, 4, Time.deltaTime * moveSpeed);
            MoveT();
            if (Mathf.Approximately(txtEndScale,4))
            {
                WaveTxt.alignment = TextAlignmentOptions.Left;
                txtEndScale = 2;
                MoveT();
                ChangeT(0, "Wave");
                ChangeT(1, "1");
                state = 2;
                StartCoroutine("NextWave");
            }
            yield return null;
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(2);
        iscine = true;
        ++nowWave;
        while (state == 0)      //Move to Center
        {
            txtEndPos.y = Mathf.Lerp(txtEndPos.y, txtEndPos.y, Time.deltaTime * moveSpeed);
            txtEndScale = Mathf.Lerp(txtEndScale, txtEndScale, Time.deltaTime * moveSpeed);
            MoveT();
            if (Mathf.Floor(Vector2.Distance(txtEndPos, txtEndPos)) == 0)
            {
                ++state;
            }
            yield return null;
        }

        while (state == 1)      //Change num
        {
            alph -= Time.deltaTime * moveSpeed * 0.5f;
            WaveNum.alpha = Mathf.Abs(alph);
            if (Mathf.Floor(alph * 10) <= 0)
            {
                ChangeT(1, nowWave.ToString());
            }
            if (alph <= -1)
            {
                alph = 1.0f;
                ++state;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }

        while (state == 2)      //Move back
        {
            txtEndPos.y = Mathf.Lerp(txtEndPos.y, 0, Time.deltaTime * moveSpeed);
            txtEndScale = Mathf.Lerp(txtEndScale, 1, Time.deltaTime * moveSpeed);
            MoveT();
            if (Mathf.Floor(txtEndPos.y) == 0)
            {
                if (nowWave > 1) WaveRound?.Invoke();
                ++state;
                iscine = false;
            }
            yield return null;
        }
    }


    /*IEnumerator WaveChange()
    {
        if(nowWave >0)
            yield return new WaitForSeconds(2);
        iscine = true;
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
                txtEndScale = 1f;
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
                txtEndScale = 1f;
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
                txtEndScale = 2f;
                if(nowWave>1)WaveRound?.Invoke();
                ++state;
                iscine = false;
            }
            yield return null;
        }
    }*/

    private void MoveT()
    {
        WaveTxt.transform.localPosition = txtEndPos;
        WaveTxt.transform.localScale = new Vector3(txtEndScale, txtEndScale, 1);
    }

    private void ChangeT(int i, string s)
    {
        TMP_Text t = null;
        switch(i)
        {
            case 0: t = WaveTxt;
                break;
            case 1: t = WaveNum;
                break;
        }
        t.text = s;
    }



    float playTime = 0;
    IEnumerator StopWatch()
    {
        while (true)
        {
            playTime += Time.deltaTime;
            StopWatchUI.text = "Time : " + Mathf.Floor(playTime).ToString()+"s";
            yield return null;
        }
    }

    public void saveTime()
    {
        StopCoroutine("StopWatch");
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
