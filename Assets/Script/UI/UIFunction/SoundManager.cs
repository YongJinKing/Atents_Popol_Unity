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
    public AudioMixer m_AudioMixer;

    public float MasterValue;
    public float BgmValue;
    public float SfxValue;
    
    
    
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
        //BgmPlayer.volume = DataManager.instance.Bgm_Volum;

        SetMasterVolume(0.75f);
        SetBgmVolume(0.75f);
        SetSfxVolume(0.75f);
        
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
    public void SetMasterVolume(float volume)
    {
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        MasterValue = volume;
    }
    public void SetBgmVolume(float volume)
    {
        m_AudioMixer.SetFloat("BgmMusic", Mathf.Log10(volume) * 20);
        BgmValue = volume;
    }
    public void SetSfxVolume(float volume)
    {
        m_AudioMixer.SetFloat("SfxMusic", Mathf.Log10(volume) * 20);
        SfxValue = volume;
    }
    
    public void MuteCheck(int index, bool OnCheck)
    {
        if(OnCheck)
        {
            if(index == 0)
                m_AudioMixer.SetFloat("Master", Mathf.Log10(MasterValue) * 20);
            if(index == 1)
                m_AudioMixer.SetFloat("BgmMusic", Mathf.Log10(BgmValue) * 20);
            if(index == 2)
                m_AudioMixer.SetFloat("SfxMusic", Mathf.Log10(SfxValue) * 20);
        }
        else
        {
            if(index == 0)
                m_AudioMixer.SetFloat("Master", Mathf.Log10(0.0001f) * 20);
            if(index == 1)
                m_AudioMixer.SetFloat("BgmMusic", Mathf.Log10(0.0001f) * 20);
            if(index == 2)
                m_AudioMixer.SetFloat("SfxMusic", Mathf.Log10(0.0001f) * 20);
        }
    }
    public void StopBgmMusic()
    {
        BgmPlayer.Stop();
    }
    
    
   
    
}
