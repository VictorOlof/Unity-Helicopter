using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates { WaitingToStart, Playing, Dead }
public static class GameState 
{
    public static PlayerStates PlayerState = PlayerStates.WaitingToStart;

    public delegate void DeadStateHandler();
    public static event DeadStateHandler OnDeadState;

    public static void TriggerDeadStateEvent() 
    {
        if (OnDeadState != null) 
        {
            OnDeadState();
        }
    }
}
