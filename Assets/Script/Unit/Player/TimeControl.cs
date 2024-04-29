using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    
    public void ChangeGameTime(float value)
    {
        Time.timeScale = value;
    }
    
}
