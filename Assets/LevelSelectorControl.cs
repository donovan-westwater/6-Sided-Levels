using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorControl : MonoBehaviour
{
    //leftLevelVisible: Used mainly for when the first level is highlighted.
    //Since there is no level 0 there should be not a preview to the left of level 1.
    public static bool leftLevelVisible = false;

    //leftLevel: Used to determine what game object is on the left side of the level select UI
    public GameObject leftLevel;

    //SelectedLevel: Used for keeping track of current highlighted level for scene transitions.
    int SelectedLevel = 1; 

    // Update is called once per frame
    void Update()
    {
        
    }

    //MoveRight: used for the right button on level select to alter the current visible previews and text.
    public void MoveRight()
    {

    }

    //MoveLeft: used for the left button on level select to alter the current visible previews and text.
    public void MoveLeft()
    {

    }

    //PlaySelectLevel: Used for the play button.
    public void PlaySelectedLevel()
    {
        if (SelectedLevel = 1)
        {
            SceneManager.LoadScene("Level 1");
            Debug.Log("Loading Level 1");
        }
        else if (SelectedLevel = 2)
        {
            SceneManager.LoadScene("Level 2");
            Debug.Log("Loading Level 2");
        }
        else if (SelectedLevel = 3)
        {
            //SceneManager.LoadScene("Level 3"); To Be Updated with once more level scenes are available.
            Debug.Log("Loading Level 3");
        }
        else if (SelectedLevel = 4)
        {
            //SceneManager.LoadScene("Level 4");
            Debug.Log("Loading Level 4");
        }
        else if (SelectedLevel = 5)
        {
            //SceneManager.LoadScene("Level 5");
            Debug.Log("Loading Level 5");
        }
        else if (SelectedLevel = 6)
        {
            //SceneManager.LoadScene("Level 6");
            Debug.Log("Loading Level 6");
        }
        else
        {
            //SceneManager.LoadScene("Level 7");
            Debug.Log("Loading Level 7");
        }
    }
}
