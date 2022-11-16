using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance { get; private set; } // could be private
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance=this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
