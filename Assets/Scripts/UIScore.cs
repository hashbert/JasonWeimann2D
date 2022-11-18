using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        ScoreSystem.OnScoreChanged += UpdateScoreText;
        UpdateScoreText(ScoreSystem.Score);
    }

    private void OnDestroy()
    {
        ScoreSystem.OnScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        _text.SetText(score.ToString());
    }
}
