using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    //GOAL: This should incriment the current level count when touched by the player. In should also tell the level manager to load the next level
    //Current issue: Level loads as soon as contact is made! needs to only look for collsions when the player isnt rotating
    // Start is called before the first frame update
    public GameObject manager;
    public GameObject player;
    Camera main;
    void Start()
    {
        manager = GameObject.Find("Level Manager");
        player = GameObject.Find("Player");
        main = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
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
                //Debug.Log(Physics2D.GetIgnoreCollision(player.GetComponent<CircleCollider2D>(), this.GetComponent<CircleCollider2D>()));
                //Debug.Log("Can exit");
                Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), this.GetComponent<CircleCollider2D>(), player.GetComponent<PlayerControl>().rotateMode);
                noHit = false;
            }
        }
        if (noHit)
        {
            Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), this.GetComponent<CircleCollider2D>(), true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!player.GetComponent<PlayerControl>().rotateMode) manager.GetComponent<Level_Manager>().nextLevel();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!player.GetComponent<PlayerControl>().rotateMode) manager.GetComponent<Level_Manager>().nextLevel();
    }
}
