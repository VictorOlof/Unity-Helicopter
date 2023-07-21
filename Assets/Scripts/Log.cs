using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Log : MonoBehaviour
{
    void Awake()
    {

        LevelEvents.OnLevelParamChanged += LogOnLevelParamChanged;
        LevelEvents.OnPlayerCKPT += LogOnPlayerCKPT;
    }

    private void OnDestroy() 
    {
        LevelEvents.OnLevelParamChanged -= LogOnLevelParamChanged;
        LevelEvents.OnPlayerCKPT -= LogOnPlayerCKPT;
    }

    private void LogOnLevelParamChanged(LevelParameters currentLevelParameters)
    {
        Debug.Log("InvokeLevelParamChanged");
    }

    private void LogOnPlayerCKPT()
    {
        Debug.Log("OnPlayerCKPT");
    }
}
