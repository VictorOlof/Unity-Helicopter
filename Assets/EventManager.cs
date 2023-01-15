using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public delegate void PlayingAction();
    public static event PlayingAction PlayerPlaying;

    

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            PlayerPlaying?.Invoke();
            //exampleEvent?.Invoke();
            // shortcut for if (exampleEvent! = null) exampleEvent();
        }
        
    }
} 
