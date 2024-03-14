using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public int nextScene;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("NextScene",1f);
    }

    // Update is called once per frame
    void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
