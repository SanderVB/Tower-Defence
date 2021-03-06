﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    [SerializeField] int playerHealth = 3;
    int score;

    private void Awake()
    {
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;
        if (gameSessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void SetPlayerHealth(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            LevelFinished(false);
            print("0");
        }
        FindObjectOfType<HealthDisplay>().UpdateHealthDisplay(playerHealth);
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
        FindObjectOfType<ScoreDisplay>().UpdateScoreDisplay(score);
    }

    public void LevelFinished (bool hasWon)
    {
        if(hasWon)
        {
            //show win screen and start next level
            print("Player has won");
        }
        else
        {
            //show lose screen and restart level/back to main menu
            print("Player has lost");
        }
    }
}
