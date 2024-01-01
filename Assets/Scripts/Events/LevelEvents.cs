using System.Collections.Generic;
using System;
using UnityEngine;

public static class LevelEvents
{
    public enum EventType
    {
        SetPlayerCKPT,
        PrepNewLevelEvent,
        PlayerCKPT,
        NewLevelEvent,
        LevelTimerComplete
    }

    private static EventType latestEventType;

    public static EventType LatestEventType
    {
        get { return latestEventType; }
    }

    public delegate void SetPlayerCKPT(Vector2 latestSpawnedLinePosition);
    public static event SetPlayerCKPT OnSetPlayerCKPT;

    public static void InvokeOnSetPlayerCKPT(Vector2 latestSpawnedLinePosition)
    {
        OnSetPlayerCKPT?.Invoke(latestSpawnedLinePosition);
        //latestEventType = EventType.SetPlayerCKPT;
    }

    public delegate void PrepNewLevelEvent(LevelParameters levelParameters);
    public static event PrepNewLevelEvent OnPrepNewLevelEvent;

    public static void InvokeOnPrepNewLevelEvent(LevelParameters levelParameters)
    {
        OnPrepNewLevelEvent?.Invoke(levelParameters);
        latestEventType = EventType.PrepNewLevelEvent;
    }

    public delegate void PlayerCKPT();
    public static event PlayerCKPT OnPlayerCKPT;

    public static void InvokeOnPlayerCKPT()
    {
        OnPlayerCKPT?.Invoke();
        //latestEventType = EventType.PlayerCKPT;
    }

    public delegate void NewLevelEvent(LevelParameters levelParameters);
    public static event NewLevelEvent OnNewLevel;
    public static void InvokeOnNewLevel(LevelParameters levelParameters)
    {
        OnNewLevel?.Invoke(levelParameters);
        latestEventType = EventType.NewLevelEvent;
    }

    public delegate void LevelTimerEvent();
    public static event LevelTimerEvent OnLevelTimerComplete;
    public static void InvokeLevelTimerComplete()
    {
        OnLevelTimerComplete?.Invoke();
        //latestEventType = EventType.LevelTimerComplete;
    }
}
