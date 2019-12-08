using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelrotater : MonoBehaviour
{
    public GameObject player;
  
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerControl>().rotateMode)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * 20f * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right * Input.GetAxis("Vertical") * 20f * Time.deltaTime, Space.World);
            
        }
    }
}
