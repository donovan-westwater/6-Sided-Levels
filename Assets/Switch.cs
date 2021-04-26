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
    Camera main;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        main = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Collision checks
    private void FixedUpdate()
    {
        bool noHit = true;
        Vector3 dir = main.transform.position - this.transform.position;
        RaycastHit ray;
        bool checkhit = Physics.Raycast(main.transform.position, -(main.transform.position - player.transform.position), out ray, 100f, Physics.DefaultRaycastLayers);
        float zlayer = ray.point.z * 0.5f; //Currently is causing the check to ignore walls by having layer be after wall
        if (!checkhit) zlayer = 0.5f;
        //End of new code
        RaycastHit2D[] rays;
        rays = Physics2D.RaycastAll(main.transform.position, -dir, 100f, Physics2D.DefaultRaycastLayers, -11, zlayer); //Orignally 0.5f
        Debug.DrawLine(main.transform.position, main.transform.position - dir);
        foreach (RaycastHit2D r in rays)
        {
            if (r.collider.gameObject.tag.Equals("Level"))
            {
                break;
            }
            if (r.collider == this.GetComponent<CircleCollider2D>())
            {
                Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), this.GetComponent<CircleCollider2D>(), player.GetComponent<PlayerControl>().rotateMode);
                noHit = false;
            }
        }
        if (noHit)
        {
            Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), this.GetComponent<CircleCollider2D>(), true);
        }
    }
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

            GameObject.Find("Switch").transform.Rotate(0, 0, 120.0f, Space.Self);
        }

    }

}
