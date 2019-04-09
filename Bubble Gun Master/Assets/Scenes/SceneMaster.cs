using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{

    public float autoLoad = 0;
    public bool useTargetForScene = false;
    public Texture2D targetTexture;

    private void Start()
    {
        if (useTargetForScene && targetTexture != null)
        {
            Cursor.SetCursor(targetTexture, new Vector2(7.5f, 7.5f), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
        }
        if (autoLoad != 0)
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

    public void Quit()
    {
        Application.Quit();
    }



}
