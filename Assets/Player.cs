using System.Collections;
using System.Collections.Generic;
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
    public bool isOnPlatform = false; //is the player on a platform?
    public bool isInWater = false;//is the player in the water?

    //variable to remember if house is already full
    public bool house1Full = false;
    public bool house2Full = false;
    public bool house3Full = false;
    public bool house4Full = false;
    public bool house5Full = false;

    //instantiate objects
    public GameObject frogInHome;
    public GameObject explosionFX;
    public GameObject waterSplashFX;

    //audio files
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip splashSound;
    public AudioClip homeSound;
    public AudioClip bonusSound;

    private GameManager myGameManager; //A reference to the GameManager in the scene.
    private HomeGuests myHomeGuests; //A reference to HomeGuests in the scene.

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = GameObject.FindObjectOfType<GameManager>();
        myHomeGuests = GameObject.FindObjectOfType<HomeGuests>();
    }

    // Update is called once per frame
    void Update()
    {
        //player movement is controlled here
        if (playerCanMove == true)
        {
            //when key is pressed - move player by 1 in according direction and play jump sound
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < myGameManager.levelConstraintTop)
            {
                transform.Translate(new Vector2(0, 1));
                GetComponent<AudioSource>().PlayOneShot(jumpSound);
                myGameManager.UpdateScore(10);
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
    }

    void LateUpdate()
    {
        if (playerIsAlive == true)
        {
            //player dies when he's not on platform
            if (isInWater == true && isOnPlatform == false)
            {
                Instantiate(waterSplashFX, transform.position, Quaternion.identity);
                GetComponent<AudioSource>().PlayOneShot(splashSound);
                KillPlayer();
                //preventing loop from running again
                isInWater = false;
                isOnPlatform = false;
            }
        }

        //checks if all 5 houses are full to win the game
        if(house1Full == true && house2Full == true && house3Full == true && house4Full == true && house5Full == true)
        {          
            myGameManager.UpdateScore(1000);
            //setting one variable back to false to prevent loop from running again
            house3Full = false;
            myGameManager.playerIsAlive = true;
            myGameManager.GameOver(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerIsAlive == true)
        {
            //kill player if he hits vehicle
            if (collision.transform.GetComponent<Vehicle>() != null)
            {              
                Instantiate(explosionFX, transform.position, Quaternion.identity);
                GetComponent<AudioSource>().PlayOneShot(deathSound);
                KillPlayer();              
            }
            //checks if player is on platform
            else if (collision.transform.GetComponent<Platform>() != null)
            {
                //sets platform as parent to move player with the platform
                transform.SetParent(collision.transform);
                isOnPlatform = true;
            }
            //checks if player is in water
            else if (collision.transform.tag == "Water")
            {              
                isInWater = true;
            }
            //checks if player has reached home1
            else if (collision.transform.tag == "Home")
            {
                //if player hasn't filled this home yet give the points
                if (house1Full == false)
                {
                    myGameManager.UpdateScore(50);
                    GetComponent<AudioSource>().PlayOneShot(homeSound);
                }
                //if home is already full, no points and kill player
                if (house1Full == true)
                {
                    Instantiate(explosionFX, transform.position, Quaternion.identity);
                    GetComponent<AudioSource>().PlayOneShot(deathSound);
                    KillPlayer();
                }

                house1Full = true;
                Instantiate(frogInHome, transform.position = new Vector3(-3.6f, 4, 0), Quaternion.identity);
                //move player back to starting position
                transform.position = new Vector3(0, -4, 0);              
            }
            //checks if player has reached home2
            else if (collision.transform.tag == "Home2")
            {              
                if (house2Full == false)
                {
                    myGameManager.UpdateScore(50);
                    GetComponent<AudioSource>().PlayOneShot(homeSound);
                }

                if (house2Full == true)
                {
                    Instantiate(explosionFX, transform.position, Quaternion.identity);
                    GetComponent<AudioSource>().PlayOneShot(deathSound);
                    KillPlayer();
                }

                house2Full = true;
                Instantiate(frogInHome, transform.position = new Vector3(-1.9f, 4, 0), Quaternion.identity);
                transform.position = new Vector3(0, -4, 0);
            }
            //checks if player has reached home3
            else if (collision.transform.tag == "Home3")
            {               

                if (house3Full == false)
                {
                    myGameManager.UpdateScore(50);
                    GetComponent<AudioSource>().PlayOneShot(homeSound);
                }

                if (house3Full == true)
                {
                    Instantiate(explosionFX, transform.position, Quaternion.identity);
                    GetComponent<AudioSource>().PlayOneShot(deathSound);
                    KillPlayer();
                }
                house3Full = true;
                Instantiate(frogInHome, transform.position = new Vector3(0, 4, 0), Quaternion.identity);
                transform.position = new Vector3(0, -4, 0);
            }
            //checks if player has reached home4
            else if (collision.transform.tag == "Home4")
            {              
                if (house4Full == false)
                {
                    myGameManager.UpdateScore(50);
                    GetComponent<AudioSource>().PlayOneShot(homeSound);
                }

                if (house4Full == true)
                {
                    Instantiate(explosionFX, transform.position, Quaternion.identity);
                    GetComponent<AudioSource>().PlayOneShot(deathSound);
                    KillPlayer();
                }
                house4Full = true;
                Instantiate(frogInHome, transform.position = new Vector3(1.9f, 4, 0), Quaternion.identity);
                transform.position = new Vector3(0, -4, 0);
            }
            //checks if player has reached home5
            else if (collision.transform.tag == "Home5")
            {              
                if (house5Full == false)
                {
                    myGameManager.UpdateScore(50);
                    GetComponent<AudioSource>().PlayOneShot(homeSound);
                }

                if (house5Full == true)
                {
                    Instantiate(explosionFX, transform.position, Quaternion.identity);
                    GetComponent<AudioSource>().PlayOneShot(deathSound);
                    KillPlayer();
                }
                house5Full = true;
                Instantiate(frogInHome, transform.position = new Vector3(3.6f, 4, 0), Quaternion.identity);
                transform.position = new Vector3(0, -4, 0);
            }
            //checks if player collected bonus points in home
            if (collision.transform.tag == "Fly")
            {
                myGameManager.UpdateScore(100);
                //destroy object when player hits
                Destroy(collision.gameObject);
                GetComponent<AudioSource>().PlayOneShot(bonusSound);
            }
        }
    }
   

    void OnTriggerExit2D(Collider2D collision)
    {
        if (playerIsAlive == true)
        {         
            if (collision.transform.GetComponent<Platform>() != null)
            {
                //remove platform as parent to move player free again
                transform.SetParent(null);
                isOnPlatform = false;
            }

            else if (collision.transform.tag == "Water")
            {
                isInWater = false;
            }
        }
    }

    void KillPlayer() // kills player, moves player back to start position and updates lives. When no loves left, calls game over screen
    {
        if (myGameManager.playerLivesRemaining == 1)
        {
            playerIsAlive = false;
        }

        //disable and enable sprite renderer and move back to start
        GetComponent<SpriteRenderer>().enabled = false;
        transform.position = new Vector3(0, -4, 0);
        GetComponent<SpriteRenderer>().enabled = true;
        myGameManager.UpdateLives(1);

        if (playerIsAlive == false)
        {
            myGameManager.GameOver(false);
        }
    }
}
