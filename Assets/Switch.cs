using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Switch & cycle present SwitchWalls on the level through the use of this switch.
public class Switch : MonoBehaviour
{
    public GameObject player;
    public GameObject[] RotateWalls;
    public GameObject[] PhaseWalls;
    public GameObject[] Walls;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Collision checks
    // Cycle: RotateWall -> PhaseWall -> Wall. And repeat!
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Find ALL the current switch walls and put them into respective arrays, so we can switch them!
        RotateWalls = GameObject.FindGameObjectsWithTag("RotateWallSwitch");
        PhaseWalls  = GameObject.FindGameObjectsWithTag("PhaseWallSwitch");
        Walls       = GameObject.FindGameObjectsWithTag("WallSwitch");

        if (!player.GetComponent<PlayerControl>().rotateMode)
        {
            // Change RotateWalls -> PhaseWalls
            foreach (GameObject wall in RotateWalls)
            {
                wall.tag = "PhaseWallSwitch";
                wall.GetComponent<SpriteRenderer>().color = new Color (0, 1, 0, 1);
            }

            // Change PhaseWalls -> Walls
            foreach (GameObject wall in PhaseWalls)
            {
                wall.tag = "WallSwitch";
                wall.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);
            }

            // Change Walls -> RotateWalls
            foreach (GameObject wall in Walls)
            {
                wall.tag = "RotateWallSwitch";
                wall.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            }
        }
    }

}
