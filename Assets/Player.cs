using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

/// <summary>
/// This script must be used as the core Player script for managing the player character in the game.
/// </summary>
public class Player : MonoBehaviour
{
    public string playerName = ""; //The players name for the purpose of storing the high score
   
    public int playerTotalLives; //Players total possible lives.
    public int playerLivesRemaining; //PLayers actual lives remaining.
   
    public bool playerIsAlive = true; //Is the player currently alive?
    public bool playerCanMove = false; //Can the player currently move?
    public bool isOnPlatform = false;
    public bool isInWater = false;
    public bool isInHome = false;

    public GameObject frogInHome;

    private GameManager myGameManager; //A reference to the GameManager in the scene.

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y <myGameManager.levelConstraintTop)
        {
            transform.Translate(new Vector2(0, 1));
            myGameManager.UpdateScore(1);
        }
       else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > myGameManager.levelConstraintBottom)
        {
            transform.Translate(new Vector2(0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > myGameManager.levelConstraintLeft)
        {
            transform.Translate(new Vector2(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < myGameManager.levelConstraintRight)
        {
            transform.Translate(new Vector2(1, 0));
        }

    }

    void LateUpdate()
    {
        if (playerIsAlive == true)
        {
            if (isInWater == true && isOnPlatform == false)
            {
                KillPlayer();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerIsAlive == true)
        {
            if (collision.transform.GetComponent<Vehicle>() != null)
            {
                KillPlayer();
            }
            else if (collision.transform.GetComponent<Platform>() != null)
            {
                transform.SetParent(collision.transform);
                isOnPlatform = true;
                
            }
            else if (collision.transform.tag == "Water")
            {
                isInWater = true;
            }
            else if (collision.transform.tag == "Home")
            {
                isInHome = true;
                myGameManager.UpdateScore(50);
                Instantiate(frogInHome, transform.position, Quaternion.identity);
                transform.Translate(new Vector2(0, -8));
                

            }
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (playerIsAlive == true)
        {
          
            if (collision.transform.GetComponent<Platform>() != null)
            {
                transform.SetParent(null);
                isOnPlatform = false;
            }

            else if (collision.transform.tag == "Water")
            {
                isInWater = false;
            }
            else if (collision.transform.tag == "Home")
            {
                isInHome = false;
            }

        }

    }

    void KillPlayer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        playerIsAlive = false;
        playerCanMove = false;
    }

}
