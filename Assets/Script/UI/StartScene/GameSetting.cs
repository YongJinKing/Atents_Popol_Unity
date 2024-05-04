using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameSetting : MonoBehaviour
{
    public Sprite Img_Sound;
    public Sprite Img_Mute;
    public GameObject Paper;
    bool[] SoundBtn = new bool[5];

    public UnityEvent CinematicStart;
    public Slider MasterSlider;
    public Slider BgmSlider;
    public Slider SfxSlider;
    private void Start() 
    {
        for(int i = 0; i < 3; i++)
        {
            SoundBtn[i] = true;
        }
        MasterSlider.onValueChanged.AddListener(SoundManager.instance.SetMasterVolume);
        BgmSlider.onValueChanged.AddListener(SoundManager.instance.SetBgmVolume);
        SfxSlider.onValueChanged.AddListener(SoundManager.instance.SetSfxVolume);
        MasterSlider.value = SoundManager.instance.MasterValue;
        BgmSlider.value = SoundManager.instance.BgmValue;
        SfxSlider.value = SoundManager.instance.SfxValue;
        SoundBtn[0] = SoundManager.instance.MasterMuteCheck;
        SoundBtn[1] = SoundManager.instance.BgmMuteCheck;
        SoundBtn[2] = SoundManager.instance.SfxMuteCheck;
        SoundMuteImageChange();
    }
    void SoundMuteImageChange()
    {
        for(int i = 0; i < 3; i++)
        {
            var go = Paper.transform.GetChild(i + 1).GetChild(0).GetChild(0).GetComponent<Image>();
            if(SoundBtn[i])
                go.sprite = Img_Sound;
            else
                go.sprite = Img_Mute;
        }
        
    }
   
    public void PressedEscBtn()
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }


    public void PressedBtnInUI(int index)
    {
        
        if(index == 3)//Cancel Btn
        {
            gameObject.SetActive(false);
            CinematicStart?.Invoke();
            Time.timeScale = 1.0f;
        }
        else
        {
            var go = Paper.transform.GetChild(index + 1).GetChild(0).GetChild(0).GetComponent<Image>();//Paper//Master,Bgm,Sfx,/Icon/Image
            SoundBtn[index] = !SoundBtn[index];
            Debug.Log(SoundBtn[index]);
            if(SoundBtn[index])
                go.sprite = Img_Sound;
            else
                go.sprite = Img_Mute;
            SoundManager.instance.MuteCheck(index, SoundBtn[index]);
        }
    }
    public void PressedBtnInGame(int index)
    {
        
        if(index == 3)//giveUp Btn
        {
            transform.GetChild(1).gameObject.SetActive(true);
            
        }
        else if(index == 4)//Cancel Btn
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
            
        else
        {
            var go = Paper.transform.GetChild(index + 1).GetChild(0).GetChild(0).GetComponent<Image>();//Paper//Master,Bgm,Sfx,/Icon/Image
            SoundBtn[index] = !SoundBtn[index];
            if(SoundBtn[index])
            {
                go.sprite = Img_Sound;
            }
            else
            {
                go.sprite = Img_Mute;
            }
            SoundManager.instance.MuteCheck(index, SoundBtn[index]);

        }
    }
    public void PressedBtnYesOrNo(int index)
    {
        if(index == 0)//씬체인지
        {
            Time.timeScale = 1.0f;
            SceneLoading.SceneNum(2);
            SceneManager.LoadScene(1);
            gameObject.SetActive(false);
        }
        transform.GetChild(1).gameObject.SetActive(false);

        
    }

}
