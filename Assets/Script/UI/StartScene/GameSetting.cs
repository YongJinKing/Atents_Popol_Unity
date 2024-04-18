using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    public Sprite Img_Sound;
    public Sprite Img_Mute;
    public GameObject Paper;
    List<bool> SoundBtn = new List<bool>();

    public Slider MasterSlider;
    public Slider BgmSlider;
    public Slider SfxSlider;

    private void Start() 
    {
        for(int i = 0; i < transform.GetComponentsInChildren<Button>().Length - 1; i++)
        {
            SoundBtn.Add(true);
        }
        MasterSlider.onValueChanged.AddListener(SoundManager.instance.SetMasterVolume);
        BgmSlider.onValueChanged.AddListener(SoundManager.instance.SetBgmVolume);
        SfxSlider.onValueChanged.AddListener(SoundManager.instance.SetSfxVolume);
        MasterSlider.value = SoundManager.instance.MasterValue;
        BgmSlider.value = SoundManager.instance.BgmValue;
        SfxSlider.value = SoundManager.instance.SfxValue;
    }
    public void PressedBtn(int index)
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
}
