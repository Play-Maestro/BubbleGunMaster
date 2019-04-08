using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{

    public float autoLoad = 0;

    private void Start()
    {
        if(autoLoad != 0)
        {
            Invoke("LoadNextScene", autoLoad);
        }
    }

    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToScores()
    {
        SceneManager.LoadScene("Scores");
    }



}
