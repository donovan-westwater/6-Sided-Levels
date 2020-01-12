using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    //GOAL: This should incriment the current level count when touched by the player. In should also tell the level manager to load the next level
    //Current issue: Level loads as soon as contact is made! needs to only look for collsions when the player isnt rotating
    // Start is called before the first frame update
    Level_Manager manager;
    PlayerControl player;
    void Start()
    {
        manager = GameObject.Find("Level Manager").GetComponent<Level_Manager>();
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!player.rotateMode) manager.nextLevel();
    }
}
