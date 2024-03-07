using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float slowTime;

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = slowTime;
    }
}
