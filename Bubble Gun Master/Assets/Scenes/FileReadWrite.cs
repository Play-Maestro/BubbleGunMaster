using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileReadWrite : MonoBehaviour
{
    public List<int> gameScores;
    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "Save.dat");
        LoadScores();
        gameScores.Sort();
        gameScores.Reverse();
    }

    public void AddScore(int score)
    {
        gameScores.Add(score);
        gameScores.Sort();
        gameScores.Reverse();
        if (gameScores.Count > 20)
        {
            for(int i = 20; i < gameScores.Count; i ++)
            {
                gameScores.RemoveAt(i);
            }
        }
    }

    public int scoreIndex(int score)
    {
        if (gameScores.Contains(score))
            return gameScores.IndexOf(score);
        else return -1;
    }

    public int[] GetScores()
    {
        return gameScores.ToArray();
    }

    public void Save()
    {
        List<string> stringList = new List<string>();
        foreach(int i in gameScores)
        {
            stringList.Add(i.ToString());
        }
        System.IO.File.WriteAllLines(savePath, stringList.ToArray());
    }

    private void LoadScores()
    {
        try
        {
            string[] lines = System.IO.File.ReadAllLines(savePath);
            foreach (string line in lines)
            {
                gameScores.Add(int.Parse(line));
            }
        } catch
        {
            print("No File");
        }
    }
} 