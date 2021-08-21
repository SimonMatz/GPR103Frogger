using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeGuests : MonoBehaviour
{
    private int randomNumber;

    public GameObject fly;
    public GameObject crocodile;

    public Player myPlayer;


    //variable to remember if house is already full
    public bool guest1 = false;
    public bool guest2 = false;
    public bool guest3 = false;
    public bool guest4 = false;
    public bool guest5 = false;


    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update()
    {

        if (myPlayer.playerCanMove == true)
        {
            randomNumber = Random.Range(1, 9999);

            if (randomNumber == 1 && guest1 == false && myPlayer.house1Full == false)
            {
                Instantiate(fly, transform.position = new Vector3(-4, 4, 0), Quaternion.identity);
                guest1 = true;
            }
            if (randomNumber == 2 && guest2 == false && myPlayer.house2Full == false)
            {
                Instantiate(fly, transform.position = new Vector3(-2, 4, 0), Quaternion.identity);
                guest2 = true;
            }
            if (randomNumber == 3 && guest3 == false && myPlayer.house3Full == false)
            {
                Instantiate(fly, transform.position = new Vector3(0, 4, 0), Quaternion.identity);
                guest3 = true;
            }
            if (randomNumber == 4 && guest4 == false && myPlayer.house4Full == false)
            {
                Instantiate(fly, transform.position = new Vector3(2, 4, 0), Quaternion.identity);
                guest4 = true;
            }
            if (randomNumber == 5 && guest5 == false && myPlayer.house5Full == false)
            {
                Instantiate(fly, transform.position = new Vector3(4, 4, 0), Quaternion.identity);
                guest5 = true;
            }
        }

    }
    
    
}