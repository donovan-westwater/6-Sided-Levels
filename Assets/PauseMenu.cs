using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    
    }

    //Used for resuming the game. Timescale 1f resume the game speed and allows
    //the player to resume playing the game.
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        MenuClickSound.click.Audio.PlayOneShot(MenuClickSound.click.MenuClick);
    }

    //Used for pausing the game. Timescale 0f ensures the player cannot move around
    //and play the game while the pause menu is up.
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        MenuClickSound.click.Audio.PlayOneShot(MenuClickSound.click.MenuClick);
    }
    
    public void LoadMenu()
    {
       //Timescale code used here to ensure game is not paused in main menu.
        Time.timeScale = 1f;
        //Loads the Menu scene.
        SceneManager.LoadScene("Menu");
        Debug.Log("Loading Menu...");
        MenuClickSound.click.Audio.PlayOneShot(MenuClickSound.click.MenuClick);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}

