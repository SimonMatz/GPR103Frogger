using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject GameOverWindow;
    public GameObject PauseWindow;
    public Player myPlayer; //A reference to Player in the scene.

    // Start is called before the first frame update
    void Start()
    {
        //storing reference to Player in this variable
        myPlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // game pause with timeScale from: https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/
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
        //setting timeScale back to one so game is not frozen after restart
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RulesMenu()
    {
        SceneManager.LoadScene(2);
    }

}
