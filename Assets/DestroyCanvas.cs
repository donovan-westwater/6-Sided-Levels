using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCanvas : MonoBehaviour
{
    /*public static bool TutorialIsOpen = true;*/

    public GameObject TutorialUICanvas;
    

    public void DestroyGameObject()
    {
        
        Destroy(TutorialUICanvas);
    }
}
