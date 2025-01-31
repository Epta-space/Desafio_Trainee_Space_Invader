using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    public int score;
    public TMP_Text ScoreText;
    void Start()
    {
        
    }

    public void UpdateScore(int points)
    {
        score += points;
        ScoreText.text = "Score:" + score;
    }
}
