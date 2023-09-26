using System.Collections.Generic;
using System;
using UnityEngine;

public static class LevelEvents
{
    public delegate void SetPlayerCKPT(Vector2 latestSpawnedLinePosition);
    public static event SetPlayerCKPT OnSetPlayerCKPT;

    public static void InvokeOnSetPlayerCKPT(Vector2 latestSpawnedLinePosition)
    {
        OnSetPlayerCKPT?.Invoke(latestSpawnedLinePosition);
    }

    

    public delegate void PrepNewLevelEvent(LevelParameters levelParameters);
    public static event PrepNewLevelEvent OnPrepNewLevelEvent;

    public static void InvokeOnPrepNewLevelEvent(LevelParameters levelParameters)
    {
        OnPrepNewLevelEvent?.Invoke(levelParameters);
    }

    public delegate void PlayerCKPT();
    public static event PlayerCKPT OnPlayerCKPT;

    public static void InvokeOnPlayerCKPT()
    {
        OnPlayerCKPT?.Invoke();
    }

    public delegate void NewLevelEvent(LevelParameters levelParameters);
    public static event NewLevelEvent OnNewLevel; 
    public static void InvokeOnNewLevel(LevelParameters levelParameters)
    {
        OnNewLevel?.Invoke(levelParameters);
    }

    public delegate void LevelTimerEvent();
    public static event LevelTimerEvent OnLevelTimerComplete; 
    public static void InvokeLevelTimerComplete()
    {
        OnLevelTimerComplete?.Invoke();
    }
}