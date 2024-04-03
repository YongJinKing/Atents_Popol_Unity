using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenningCine : MonoBehaviour
{
    public RectTransform plFace;
    public RectTransform moFace;

    public Vector2 plPos;
    public Vector2 moPos;

    public Vector2 plEndPos;
    public Vector2 moEndPos;

    public float moveSpeed;

    byte nowState = 0;

    // Start is called before the first frame update
    void Start()
    {
        plFace = GetComponent<RectTransform>();
        moFace = GetComponent<RectTransform>();
        StartCoroutine(Openning());
    }

    IEnumerator Openning()
    {
        while (nowState == 0)
        {
            plPos.x = Mathf.Lerp(plPos.x,plEndPos.x,Time.deltaTime * moveSpeed);
            plPos.y = Mathf.Lerp(plPos.y,plEndPos.y,Time.deltaTime * moveSpeed);
            Debug.Log(plPos);
            plFace.anchoredPosition = plPos;

            if (Mathf.Approximately(Vector2.Distance(plPos,plEndPos),0))
            {
                Debug.Log("end");
                StopCoroutine(Openning());
            }
            yield return null;

        }
    }
}
