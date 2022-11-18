using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static int Score { get; private set; }

    public static event Action<int> OnScoreChanged;

    private void Start()
    {
        Score = 0;
    }

    public static void Add(int points)
    {
        Score += points;
        Debug.Log(Score);
        OnScoreChanged?.Invoke(Score);
        int highscore = PlayerPrefs.GetInt("HighScore", 0);
        if (Score > highscore)
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
    }
}
