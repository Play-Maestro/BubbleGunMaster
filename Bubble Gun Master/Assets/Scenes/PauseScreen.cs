using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    private bool paused = false;
    public GameObject pauseScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool getIsPaused()
    {
        return paused;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            if(paused)
            {
                paused = false;
                HidePauseScreen();
            }
            else
            {
                paused = true;
                ShowPauseScreen();
            }
        }
    }

    private void ShowPauseScreen()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    private void HidePauseScreen()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void Continue()
    {
        paused = false;
        HidePauseScreen();
    }

    public void Exit()
    {
        SceneController sceneController = FindObjectOfType<SceneController>();
        HidePauseScreen();
        sceneController.YouLose();
    }
}
