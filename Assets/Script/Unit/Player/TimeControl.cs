using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float Timescale;
    public void Update()
    {
        Time.timeScale = Timescale;
    }
    
}
