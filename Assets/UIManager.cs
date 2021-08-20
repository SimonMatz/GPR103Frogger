using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject GameOverWindow;
    public GameObject PauseWindow;

    public Player myPlayer;


    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseWindow.SetActive(!PauseWindow.activeSelf);

            if (PauseWindow.activeSelf == true)
            {
                Time.timeScale = 0;
                myPlayer.playerCanMove = false;
                
            }
            else
            {
                myPlayer.playerCanMove = true;
                Time.timeScale = 1;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }


}
