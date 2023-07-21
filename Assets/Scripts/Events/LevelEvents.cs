using System.Collections.Generic;
using System;

public static class LevelEvents
{
    public delegate void PlayerCKPT();
    public static event PlayerCKPT OnPlayerCKPT;

    public static void InvokeOnPlayerCKPT()
    {
        OnPlayerCKPT?.Invoke();
    }

    public delegate void NewLevelEvent(LevelParameters levelParameter);
    public static event NewLevelEvent OnLevelParamChanged; 
    public static void InvokeLevelParamChanged(LevelParameters currentLevelParameters)
    {
        OnLevelParamChanged?.Invoke(currentLevelParameters);
    }

    public delegate void LevelTimerEvent();
    public static event LevelTimerEvent OnLevelTimerComplete; 
    public static void InvokeLevelTimerComplete()
    {
        OnLevelTimerComplete?.Invoke();
    }
}