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
    

    //variable to remember if house is already full
    public bool house1Full = false;
    public bool house2Full = false;
    public bool house3Full = false;
    public bool house4Full = false;
    public bool house5Full = false;

    public GameObject frogInHome;

    public GameObject explosionFX;
    public GameObject waterSplashFX;

    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip splashSound;

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
            GetComponent<AudioSource>().PlayOneShot(jumpSound);
            myGameManager.UpdateScore(0);
        }
       else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > myGameManager.levelConstraintBottom)
        {
            transform.Translate(new Vector2(0, -1));
            GetComponent<AudioSource>().PlayOneShot(jumpSound);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > myGameManager.levelConstraintLeft)
        {
            transform.Translate(new Vector2(-1, 0));
            GetComponent<AudioSource>().PlayOneShot(jumpSound);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < myGameManager.levelConstraintRight)
        {
            transform.Translate(new Vector2(1, 0));
            GetComponent<AudioSource>().PlayOneShot(jumpSound);
        }

    }

    void LateUpdate()
    {
        if (playerIsAlive == true)
        {

            if (isInWater == true && isOnPlatform == false)
            {
                Instantiate(waterSplashFX, transform.position, Quaternion.identity);
                GetComponent<AudioSource>().PlayOneShot(splashSound);
                KillPlayer();             
                isInWater = false;
                isOnPlatform = false;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerIsAlive == true)
        {
            if (collision.transform.GetComponent<Vehicle>() != null)
            {
                //myGameManager.UpdateLives(1);
                Instantiate(explosionFX, transform.position, Quaternion.identity);
                GetComponent<AudioSource>().PlayOneShot(deathSound);
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
                
                if (house1Full == false)
                {
                    myGameManager.UpdateScore(50);
                }
                house1Full = true;
                Instantiate(frogInHome, transform.position = new Vector3(0, 4, 0), Quaternion.identity);
                transform.position = new Vector3(0, -4, 0);
                

            }
            else if (collision.transform.tag == "Home2")
            {
                

                if (house2Full == false)
                {
                    myGameManager.UpdateScore(50);
                }
                house2Full = true;
                Instantiate(frogInHome, transform.position = new Vector3(2, 4, 0), Quaternion.identity);
                transform.position = new Vector3(0, -4, 0);

            }
            else if (collision.transform.tag == "Home3")
            {               
                if (house3Full == false)
                {
                    myGameManager.UpdateScore(50);
                }
                house3Full = true;
                Instantiate(frogInHome, transform.position = new Vector3(4, 4, 0), Quaternion.identity);
                transform.position = new Vector3(0, -4, 0);

            }
            else if (collision.transform.tag == "Home4")
            {              
                if (house4Full == false)
                {
                    myGameManager.UpdateScore(50);
                }
                house4Full = true;
                Instantiate(frogInHome, transform.position = new Vector3(-2, 4, 0), Quaternion.identity);
                transform.position = new Vector3(0, -4, 0);

            }
            else if (collision.transform.tag == "Home5")
            {              
                if (house5Full == false)
                {
                    myGameManager.UpdateScore(50);
                }
                house5Full = true;
                Instantiate(frogInHome, transform.position = new Vector3(-4, 4, 0), Quaternion.identity);
                transform.position = new Vector3(0, -4, 0);

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
            

        }

    }

    void KillPlayer()
    {
       

        //Instantiate(explosionFX, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().enabled = false;
        transform.position = new Vector3(-0, -4, 0);
        GetComponent<SpriteRenderer>().enabled = true;
        myGameManager.UpdateLives(1);

    }

}
