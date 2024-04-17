using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public static int nextScene;
    public Slider ProgressBar;
    public Text LoadTxt;

    void Start()
    {
        LoadTxt.text = "Loading...";
        SoundManager.instance.StopBgmMusic();
        StartCoroutine(LoadScene());
    }

    public static void SceneNum(int i)
    {
        nextScene = i;
    }

    void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if(ProgressBar.value < 0.9f)
            {
                ProgressBar.value = Mathf.MoveTowards(ProgressBar.value, 0.9f, Time.deltaTime);
            }
            else
            {
                LoadTxt.text = "Done!";
                Invoke("NextScene", 1f);
            }
        }
    }
}
