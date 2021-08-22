using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Scoring")]
    public float currentScore = 0; //The current score in this round.
    //private float highScore; ---- Removed this variable because using PlayerPrefs to store highscore ---
    public int playerTotalLives; //Players total possible lives.
    public int playerLivesRemaining; //PLayers actual lives remaining.
    public bool gameOver = false;
    public bool playerIsAlive = true;

    // variables for UI elements
    [Header("UI")]
    public TMP_Text currentScoreUI;
    public TMP_Text currentLivesUI;
    public TMP_Text timeLeftUI;
    public TMP_Text GameOverMessage;
    public TMP_Text finalScore;
    public TMP_Text highScoreUI;
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

    public Player myPlayer;
    private bool timeWasUp = false;


    public void Awake()
    {
        //storing reference to UImanager in this variable
        myUIManager = FindObjectOfType<UIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //setting the lives and time available
        playerTotalLives = 3;
        totalGameTime = 60;

        //storing reference to Player in this variable
        myPlayer = FindObjectOfType<Player>();

        //initialising highscore
        highScoreUI.text = PlayerPrefs.GetFloat("HighScore", 0).ToString();

        //resetting&initialising current score and lives remaining
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
        
        else if (gameTimeRemaining <= 0 && timeWasUp == false)
        {
            playerIsAlive = false;
            timeLeftUI.text = 0.ToString();
            GameOver(false);
            //since loop is in update, need to use this variable so the loop only runs once
            timeWasUp = true;
        }
    }

    public void UpdateScore(float scoreAmount)   //Function to update scores
    {
        currentScore += scoreAmount;
        currentScoreUI.text = currentScore.ToString();

        //followed highscore tutorial from https://www.youtube.com/watch?v=vZU51tbgMXk
        if (currentScore > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", currentScore);
            highScoreUI.text = currentScore.ToString();
        }        
    }

    public void UpdateLives(int livesAmount)     //Function to update lives
    {
        playerLivesRemaining -= livesAmount;
        currentLivesUI.text = playerLivesRemaining.ToString();

        if (playerLivesRemaining <= 0)
        {
            playerIsAlive = false;
        }
    }

    public void GameOver(bool win)  //stops game, sets win/lose message, opens game over window and calcs extra points for time left
    {
        //to pause game
        Time.timeScale = 0;
        myPlayer.playerCanMove = false;
        
        if (win == true)
        {
            GameOverMessage.text = "You win!";
        }
        else
        {
            GameOverMessage.text = "You lose!";
        }
        myUIManager.GameOverWindow.SetActive(true);

        if (gameTimeRemaining > 0 && playerIsAlive == true)
        {
            UpdateScore(Mathf.Round(gameTimeRemaining) * 20);
            playerIsAlive = false;

        }

        finalScore.text = currentScore.ToString();
    }
}
