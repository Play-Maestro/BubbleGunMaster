using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private string scoreString = "";
    public Text scoreText;
    public FileReadWrite fileIO;

    // Start is called before the first frame update
    void Start()
    {
        int[] scores = fileIO.GetScores();
        int rank = fileIO.scoreIndex(RuntimeData.myScore);
        for (int i = 0; i < scores.Length; i ++)
        {
            if(i == rank)
            {
                scoreString += "*";
            }
            scoreString += ((i + 1).ToString() + ": ");
            scoreString += scores[i].ToString();
            if (i == rank)
            {
                scoreString += "*";
            }
            scoreString += "\n";
        }
        scoreText.text = scoreString;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
