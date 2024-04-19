using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DamageTextController : MonoBehaviour
{
    private static DamageTextController _instance;

    public static DamageTextController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DamageTextController>();
                if(_instance == null)
                {
                    Debug.Log("DamageTxtController Is Not Found");
                }
            }
            return _instance;
        }
    }

    public GameObject DmgTxt;
    Vector2 ScreenPos;

    public void DmgTxtPrint(Vector3 point,int Dmg)
    {
        ScreenPos = Camera.main.WorldToScreenPoint(point);
        GameObject dmg = Instantiate(DmgTxt,ScreenPos,Quaternion.identity,transform);
        dmg.GetComponent<TMP_Text>().text = Dmg.ToString();
    }
}
