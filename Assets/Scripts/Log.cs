using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Log : MonoBehaviour
{
    void Awake()
    {
        LevelEvents.OnNewLevel += LogOnNewLevel;
        LevelEvents.OnPlayerCKPT += LogOnPlayerCKPT;
    }

    private void OnDestroy() 
    {
        LevelEvents.OnNewLevel -= LogOnNewLevel;
        LevelEvents.OnPlayerCKPT -= LogOnPlayerCKPT;
    }

    private void LogOnNewLevel()
    {
        Debug.Log("InvokeOnNewLevel");
    }

    private void LogOnPlayerCKPT()
    {
        Debug.Log("OnPlayerCKPT");
    }
}
