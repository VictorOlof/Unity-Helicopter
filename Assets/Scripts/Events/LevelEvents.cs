using System.Collections.Generic;
using System;

public static class LevelEvents
{
    public delegate void SetPlayerCKPT();
    public static event SetPlayerCKPT OnSetPlayerCKPT;

    public static void InvokeOnSetPlayerCKPT()
    {
        OnSetPlayerCKPT?.Invoke();
    }

    public delegate void PlayerCKPT();
    public static event PlayerCKPT OnPlayerCKPT;

    public static void InvokeOnPlayerCKPT()
    {
        OnPlayerCKPT?.Invoke();
    }

    public delegate void NewLevelEvent(); //LevelParameters currentLevelParameters
    public static event NewLevelEvent OnNewLevel; 
    public static void InvokeOnNewLevel() // LevelParameters currentLevelParameters
    {
        OnNewLevel?.Invoke(); //currentLevelParameters
    }

    public delegate void LevelTimerEvent();
    public static event LevelTimerEvent OnLevelTimerComplete; 
    public static void InvokeLevelTimerComplete()
    {
        OnLevelTimerComplete?.Invoke();
    }
}