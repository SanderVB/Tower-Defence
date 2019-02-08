using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    GameSession gameSession;
    Text scoreText;

    // Use this for initialization
    void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
        scoreText.text = "000";
    }

    public void UpdateScoreDisplay(int score)
    {
        scoreText.text = score.ToString("000");
    }
}
