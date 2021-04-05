using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    // Start tells the text to be its original color
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }
    
    //OnMouse Enter tells the text to change color when the mouse is touching it. 
    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    //OnMouseExit tells the text to return to its original color when the mouse is no longer hovering over it.
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

}
