using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    public GameObject player;
    public AudioSource musicPlayer;
    public AudioClip loseSound;
    public TextMeshProUGUI scoreText;
    private float playerPosition = 0;
    private int coins = 0;
    private float timeAlive = 0;
    private float scoreUpdateTime = 0;
    private bool isAlive = true;
    public FileReadWrite fileIO;

    public void YouLose()
    {
        isAlive = false;
        musicPlayer.clip = loseSound;
        musicPlayer.Play();
        musicPlayer.loop = false;
        int score = ((int)Mathf.Round(playerPosition * 10) + (coins * 10) + (int)Mathf.Round(timeAlive * 5));
        RuntimeData.myScore = score;
        fileIO.AddScore(score);
        fileIO.Save();
        Invoke("ChangeScene", loseSound.length);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Scores");
    }

    public void SetPlayerPosition(float playerPos)
    {
        if (playerPos > playerPosition)
        {
            playerPosition = playerPos;
        }
    }

    public void AddCoin()
    {
        coins++;
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            timeAlive += Time.deltaTime;
        }
        if (scoreUpdateTime <= 0)
        {
            int score = ((int)Mathf.Round(playerPosition * 10) + (coins * 10) + (int)Mathf.Round(timeAlive * 5));
            scoreText.SetText("Score: " + score.ToString());
            scoreUpdateTime = 2;
        }
        scoreUpdateTime -= Time.deltaTime;
    }
}
