using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class CinematicSystem : MonoBehaviour
{
    public VideoPlayer CinematicVideo;
    public GameObject Popup;
    public GameObject GameSetting;
 

    private void Start() 
    {
        CinematicVideo.time = 0.0f;
        CinematicVideo.loopPointReached += CheckOver;
    }
    private void Update() 
    {
        var inst = SoundManager.instance;
        if(inst.MasterValue >= inst.BgmValue)
        {
            CinematicVideo.SetDirectAudioVolume(0, inst.BgmValue);
        }
        else
        {
            CinematicVideo.SetDirectAudioVolume(0, inst.MasterValue);
        }
        if(!inst.MasterMuteCheck || !inst.BgmMuteCheck)
        {
            CinematicVideo.SetDirectAudioMute(0, true);
        }
        else
        {
            CinematicVideo.SetDirectAudioMute(0, false);
        }
    }
    public void PressedBtn(int index)
    {
        if(index == 0)
        {
            Popup.gameObject.SetActive(true);
            CinematicVideo.Pause();
        }
        if(index == 1)
        {
            GameSetting.gameObject.SetActive(true);
            CinematicVideo.Pause();
        }
    }
    public void PressedEscBtn()
    {
        if(GameSetting.gameObject.activeSelf)
        {
            GameSetting.gameObject.SetActive(false);
            CinematicVideo.Play();
        }
        else
        {
            GameSetting.gameObject.SetActive(true);
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
    public void CinematicStart()
    {
        CinematicVideo.Play();
    }

    void CheckOver(VideoPlayer vp)
    {
        //SenceChange
        SceneLoading.SceneNum(2);
        SceneManager.LoadScene(1);
        print("Video Is Over");
    }
    
}
