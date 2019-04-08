using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTitleScore : MonoBehaviour
{
    public FileReadWrite fileIO;
    public Text scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = ("High Score: " + fileIO.GetScores()[0].ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
