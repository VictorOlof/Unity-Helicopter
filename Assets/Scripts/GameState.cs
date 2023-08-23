using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates 
{ 
    WaitingToStart, 
    Playing, 
    Dead 
}

public static class GameState 
{
    public static PlayerStates PlayerState = PlayerStates.WaitingToStart;

    public delegate void WaitStateHandler();
    public static event WaitStateHandler OnWaitingToStartStateEvent;
    public static void TriggerWaitingToStartStateEvent() 
    {
        OnWaitingToStartStateEvent?.Invoke();
    }
    
    public delegate void PlayStateHandler();
    public static event PlayStateHandler OnPlayState;
    public static void TriggerPlayStateEvent() 
    {
        OnPlayState?.Invoke();
    }
    
    public delegate void DeadStateHandler();
    public static event DeadStateHandler OnDeadState;
    public static void TriggerDeadStateEvent() 
    {
        OnDeadState?.Invoke();
    }
}
