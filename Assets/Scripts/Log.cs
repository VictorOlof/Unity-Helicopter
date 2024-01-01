using UnityEngine;
using System.Collections.Generic;
using System;

public class Log : MonoBehaviour
{
    void Awake()
    {
        LevelEvents.OnSetPlayerCKPT      += LogOnSetPlayerCKPT;
        LevelEvents.OnPlayerCKPT         += LogOnPlayerCKPT;
        LevelEvents.OnPrepNewLevelEvent  += LogOnPrepNewLevelEvent;
        LevelEvents.OnNewLevel           += LogOnNewLevel;
        LevelEvents.OnLevelTimerComplete += LogOnLevelTimerComplete;
    }

    private void OnDestroy() 
    {
        LevelEvents.OnSetPlayerCKPT      -= LogOnSetPlayerCKPT;
        LevelEvents.OnPlayerCKPT         -= LogOnPlayerCKPT;
        LevelEvents.OnPrepNewLevelEvent  -= LogOnPrepNewLevelEvent;
        LevelEvents.OnNewLevel           -= LogOnNewLevel;
        LevelEvents.OnLevelTimerComplete -= LogOnLevelTimerComplete;
    }

    private void LogOnSetPlayerCKPT(Vector2 _)
    {
        //Debug.Log("LogOnSetPlayerCKPT");
    }

    private void LogOnPlayerCKPT()
    {
        //Debug.Log("LogOnPlayerCKPT");
    }

    private void LogOnPrepNewLevelEvent(LevelParameters levelParameters)
    {
        //Debug.Log("LogOnPrepNewLevelEvent levelDuration: " + levelParameters.levelDuration);
    }

    private void LogOnNewLevel(LevelParameters levelParameters)
    {
        //Debug.Log("LogOnNewLevel levelDuration: " + levelParameters.levelDuration);
    }

    private void LogOnLevelTimerComplete()
    {
        //Debug.Log("LogOnLevelTimerComplete");
    }
}