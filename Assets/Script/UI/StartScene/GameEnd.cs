using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public void PressedBtn(int index)
    {
        if(index == 0)
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
        transform.gameObject.SetActive(false);
    }
    public void PressedEscBtn()
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        
    }
}
