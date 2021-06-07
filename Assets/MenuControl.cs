using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
 
    public void ButtonStart()
    {
        SceneManager.LoadScene(2);
        MenuClickSound.click.Audio.PlayOneShot(MenuClickSound.click.MenuClick);
    }

    public void ButtonLevelSelect()
    {
        SceneManager.LoadScene(1);
        MenuClickSound.click.Audio.PlayOneShot(MenuClickSound.click.MenuClick);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
