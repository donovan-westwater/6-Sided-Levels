using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    //Level Manager should keep be the one keeping the stats on player, not player itself
    public static Level_Manager manager;
    int currentLevel = 2;
    int highestUnlockedLevel; //Dont use for now. This exists purely for making a level selecter in the future. 
                              //The level manager infomation is the stuff that is going to be used for saving
    void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }
    public void nextLevel()
    {
        currentLevel += 1;
        if (SceneManager.sceneCountInBuildSettings > currentLevel) SceneManager.LoadScene(currentLevel);
        else
        {
            currentLevel = 1;
            SceneManager.LoadScene(0);
        }
    }
    public void setLevel(int lvl)
    {
        currentLevel = lvl;
    }
    public void restartLevel()
    {
        //SceneManager.LoadScene(currentLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}