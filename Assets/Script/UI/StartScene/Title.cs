using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject Canvas;
    private void Start() {
        
        SoundManager.instance.PlayBgmMusic("TitleMusic");
    }
    public void PressedBtn(int index)
    {
        MenuAct(index+1);
    }
    public void MenuAct(int index)
    {
        if (index == 0)
            Canvas.transform.Find("Title").gameObject.SetActive(false);
        Canvas.transform.GetChild(index).gameObject.SetActive(true);
    }
    public void EscButtonAct()
    {
        
        if(!Canvas.transform.GetChild(3).gameObject.activeSelf)
        {
            MenuAct(3);
        }
            
    }   
}
