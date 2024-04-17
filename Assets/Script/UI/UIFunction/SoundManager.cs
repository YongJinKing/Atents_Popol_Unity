using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audioBgmClips;
    public AudioClip[] audioSfxClips;
    public AudioSource BgmPlayer;
    public AudioSource SfxPlayer;
    public AudioMixer mixer;
    
    public static SoundManager instance;
    private void Awake()    
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }
    private void Start() 
    {
        BgmPlayer.volume = DataManager.instance.Bgm_Volum;
    }
    public void PlayBgmMusic(string Type)
    {
        int index = 0;

        switch(Type)
        {
            case "TitleBgm": index = 0; break;
            case "MainBgm": index = 1; break;
        }
        BgmPlayer.clip = audioBgmClips[index];
        BgmPlayer.Play();
    }
    public void StopBgmMusic()
    {
        BgmPlayer.Stop();
    }
    public void SetMusicVolume(float volume)
    {
        BgmPlayer.volume = volume;
    }
    
   
    
}
