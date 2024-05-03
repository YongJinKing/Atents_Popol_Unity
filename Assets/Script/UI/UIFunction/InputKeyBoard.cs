using UnityEngine;
using UnityEngine.Events;

public class InputKeyBoard : MonoBehaviour
{
    public UnityEvent EscKeyUpAct;
    public UnityEvent EscKeyDownAct;
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))//상승 펄스
        {
            EscKeyDownAct?.Invoke();
        }
        if(Input.GetKeyUp(KeyCode.Escape))//하강 펄스
        {
            EscKeyUpAct?.Invoke();
        }
    }
}
