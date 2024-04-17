using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    
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
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayMusic()
    {
        if(audioSource.isPlaying) return;
        audioSource.Play();
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }
    
}
