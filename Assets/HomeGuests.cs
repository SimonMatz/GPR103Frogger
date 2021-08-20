using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeGuests : MonoBehaviour
{
    private int randomNumber;

    public GameObject fly;
    public GameObject crocodile;

    public Player myPlayer;


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

            if (randomNumber == 1)
                Instantiate(fly, transform.position = new Vector3(0, 4, 0), Quaternion.identity);

            if (randomNumber == 2)
                Instantiate(fly, transform.position = new Vector3(2, 4, 0), Quaternion.identity);

            if (randomNumber == 3)
                Instantiate(fly, transform.position = new Vector3(4, 4, 0), Quaternion.identity);

            if (randomNumber == 4)
                Instantiate(fly, transform.position = new Vector3(-2, 4, 0), Quaternion.identity);

            if (randomNumber == 5)
                Instantiate(fly, transform.position = new Vector3(-4, 4, 0), Quaternion.identity);

        }

    }
    
    
}