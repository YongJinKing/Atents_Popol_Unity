using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class CinematicSystem : MonoBehaviour
{
    public VideoPlayer CinematicVideo;
    public GameObject Popup;
 

    private void Start() 
    {
        CinematicVideo.time = 0.0f;
        CinematicVideo.loopPointReached += CheckOver;
    }
    public void PressedBtn(int index)
    {
        if(index == 0)//Sence Change
        {
            Popup.gameObject.SetActive(true);
            CinematicVideo.Pause();
        }
    }
    public void PressedYesOrNoBtn(int index)
    {
        if(index == 0)
        {
            SceneLoading.SceneNum(2);
            SceneManager.LoadScene(1);
        }
        CinematicVideo.Play();
        Popup.gameObject.SetActive(false);
    }

    void CheckOver(VideoPlayer vp)
    {
        //SenceChange
        SceneLoading.SceneNum(2);
        SceneManager.LoadScene(1);
        print("Video Is Over");
    }
}
