﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    //Wall needs to disable collider if not on screen
    GameObject Player;
    Camera main;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        main = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Currentlly all of the walls are ignoring collsion, that should not be true!
       // bool watch = Physics2D.GetIgnoreCollision(Player.GetComponent<CircleCollider2D>(), this.GetComponent<BoxCollider2D>()); //Only exisits to check for ignore, delete when done
        bool noHit = true;
        Vector3 dir = main.transform.position - this.transform.position;
        //Experimental (new code, may not work)
        RaycastHit ray;
        bool checkhit = Physics.Raycast(main.transform.position, -(main.transform.position - Player.transform.position), out ray,100f, Physics.DefaultRaycastLayers);
        float zlayer = ray.point.z*0.5f; //Currently is causing the check to ignore walls by having layer be after wall
        if (!checkhit) zlayer = 0.5f;
        //End of new code
        RaycastHit2D[] rays;
        rays = Physics2D.RaycastAll(main.transform.position, -dir, 100f, Physics2D.DefaultRaycastLayers, -11, zlayer); //Orignally 0.5f
        Debug.DrawLine(main.transform.position, main.transform.position - dir);
        //if (this.CompareTag("RotateWall")) Debug.Log(" TEST");
            foreach (RaycastHit2D r in rays){
            if (r.collider.gameObject.tag.Equals("Level"))
            {
                break;
            }
            if (r.collider == this.GetComponent<BoxCollider2D>())
            {
                Physics2D.IgnoreCollision(Player.GetComponent<CircleCollider2D>(), this.GetComponent<BoxCollider2D>(), (Player.GetComponent<PlayerControl>().rotateMode && (this.CompareTag("Wall") || this.CompareTag("WallSwitch"))));
                if (this.CompareTag("PhaseWall") || this.CompareTag("PhaseWallSwitch")) Physics2D.IgnoreCollision(Player.GetComponent<CircleCollider2D>(), this.GetComponent<BoxCollider2D>(), (!Player.GetComponent<PlayerControl>().rotateMode));
                //if (this.CompareTag("RotateWall") || this.CompareTag("RotateWallSwitch")) Debug.Log(Player.GetComponent<PlayerControl>().rotateMode && !this.CompareTag("RotateWall"));
                noHit = false;
            }
        }
        if (noHit)
        {
            Physics2D.IgnoreCollision(Player.GetComponent<CircleCollider2D>(), this.GetComponent<BoxCollider2D>(), true);
        }
        /*
        RaycastHit2D ray;
        ray  = Physics2D.Raycast(main.transform.position, -dir,100f,Physics2D.DefaultRaycastLayers,-11,0.5f);
        Debug.DrawLine(main.transform.position, main.transform.position - dir);
        if (ray.collider == this.GetComponent<BoxCollider2D>())
        {
            Physics2D.IgnoreCollision(Player.GetComponent<CircleCollider2D>(), this.GetComponent<BoxCollider2D>(), false);
        }
        else
        {
            Physics2D.IgnoreCollision(Player.GetComponent<CircleCollider2D>(), this.GetComponent<BoxCollider2D>(),true);
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Player.GetComponent<PlayerControl>().rotateMode) Debug.Log(this.gameObject.name);
    }
    /*
    // Disable the behaviour when it becomes invisible...
    void OnBecameInvisible()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log("I CANT BE SEEN!");
    }

    // ...and enable it again when it becomes visible.
    void OnBecameVisible()
    {
        this.GetComponent<BoxCollider2D>().enabled = true;
        Debug.Log("VISIBLE!");
    }
    */
    //TODO: Add sprite changes for switch walls and normal walls
    public void ChangeWallType(int walltype)
    {
        switch (walltype)

        {
            case 0:
                this.tag = "Wall";
                this.GetComponent<SpriteRenderer>().color = Color.black;
                break;
            case 1:
                this.tag = "RotateWall";
                this.GetComponent<SpriteRenderer>().color = Color.red;
                break;

            case 2:
                this.tag = "PhaseWall";
                this.GetComponent<SpriteRenderer>().color = Color.black;
                break;
            case 3:
                this.tag = "WallSwitch";
                this.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);
                break;
            case 4:
                this.tag = "RotateWallSwitch";
                this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                break;

            case 5:
                this.tag = "PhaseWallSwitch";
                this.GetComponent<SpriteRenderer>().color = new Color (0, 1, 0, 1);
                break;




            default:
                this.tag = "Wall";
                this.GetComponent<SpriteRenderer>().color = Color.black;
                break;
        }
    }
}
