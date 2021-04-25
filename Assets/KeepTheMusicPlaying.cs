using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KeepTheMusicPlaying : MonoBehaviour
{
    static KeepTheMusicPlaying instance = null; 
    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
         if (instance != null)
        {
            Destroy(gameObject);
        }
        else { instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
