using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [Header("Wave")]
    public int nowWave = 0;
    bool iscine = false;
    public int TotalWave;

    [Space(3)]
    [Header("UISetting")]
    public TMP_Text WaveTxt;
    public TMP_Text WaveNum;
    public TMP_Text StopWatchUI;
    public Image Warnning;
    public Image BossHpBar;
    public LayerMask layer;
    GameObject BossObject;

    public Vector2 txtEndPos;   //ScreenCenter Pos
    public float txtEndScale;   //Make UI How Much Bigger
    Vector2 txtPos;
    float txtScale = 1;

    public float moveSpeed;
    public byte LimitTime = 3;

    [Space(3)]
    [Header("InstantiateObj")]
    public GameObject BossOpening;
    public GameObject BossTiara;
    public GameObject BossSpawnRing;
    public UnityEngine.Events.UnityEvent WaveRound;
    public UnityEngine.Events.UnityEvent TimeOut;

    

    private void Start()
    {
        StartCoroutine("WaveStart");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            nextWave();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartCoroutine("BossIntro");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            playTime += 10;
        }
    }

    public void nextWave()
    {
        if (nowWave < TotalWave)
        {
            state = 0;
            StartCoroutine("NextWave");
        }
    }


    int state = 0;
    float alph = 1.0f;
    IEnumerator WaveStart()
    {
        WaveTxt.alignment = TextAlignmentOptions.Center;
        txtPos = txtEndPos;
        txtScale = txtEndScale * 2;
        MoveT();
        ChangeT(0, "Ready");
        txtScale = txtEndScale * 4;
        yield return new WaitForSeconds(2);
        while (state == 0)
        {
            ChangeT(0, "Start!");
            txtScale = Mathf.Lerp(txtScale,txtEndScale*2,Time.deltaTime*moveSpeed*3);
            MoveT();
            if (Mathf.Approximately(txtScale,4))
            {
                StartCoroutine("StopWatch");
                ++state;
            }
            yield return null;
        }
        while (state == 1)
        {
            alph -= Time.deltaTime * moveSpeed * 0.5f;
            WaveTxt.alpha = WaveNum.alpha = Mathf.Abs(alph);
            if (Mathf.Floor(alph * 10) <= 0)
            {
                ChangeT(1, nowWave.ToString());
                txtScale = txtEndScale;
                MoveT();
                WaveTxt.alignment = TextAlignmentOptions.Left;
                ChangeT(0, "Wave");
                ChangeT(1, "1");
            }
            if (alph <= -1)
            {
                alph = 1.0f;
                ++state;
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
        if (nowWave == TotalWave) StartCoroutine("BossIntro");
        while (state == 0)      //Move to Center
        {
            txtPos.y = Mathf.Lerp(txtPos.y, txtEndPos.y, Time.deltaTime * moveSpeed);
            txtScale = Mathf.Lerp(txtScale, txtEndScale, Time.deltaTime * moveSpeed);
            MoveT();
            if (Vector2.Distance(txtPos, txtEndPos) <= 1)
            {
                if (nowWave == TotalWave)
                {
                    state = 10;
                    break;
                }
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

        while (state == 10)
        {
            alph -= Time.deltaTime * moveSpeed * 0.5f;
            WaveTxt.alpha = WaveNum.alpha = Mathf.Abs(alph);
            if (Mathf.Floor(alph * 10) <= 0)
            {
                ChangeT(1, "");
                WaveTxt.alignment = TextAlignmentOptions.Center;
                ChangeT(0, "Boss");
            }
            if (alph <= -1)
            {
                alph = 1.0f;
                state = 2;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }

        while (state == 2)      //Move back
        {
            txtPos.y = Mathf.Lerp(txtPos.y, 0, Time.deltaTime * moveSpeed);
            txtScale = Mathf.Lerp(txtScale, 1, Time.deltaTime * moveSpeed);
            MoveT();
            if (Mathf.Abs(txtPos.y) <= 0.1f)
            {
                txtPos = Vector2.zero;
                MoveT();
                WaveRound?.Invoke();
                ++state;
                iscine = false;
            }
            yield return null;
        }
        if(state == 3 && nowWave == TotalWave)
        {
            txtPos = WaveTxt.transform.parent.localPosition;
            txtEndPos = WaveTxt.transform.parent.parent.GetChild(0).localPosition;
            ++state;
        }
        while (state == 4 && nowWave == TotalWave)
        {
            txtPos.y += Time.deltaTime * 70;
            txtEndPos.y -= Time.deltaTime * 70;
            WaveTxt.transform.parent.localPosition = txtPos;
            WaveTxt.transform.parent.parent.GetChild(0).localPosition = txtEndPos;
            if(txtEndPos.y <= 485)
            {
                ++state;
            }
            yield return null;
        }
    }

    IEnumerator BossIntro()
    {
        Instantiate(BossSpawnRing, Vector3.zero, Quaternion.identity);
        Color color = Warnning.color;
        byte c = 5;
        while (c > 0)
        {
            for (float alpha = 0f; alpha <= 70f; alpha += Time.deltaTime * 140)
            {
                color.a = alpha / 255f;
                Warnning.color = color;
                yield return null;
            }
            for (float alpha = 70f; alpha >= 0f; alpha -= Time.deltaTime * 140)
            {
                color.a = alpha / 255f;
                Warnning.color = color;
                yield return null;
            }
            --c;
        }
        Warnning.enabled = false;
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
        Collider[] monsterColliders = Physics.OverlapSphere(Vector3.zero, 30f, layer);
        if (monsterColliders.Length > 0)
        {
            BossObject = monsterColliders[0].gameObject;
            //Debug.Log(BossObject.transform.root.name);
        }
    }

    public void BossHPFresh(int ReferenceValue, int Value)
    {
        BossHpBar.fillAmount = (float)Value / (float)ReferenceValue;
    }

    private void MoveT()
    {
        WaveTxt.transform.localPosition = txtPos;
        WaveTxt.transform.localScale = Vector3.one * txtScale;
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
            ++playTime;
            timeDisplay();
            yield return new WaitForSeconds(1);
        }
    }

    private void timeDisplay()
    {
        if(playTime >= LimitTime * 60)
        {
            StopWatchUI.text = "TimeOut";
            TimeOut?.Invoke();
            saveTime();
        }
        else if (playTime > (LimitTime - 1) * 60)
        {
            StopWatchUI.color = Color.red;
            StopWatchUI.transform.localScale = Vector3.one * 1.5f;
            StopWatchUI.text = (LimitTime * 60 - playTime).ToString();
        }
        else
        {
            StopWatchUI.text = string.Format("{0:0} : {1:00}", Mathf.FloorToInt(playTime / 60), playTime % 60);
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
