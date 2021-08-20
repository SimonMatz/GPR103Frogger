﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script is to be attached to a GameObject called GameManager in the scene. It is to be used to manager the settings and overarching gameplay loop.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Scoring")]
    public float currentScore = 0; //The current score in this round.
    public int highScore = 0; //The highest score achieved either in this session or over the lifetime of the game.
    public int playerTotalLives = 10; //Players total possible lives.
    public int playerLivesRemaining; //PLayers actual lives remaining.
    public bool gameOver = false;
    public bool playerIsAlive = true;

    [Header("UI")]
    public TMP_Text currentScoreUI;
    public TMP_Text currentLivesUI;
    public TMP_Text timeLeftUI;
    public TMP_Text GameOverMessage;
    public UIManager myUIManager;

    [Header("Playable Area")]
    public float levelConstraintTop; //The maximum positive Y value of the playable space.
    public float levelConstraintBottom; //The maximum negative Y value of the playable space.
    public float levelConstraintLeft; //The maximum negative X value of the playable space.
    public float levelConstraintRight; //The maximum positive X value of the playablle space.

    [Header("Gameplay Loop")]
    public bool isGameRunning; //Is the gameplay part of the game current active?
    public float totalGameTime; //The maximum amount of time or the total time avilable to the player.
    public float gameTimeRemaining; //The current elapsed time


    public void Awake()
    {
        myUIManager = FindObjectOfType<UIManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
        UpdateScore(-currentScore);
        UpdateLives(-playerTotalLives);
        playerLivesRemaining = playerTotalLives;
        gameTimeRemaining = totalGameTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTimeRemaining > 0)
        {
           gameTimeRemaining -= Time.deltaTime;
            
            timeLeftUI.text = Mathf.Round(gameTimeRemaining).ToString();
        }
        if (gameTimeRemaining > 0 && gameOver == true && playerIsAlive == true)
        {          
            UpdateScore(Mathf.Round(gameTimeRemaining) * 20);
            playerIsAlive = false;          
            
        }

        else if(gameTimeRemaining <= 0)
        {
            playerIsAlive = false;
            timeLeftUI.text = 0.ToString();
        }
    }

    public void UpdateScore(float scoreAmount)
    {
        currentScore += scoreAmount;
        currentScoreUI.text = currentScore.ToString();
        
    }
    public void UpdateLives(int livesAmount)
    {
        playerLivesRemaining -= livesAmount;
        currentLivesUI.text = playerLivesRemaining.ToString();

        if (playerLivesRemaining <= 0)
        {
            playerIsAlive = false;
        }
    }

    public void GameOver(bool win)
    {
        if (win == true)
        {
            GameOverMessage.text = "You win!";
        }
        else
        {
            GameOverMessage.text = "You lose!";
        }
        myUIManager.GameOverWindow.SetActive(true);
    }

}
