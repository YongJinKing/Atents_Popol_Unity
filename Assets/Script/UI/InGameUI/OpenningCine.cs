using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenningCine : MonoBehaviour
{
    public GameObject plFace;
    public GameObject moFace;

    public Vector2 plPos;
    public Vector2 moPos;

    public Vector2 plEndPos;
    public Vector2 moEndPos;

    public float moveSpeed;

    public float shackMag;

    byte nowState = 0;

    // Start is called before the first frame update
    void Start()
    {
        //plFace = GetComponent<RectTransform>();
        //moFace = GetComponent<RectTransform>();
        StartCoroutine(Openning());
    }

    IEnumerator Openning()
    {
        while (nowState == 0)
        {
            plPos.x = Mathf.Lerp(plPos.x, plEndPos.x, Time.deltaTime * moveSpeed);
            moPos.x = Mathf.Lerp(moPos.x, moEndPos.x, Time.deltaTime * moveSpeed);
            UIMove();
            if (Mathf.Floor(Vector2.Distance(plPos, plEndPos)) == 0)
            {
                Debug.Log("end");
                ++nowState;
            }
            yield return null;
        }
        float length = 0;
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
                Debug.Log("Shackend");
                plEndPos = new Vector2(-1350, 0);
                moEndPos = new Vector2(1350, 0);
                yield return new WaitForSeconds(1);
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
                Debug.Log("end");
                ++nowState;
                StopCoroutine(Openning());
            }
            yield return null;
        }
    }

    private void UIMove()
    {
        plFace.GetComponent<RectTransform>().anchoredPosition = plPos;
        moFace.GetComponent<RectTransform>().anchoredPosition = moPos;
    }
}
