using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClickSound : MonoBehaviour
{

	public AudioSource Audio;
	public AudioClip MenuClick;
	public static MenuClickSound click;

    void Awake()
    {
   
         if (click != null)
        {
            Destroy(gameObject);
        }
        else { click = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
