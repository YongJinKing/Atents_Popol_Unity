using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float Timescale;
    public void Update()
    {
        Time.timeScale = Timescale;
    }
    
}
