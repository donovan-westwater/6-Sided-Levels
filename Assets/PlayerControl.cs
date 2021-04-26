using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject manager;
    public Collider2D[] walls;
    public bool rotateMode = false;
    public bool gameover;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Level Manager");
        walls = FindObjectsOfType<Collider2D>();
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateMode == false)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime, Input.GetAxis("Vertical") * Time.deltaTime, 0);
            if (movement != Vector3.zero)
            {
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                //transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                //Vector3 facing = gameObject.transform.GetChild(0).gameObject.transform.position;
                //gameObject.transform.GetChild(0).gameObject.transform.RotateAround(transform.position, Vector3.forward, Vector2.Angle(facing - transform.position, movement)*0.05f);

            }
            transform.position += movement;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //CHECK BOUNDS WITH CRICLECAST (WONT DETECT COLLIDERS OF THE THE THINGS ITS STARTS INSIDE OF !!!!!)
            //project a cicle forward in direction of rotation with circlecast, if it collides with something, have it turn on a flag
            if (!isHoveringOverWall()) rotateMode = !rotateMode;
            //Physics.IgnoreLayerCollision(1, 0,!rotateMode);
            //this.GetComponent<CircleCollider2D>().enabled = !rotateMode;
        }
    }

    void FixedUpdate()
    {
        // Need optimization
        if (rotateMode == true)
        {
            // We need to check in case we FORCEFULLY rotate over a wall that we're not suposed to (RotateWalls, PhaseWalls, etc.)
            foreach (Collider2D c in walls)
            {
                if (c.Equals(this.GetComponent<CircleCollider2D>())) continue;

                // Needs tuning - Sometimes going over a wall isn't detected when it should be.
                Camera main = GameObject.Find("Main Camera").GetComponent<Camera>();
                RaycastHit ray;
                bool isRay;
                isRay = Physics.Raycast(main.transform.position, -(main.transform.position - this.transform.position), out ray, 100f, Physics.DefaultRaycastLayers);
                float z = (!isRay) ? 0.5f : ray.point.z * 0.5f;
                if (c.transform.position.z > z) continue;

                if (c.OverlapPoint(this.transform.position))
                {
                    if ((c.gameObject.tag.Equals("RotateWall") || c.gameObject.tag.Equals("RotateWallSwitch") || c.gameObject.tag.Equals("PhaseWall") || c.gameObject.tag.Equals("PhaseWallSwitch")) && gameover == false)
                    {
                        gameover = true;
                        Debug.Log("Game Over Condition");
                        //manager.GetComponent<Level_Manager>().nextLevel();
                    }
                }
            }
        }
    }

    //Is very computationally exspenseive, OPTIMIZE THIS! 
    //Current issue, Walls seem to all be set to ignore collsion at the begining of the game
    //Ethier start changing to the face system or change the way walls are checked (Use overlap area to do this!)
    bool isHoveringOverWall()
    {
        //if (rotateMode == false) return false;
        walls = FindObjectsOfType<Collider2D>();
        foreach (Collider2D c in walls)
        {
            if (c.Equals(this.GetComponent<CircleCollider2D>())) continue;
            //if (Physics2D.GetIgnoreCollision(this.GetComponent<CircleCollider2D>(), c)) continue;
            //Get a raycast to the cube to f [needs to be tested via running the game and breakpoint watching still!!]
            Camera main = GameObject.Find("Main Camera").GetComponent<Camera>();
            RaycastHit ray;
            bool isRay;
            isRay = Physics.Raycast(main.transform.position, -(main.transform.position - this.transform.position), out ray, 100f, Physics.DefaultRaycastLayers);
            float z = (!isRay) ? 0.5f : ray.point.z * 0.5f;
            if (c.transform.position.z > z) continue;

            if (c.OverlapPoint(this.transform.position))
            {
                return true;
            }
        }
        return false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        float dist = contact.separation;
        Debug.Log("Contact point count: "+collision.contactCount);
        Debug.Log("Contact point dist: " + dist);
        if (dist < -0.2) manager.GetComponent<Level_Manager>().restartLevel();
    }
}

    
