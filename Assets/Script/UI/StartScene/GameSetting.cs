using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    public Sprite Img_Sound;
    public Sprite Img_Mute;
    public GameObject Paper;
    bool[] SoundBtn = new bool[5];

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
    }
    public void PressedBtnInUI(int index)
    {
        
        if(index == 3)//Cancel Btn
            gameObject.SetActive(false);
        else
        {
            var go = Paper.transform.GetChild(index + 1).GetChild(0).GetChild(0).GetComponent<Image>();//Paper//Master,Bgm,Sfx,/Icon/Image
            SoundBtn[index] = !SoundBtn[index];
            Debug.Log(SoundBtn[index]);
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
    public void PressedBtnInGame(int index)
    {
        Time.timeScale = 0.0f;
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
            Time.timeScale = 1;
            SceneLoading.SceneNum(2);
            SceneManager.LoadScene(1);
            gameObject.SetActive(false);
        }
        transform.GetChild(1).gameObject.SetActive(false);

        
    }

}
