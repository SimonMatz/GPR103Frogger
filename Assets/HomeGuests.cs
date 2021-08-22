using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeGuests : MonoBehaviour
{
    //variable to store random number
    private int randomNumber;

    public GameObject worm;
    public Player myPlayer; //A reference to Player in the scene.

    //variables to remember if house is already full
    public bool guest1 = false;
    public bool guest2 = false;
    public bool guest3 = false;
    public bool guest4 = false;
    public bool guest5 = false;

    // Start is called before the first frame update
    void Start()
    {
        //storing reference to Player in this variable
        myPlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayer.playerCanMove == true)
        {
            //creates random number between 1 and 11111
            randomNumber = Random.Range(0, 11111);

            // when random number is 1-5 and this hasn't happened before or frog is already in this home - spawn worm in home location
            // (checking with guest variable and houseFull variable)
            if (randomNumber == 1 && guest1 == false && myPlayer.house1Full == false)
            {
                Instantiate(worm, transform.position = new Vector3(-3.6f, 4, 0), Quaternion.identity);
                guest1 = true;
            }
            if (randomNumber == 2 && guest2 == false && myPlayer.house2Full == false)
            {
                Instantiate(worm, transform.position = new Vector3(-1.9f, 4, 0), Quaternion.identity);
                guest2 = true;
            }
            if (randomNumber == 3 && guest3 == false && myPlayer.house3Full == false)
            {
                Instantiate(worm, transform.position = new Vector3(0, 4, 0), Quaternion.identity);
                guest3 = true;
            }
            if (randomNumber == 4 && guest4 == false && myPlayer.house4Full == false)
            {
                Instantiate(worm, transform.position = new Vector3(1.9f, 4, 0), Quaternion.identity);
                guest4 = true;
            }
            if (randomNumber == 5 && guest5 == false && myPlayer.house5Full == false)
            {
                Instantiate(worm, transform.position = new Vector3(3.6f, 4, 0), Quaternion.identity);
                guest5 = true;
            }
        }

    }
}