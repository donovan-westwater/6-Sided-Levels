using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool rotateMode = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateMode == false)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime, Input.GetAxis("Vertical") * Time.deltaTime,0);
            if (movement != Vector3.zero)
            {
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                //transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                Vector3 facing = gameObject.transform.GetChild(0).gameObject.transform.position;
                gameObject.transform.GetChild(0).gameObject.transform.RotateAround(transform.position, Vector3.forward, Vector2.Angle(facing - transform.position, movement)*0.05f);
            }
            transform.position += movement;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotateMode = !rotateMode;
            //Physics.IgnoreLayerCollision(1, 0,!rotateMode);
            this.GetComponent<CircleCollider2D>().enabled = !rotateMode;
        }
    }
}
