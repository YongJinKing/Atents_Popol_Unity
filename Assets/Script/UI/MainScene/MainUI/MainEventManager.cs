using UnityEngine;




public class MainEventManager : MonoBehaviour
{
    public GameObject gameCanvas;//게임 
    public GameObject DontDestoyedObj;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBgmMusic("MainBgm");
        for(int i = 2; i < gameCanvas.transform.childCount; i++)
                gameCanvas.transform.GetChild(i).gameObject.SetActive(false);
            transform.gameObject.SetActive(true);

    }
    
    void ChangeMainUI()
    {
        
        for(int i = 2; i < gameCanvas.transform.childCount; i++)
        {
            gameCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void MainUiControll(int index)
    {
        ChangeMainUI();
        gameCanvas.transform.GetChild(index+3).gameObject.SetActive(true);
    }
    
}
