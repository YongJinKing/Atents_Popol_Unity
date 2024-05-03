using System.Collections;
using TMPro;
using UnityEngine;

public class OpenningCine : MonoBehaviour
{
    public GameObject plFace;
    public GameObject moFace;
    public TextMeshProUGUI BossName;

    public Vector2 plPos;
    public Vector2 moPos;

    public Vector2 plEndPos;
    public Vector2 moEndPos;

    public float moveSpeed;

    public float shackMag;
    public float length = 0;

    [Space(3)]
    [Header("VS Txt")]
    public GameObject VText;
    public GameObject SText;

    public Vector2 VPos;
    public Vector2 SPos;

    public Vector2 VEndPos;
    public Vector2 SEndPos;

    public GameObject VS_VFX;
    public GameObject Frame_VFX;

    public float waitTime;

    public UnityEngine.Events.UnityEvent cineEndEvent;

    byte nowState = 0;

    private void Start()
    {
        switch (DataManager.instance.StageNum)
        {
            case 1: BossName.text = "Slime";
                break;
            case 2: BossName.text = "Snail";
                break;
        }
        StartCoroutine(Openning());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            StartCoroutine(Openning());
        }
    }

    IEnumerator Openning()
    {
        cameraMove.isCine = true;
        while (nowState == 0)
        {
            plPos.x = Mathf.Lerp(plPos.x, plEndPos.x, Time.deltaTime * moveSpeed * 2);
            moPos.x = Mathf.Lerp(moPos.x, moEndPos.x, Time.deltaTime * moveSpeed * 2);

            UIMove();
            if (Mathf.Floor(Vector2.Distance(plPos, plEndPos)) == 0)
            {
                Instantiate(Frame_VFX, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10), Quaternion.identity);
                ++nowState;
            }
            yield return null;
        }

        while (nowState == 1)
        {
            float x = Random.Range(-1, 1) * shackMag;
            float y = Random.Range(-1, 1) * shackMag;

            plPos = new Vector2(plEndPos.x + x, plEndPos.y + y);
            moPos = new Vector2(moEndPos.x + x, moEndPos.y + y);

            UIMove();

            length += Time.deltaTime;

            if (length >= 1)
            {
                plPos = plEndPos;
                moPos = moEndPos;
                UIMove();
                plEndPos.x -= 50;
                moEndPos.x += 50;
                length = 0;
                ++nowState;
            }
            yield return null;
        }
        while (nowState == 2)
        {
            plPos.x = Mathf.Lerp(plPos.x, plEndPos.x, Time.deltaTime * moveSpeed);
            moPos.x = Mathf.Lerp(moPos.x, moEndPos.x, Time.deltaTime * moveSpeed);
            UIMove();
            if (Mathf.Floor(Vector2.Distance(plPos, plEndPos)) == 0)
            {
                ++nowState;
            }
            yield return null;
        }

        while (nowState == 3)
        {
            VPos = Vector2.Lerp(VPos, VEndPos, Time.deltaTime * moveSpeed * 2f);
            SPos = Vector2.Lerp(SPos, SEndPos, Time.deltaTime * moveSpeed * 2f);
            UIMove();
            if (Mathf.Floor(Vector2.Distance(VPos, VEndPos)) == 0)
            {
                Instantiate(VS_VFX, new Vector3(transform.position.x,transform.position.y,transform.position.z-10), Quaternion.identity);
                ++nowState;
            }
            yield return null;
        }

        
        while (nowState == 4)
        {
            float x = Random.Range(-1, 1) * shackMag;
            float y = Random.Range(-1, 1) * shackMag;

            plPos = new Vector2(plEndPos.x + x, plEndPos.y + y);
            moPos = new Vector2(moEndPos.x + x, moEndPos.y + y);

            UIMove();

            length += Time.deltaTime;

            if (length >= 0.3f)
            {
                plPos = plEndPos;
                moPos = moEndPos;
                UIMove();

                plEndPos = new Vector2(-1350, 0);
                moEndPos = new Vector2(1350, 0);
                VEndPos = new Vector2(-800, 40);
                SEndPos = new Vector2(800, -40);

                yield return new WaitForSeconds(waitTime);
                ++nowState;
            }
            yield return null;
        }
        while (nowState == 5)
        {
            plPos.x = Mathf.Lerp(plPos.x, plEndPos.x, Time.deltaTime * moveSpeed);
            moPos.x = Mathf.Lerp(moPos.x, moEndPos.x, Time.deltaTime * moveSpeed);
            VPos = Vector2.Lerp(VPos, VEndPos, Time.deltaTime * moveSpeed * 2f);
            SPos = Vector2.Lerp(SPos, SEndPos, Time.deltaTime * moveSpeed * 2f);
            VText.GetComponent<TMP_Text>().alpha -= Time.deltaTime * moveSpeed * 2;
            SText.GetComponent<TMP_Text>().alpha -= Time.deltaTime * moveSpeed * 2;
            UIMove();

            if (Mathf.Floor(Vector2.Distance(plPos, plEndPos)) == 0)
            {
                ++nowState;
                cameraMove.isCine = false;
                StopCoroutine(Openning());
                cineEndEvent?.Invoke();
                Destroy(gameObject);
            }
            yield return null;
        }
    }

    private void UIMove()
    {
        plFace.GetComponent<RectTransform>().anchoredPosition = plPos;
        moFace.GetComponent<RectTransform>().anchoredPosition = moPos;

        VText.GetComponent<RectTransform>().anchoredPosition = VPos;
        SText.GetComponent<RectTransform>().anchoredPosition = SPos;
    }
}
