using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectorControl : MonoBehaviour
{
    //leftLevelVisible: Used mainly for when the first level is highlighted.
    //Since there is no level 0 there should be not a preview to the left of level 1.
    public  GameObject leftLevelVisible;

    //leftLevel: Used to determine what game object is on the left side of the level select UI
    public GameObject leftLevel;

    public GameObject centerLevel;

    public GameObject rightLevel;
    
    //For image preview changes.
    
    public Sprite level1Img;

    public Sprite level2Img;

    //Next three lines are used for modifying the "Level 1,2,3, etc." text on the left, right, and center text boxes.
    
    public Text levelTextLeft;

    public Text levelTextCenter;

    public Text levelTextRight;

    //SelectedLevel: Used for keeping track of current highlighted level for scene transitions.
    int SelectedLevel = 1; 

    // Update is called once per frame
    void Update()
    {
        if (SelectedLevel == 1) 
        {
            leftLevelVisible.SetActive(false);
            centerLevel.GetComponent<Image>().sprite = level1Img;
            levelTextCenter.text = "Level 1";
            rightLevel.GetComponent<Image>().sprite = level2Img;
            levelTextRight.text = "Level 2";
        }
        else if (SelectedLevel == 2)
        {
            leftLevelVisible.SetActive(true);
            leftLevel.GetComponent<Image>().sprite = level1Img;
            levelTextLeft.text = "Level 1";
            centerLevel.GetComponent<Image>().sprite = level2Img;
            levelTextCenter.text = "Level 2";
            rightLevel.GetComponent<Image>().sprite = level2Img;
            levelTextRight.text = "Level 3";
        }
    }

    //MoveRight: used for the right button on level select to alter the current visible previews and text.
    public void MoveRight()
    {
        if (SelectedLevel < 7)
        {
            SelectedLevel += 1;
            Debug.Log("Move forward one level");
        }
        else
        {
            SelectedLevel = 7;
            Debug.Log("Already at latest level. Cannot go any further");
        }
    }

    //MoveLeft: used for the left button on level select to alter the current visible previews and text.
    public void MoveLeft()
    {
        if (SelectedLevel > 1)
        {
            SelectedLevel -= 1;
            Debug.Log("Moved Back one level");
        }
        else
        {
            SelectedLevel = 1;
            Debug.Log("Already at earliest level");
        }
    }

    //PlaySelectLevel: Used for the play button.
    public void PlaySelectedLevel()
    {
        if (SelectedLevel == 1)
        {
            SceneManager.LoadScene("Level 1");
            Debug.Log("Loading Level 1");
        }
        else if (SelectedLevel == 2)
        {
            SceneManager.LoadScene("Level 2");
            Debug.Log("Loading Level 2");
        }
        else if (SelectedLevel == 3)
        {
            //SceneManager.LoadScene("Level 3"); To Be Updated with once more level scenes are available.
            Debug.Log("Loading Level 3");
        }
        else if (SelectedLevel == 4)
        {
            //SceneManager.LoadScene("Level 4");
            Debug.Log("Loading Level 4");
        }
        else if (SelectedLevel == 5)
        {
            //SceneManager.LoadScene("Level 5");
            Debug.Log("Loading Level 5");
        }
        else if (SelectedLevel == 6)
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
